public class WhileCommand : Command
{
    private BoolExpr m_cond;
    private Command m_cmds;

    public WhileCommand(int line, BoolExpr cond, Command cmds)
        : base(line)
    {
        m_cond = cond;
        m_cmds = cmds;
    }

    public override void Execute()
    {
        while (m_cond.Expr())
        {
            m_cmds.Execute();
        }
    }
}
