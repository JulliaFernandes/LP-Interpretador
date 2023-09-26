public class ConstBoolExpr : BoolExpr
{
    private readonly bool m_value;

    public ConstBoolExpr(int line, bool value)
        : base(line)
    {
        m_value = value;
    }

    public override bool Expr()
    {
        return m_value;
    }
}
