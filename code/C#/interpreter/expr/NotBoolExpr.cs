public class CustomBoolExpr : BoolExpr
{
    private BoolExpr m_expr;
    public CustomBoolExpr(int line, BoolExpr expr) : base(line)
    {
        m_expr = expr;
    }

    public override bool Expr()
    {
        return !m_expr.Expr();
    }
}
