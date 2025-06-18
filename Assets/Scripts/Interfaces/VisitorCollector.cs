public class VisitorCollector : IVisitor
{
    private ResourseCreator _resourseCollector;

    public VisitorCollector(ResourseCreator collector)
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
