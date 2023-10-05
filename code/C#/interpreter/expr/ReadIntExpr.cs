using System;

public class ReadIntExpr : IntExpr
{
    public ReadIntExpr(int line) : base(line)
    {
    }

    public override int Expr()
    {
        int value;
        if (!int.TryParse(Console.ReadLine(), out value))
        {
            Console.WriteLine("Erro de entrada: Valor de entrada inválido.");
            Environment.Exit(1);
        }
        System.Console.WriteLine("ReadEXPR: " + value);
        return value;
    }
}
