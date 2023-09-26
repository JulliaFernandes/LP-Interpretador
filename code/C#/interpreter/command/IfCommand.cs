using System;

public class IfCommand : Command
{
    private BoolExpr m_cond;
    private Command m_thenCmds;
    private Command m_elseCmds;

    public IfCommand(int line, BoolExpr cond, Command thenCmds, Command elseCmds = null)
        : base(line)
    {
        m_cond = cond;
        m_thenCmds = thenCmds;
        m_elseCmds = elseCmds;
    }

    public override void Execute()
    {
        if (m_cond.Expr())
        {
            m_thenCmds.Execute();
        }
        else
        {
            if (m_elseCmds != null)
            {
                m_elseCmds.Execute();
            }
        }
    }
}
