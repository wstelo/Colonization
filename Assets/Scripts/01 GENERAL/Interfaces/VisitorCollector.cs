public class VisitorCollector : IVisitor
{
    private ResourseCollector _resourseCollector;

    public VisitorCollector(ResourseCollector collector)
    {
        _resourseCollector = collector;
    }

    public void Visit(Stone stone)
    {
        _resourseCollector.AddAmount(stone);
    }

    public void Visit(Tree tree)
    {
        _resourseCollector.AddAmount(tree);
    }

    public void Visit(Iron iron)
    {
        _resourseCollector.AddAmount(iron);
    }
}
