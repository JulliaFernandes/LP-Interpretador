using System;

public class OutputCommand : Command
{
    private IntExpr m_expr;

    public OutputCommand(int line, IntExpr expr)
        : base(line)
    {
        m_expr = expr;
    }

    public override void Execute()
    {
        int v = m_expr.Expr();
        Console.WriteLine(v);
    }
}
