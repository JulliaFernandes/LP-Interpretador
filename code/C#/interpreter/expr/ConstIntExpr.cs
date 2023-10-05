public class ConstIntExpr : IntExpr
{
    private int m_value;

    public ConstIntExpr(int line, int value)
        : base(line)
    {
        System.Console.WriteLine("ConstIntEXPR: " + value);
        m_value = value;
    }

    public override int Expr()
    {
        return m_value;
    }
}
