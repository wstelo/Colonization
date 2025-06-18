public  class Tree : Resourse
{
    public override void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}
