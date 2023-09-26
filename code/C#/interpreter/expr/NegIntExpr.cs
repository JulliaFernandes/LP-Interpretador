using System;

public class NegIntExpr : IntExpr
{
    private IntExpr m_expr;

    public NegIntExpr(int line, IntExpr expr)
        :base(line)
    {
        m_expr = expr;
    }

    public override int Expr()
    {
        return -m_expr.Expr();
    }
}

