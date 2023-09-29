using System;
using System.Collections.Generic;

public static class Memory
{
    private static readonly Dictionary<string, int> m_memory = new Dictionary<string, int>();

    public static int Read(string name)
    {
        if (m_memory.TryGetValue(name, out int value))
        {
            return value;
        }
        else
        {
            throw new Exception($"Variável '{name}' não encontrada na memória.");
        }
    }

    public static void Write(string name, int value)
    {
        m_memory[name] = value;
    }
}
