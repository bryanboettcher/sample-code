using EnumerableUniqueness;

var infiniteStream = Enumerable.Empty<CustomWidget>();

IEnumerable<CustomWidget> RemoveDuplicates(IEnumerable<CustomWidget> input)
{
    var alreadySeen = new HashSet<CustomWidget>(CustomWidget.CaseInsensitiveComparer.Instance);
    foreach (var widget in input)
    {
        if (!alreadySeen.Add(widget))
            yield return widget;
    }
}