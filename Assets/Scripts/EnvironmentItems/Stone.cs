public class Stone : Resourse
{
    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}
