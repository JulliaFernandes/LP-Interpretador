using System;
public class BinaryIntExpr : IntExpr
{
    public enum Op
    {
        ADD,
        SUB,
        MUL,
        DIV,
        MOD
    }

    private IntExpr left;
    private Op op;
    private IntExpr right;

    public BinaryIntExpr(int line, IntExpr left, Op op, IntExpr right) : base(line)
    {
        this.left = left;
        this.op = op;
        this.right = right;
    }

    public override int Expr()
    {
        int v1 = left.Expr();
        int v2 = right.Expr();

        switch (op) //semantica da expressao
        {
            case Op.ADD:
                return v1 + v2;
            case Op.SUB:
                return v1 - v2;
            case Op.MUL:
                return v1 * v2;
            case Op.DIV:
                if (v2 == 0)
                {
                    Console.WriteLine("Erro: Divisão por zero.");
                    Environment.Exit(1);
                }
                return v1 / v2;
            case Op.MOD:
            default:
                if (v2 == 0)
                {
                    Console.WriteLine("Erro: Divisão por zero.");
                    Environment.Exit(1);
                }
                return v1 % v2;
        }
    }
}
