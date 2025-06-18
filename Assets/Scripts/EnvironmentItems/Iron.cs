public class Iron : Resourse
{
    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}
