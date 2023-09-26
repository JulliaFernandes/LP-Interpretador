public abstract class Command
{
    private int m_line;

    protected Command(int line)
    {
        m_line = line;
    }

    public int Line
    {
        get { return m_line; }
    }

    public abstract void Execute();
}
