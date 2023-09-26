public class MyCustomBoolExpr : BoolExpr // Renomeada para MyCustomBoolExpr
{
    public enum Op
    {
        EQUAL,
        NOT_EQUAL,
        LOWER,
        GREATER,
        LOWER_EQUAL,
        GREATER_EQUAL
    }

    private IntExpr m_left;
    private Op m_op;
    private IntExpr m_right;

    public MyCustomBoolExpr(int line, IntExpr left, Op op, IntExpr right):base(line)
    {
        m_left = left;
        m_op = op;
        m_right = right;
    }

    public override bool Expr()
    {
        int v1 = m_left.Expr();
        int v2 = m_right.Expr();

        switch (m_op)
        {
            case Op.EQUAL:
                return v1 == v2;
            case Op.NOT_EQUAL:
                return v1 != v2;
            case Op.LOWER:
                return v1 < v2;
            case Op.LOWER_EQUAL:
                return v1 <= v2;
            case Op.GREATER:
                return v1 > v2;
            case Op.GREATER_EQUAL:
            default:
                return v1 >= v2;
        }
    }
}
