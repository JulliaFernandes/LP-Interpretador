public abstract class IntExpr
{
    private int m_line;
    public IntExpr(int line)
    {
        m_line = line;
    }

    public int Line
    {
        get { return m_line; }
    }
    public abstract int Expr();
}
