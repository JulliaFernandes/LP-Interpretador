public abstract class BoolExpr
{
    private readonly int m_line;

    public BoolExpr(int line)
    {
        m_line = line;
    }

    public int Line
    {
        get { return m_line; }
    }

    public abstract bool Expr();
}
