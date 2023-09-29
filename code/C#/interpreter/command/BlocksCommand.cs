using System;
using System.Collections.Generic;

public class BlocksCommand : Command
{
    private List<Command> m_cmds;

    public BlocksCommand(int line)
        : base(line)
    {
        m_cmds = new List<Command>();
    }

    public void AddCommand(Command cmd)
    {
        m_cmds.Add(cmd);
    }

    public override void Execute()
    {
        foreach (Command cmd in m_cmds)
        {
            cmd.Execute();
        }
    }
}
