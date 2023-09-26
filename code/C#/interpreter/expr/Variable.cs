public class Variable : IntExpr
{
    private string name;
    private int value;

    public Variable(int line, string name) : base(line)
    {
        this.name = name;
        this.value = 0;
    }

    public string Name
    {
        get { return name; }
    }

    public void SetValue(int value)
    {
        this.value = value;
    }

    public override int Expr()
    {
        return value;
    }
}
