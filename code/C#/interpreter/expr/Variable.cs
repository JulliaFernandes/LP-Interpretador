using System;

public class Variable : IntExpr
{
    private string m_name;

    public Variable(int line, string name) : base(line)
    {
        m_name = name;
    }

    public override int Expr()
    {
        return Value();
    }

    public int Value()
    {
        return Memory.Read(m_name);
    }

    public void SetValue(int value)
    {
        Memory.Write(m_name, value);
    }
}
