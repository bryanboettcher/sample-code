namespace DiyAutoScanner.Library
{
    public interface IWorker {}

    public interface ISlacker {}

    public class MyWorkerClass : IWorker {}

    public class MyTeenager : IWorker, ISlacker {}

    public abstract class BaseCat<T> {}

    public class MyKitty : BaseCat<int>, ISlacker {}

    /*public interface IValidator<T> {}

    public class NotNullValidator<T> : IValidator<T>
    {
        public override string ToString() => $"NotNullValidator<{typeof(T).Name}>";
    }*/
}