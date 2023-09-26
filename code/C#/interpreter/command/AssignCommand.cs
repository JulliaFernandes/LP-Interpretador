using System;

public class AssignCommand : Command
{
    private Variable m_var;
    private IntExpr m_expr;

    public AssignCommand(int line, Variable var, IntExpr expr) : base(line)
    {
        m_var = var;
        m_expr = expr;
    }

    public override void Execute()
    {
        int value = m_expr.Expr();
        m_var.SetValue(value);
    }
}
