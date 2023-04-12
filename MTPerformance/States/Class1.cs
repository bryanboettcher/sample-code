using System.Collections.Concurrent;
using MassTransit;
using MassTransit.Courier.Contracts;

namespace MTPerformance.States
{
    public class MetraTechItem : SagaStateMachineInstance
    {
        // machinery for MT
        public Guid CorrelationId { get; set; }
        public string? CurrentState { get; set; }

        // machinery for nested records
        //public HashSet<Guid> Children { get; set; }
        public Dictionary<Guid, decimal>? ChildValues { get; set; }
        public int TotalChildren { get; set; }
        public Guid? ParentId { get; set; }

        // compile-time 'propertybag' items
        public int CustomerRecordId { get; set; }   // customer's unique identifier
        public int? CustomerParentId { get; set; }  // customer's unique parent-id
        public string ProductName { get; set; }     // customer's product name
        public string Description { get; set; }     // our generated text
        public decimal FinalPrice { get; set; }     // seems like a useful thing to calculate
    }

    public class MetraTechItemStateMachine : MassTransitStateMachine<MetraTechItem>
    {
        private static readonly ConcurrentDictionary<int, Guid?> IdMappings = new();

        public MetraTechItemStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Initially(
                    When(ItemReady)
                        .Then(ctx =>
                        {
                            ctx.Saga.CustomerRecordId = ctx.Message.RecordId;
                            ctx.Saga.CustomerParentId = ctx.Message.ParentId;
                            ctx.Saga.ProductName = ctx.Message.ProductName ?? "";
                            ctx.Saga.TotalChildren = ctx.Message.TotalChildren ?? 0;
                        })
                        .Then(ctx =>
                        {
                            // this is a really dumb way to do this, but works for a demo
                            IdMappings.TryAdd(ctx.Message.RecordId, ctx.CorrelationId);

                            if (ctx.Message.ParentId is null)
                                return;

                            if (IdMappings.TryGetValue(ctx.Message.ParentId.Value, out var parentId))
                                ctx.Saga.ParentId = parentId;
                        })
                        .TransitionTo(Processing)
                        .PublishAsync(ctx => ctx.Init<PrepareRouting>(new
                        {
                            ctx.Message.CorrelationId,
                            ctx.Message.RecordId,
                            ctx.Saga.ParentId,
                            ctx.Message.ProductName,
                            RemainingChildren = ctx.Message.TotalChildren
                        }))
                    );

            During(Processing, 
                When(ItemSuspending)
                    .Then(ctx =>
                    {
                        ctx.Saga.Description = "QUERY " + ctx.GetVariable<int>("ProductId");
                    })
                    .TransitionTo(Waiting),
                When(PriceCalculated)
                    .Then(ctx =>
                    {
                        ctx.Saga.FinalPrice = ctx.GetVariable<decimal>("FinalPrice") ?? -1m;
                    }));

            During(Processing, Waiting, 
                When(ChildUpdate)
                    .Then(ctx =>
                    {
                        var childId = ctx.Message.ChildId;
                        var value = ctx.Message.Value;

                        // ctx *should* be the parent here
                        if (ctx.Saga.ChildValues is null)
                            ctx.Saga.ChildValues = new();

                        ctx.Saga.ChildValues.Add(childId, value);
                    })
                    .If(ctx => (ctx.Saga.ChildValues?.Count ?? 0) == ctx.Saga.TotalChildren, 
                        ctx => ctx
                            .TransitionTo(Processing)
                            .PublishAsync(p => p.Send<PrepareRouting>(new
                            {
                                p.CorrelationId,
                                p.Saga.RecordId,
                                p.Saga.ParentId,
                                p.Saga.ProductName,
                                RemainingChildren = p.Saga.TotalChildren
                            })))
            );
        }

        public State Processing { get; set; }
        public State Waiting { get; set; }
        public State Complete { get; set; }



        public Event<ItemReady> ItemReady { get; }
        public Event<CapturePrice> PriceCalculated { get; }
        public Event<SuspendItem> ItemSuspending { get; }
        public Event<NestedUpdate> ChildUpdate { get; }
    }

    public interface ItemReady
    {
        Guid CorrelationId { get; }
        int RecordId { get; }
        int? ParentId { get; }
        int? TotalChildren { get; }
        string? ProductName { get; }
    }

    public interface PrepareRouting
    {
        Guid CorrelationId { get; }
        int RecordId { get; }
        Guid? ParentId { get; }
        string? ProductName { get; }
        int RemainingChildren { get; }
    }

    public interface SuspendItem : RoutingSlipActivityCompleted
    {
        Guid CorrelationId { get; }
    }

    public interface CapturePrice : RoutingSlipActivityCompleted
    {
        Guid CorrelationId { get; }
    }

    public interface NestedUpdate
    {
        Guid CorrelationId { get; }
        Guid ChildId { get; }

        decimal Value { get; }
    }
}
