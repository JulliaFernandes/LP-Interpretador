using System;

namespace TinyInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args.Length);
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: " + args[0] + " [Tiny program]");
                Environment.Exit(1);
            }

            try
            {
                Lexeme lex;
                using (LexicalAnalysis l = new LexicalAnalysis(args[0]))
                {
                    //minuto 2:21:16
                    SyntaticAnalysis s = new SyntaticAnalysis(l);
                    // Command cmd = s.Start();
                    // cmd.Execute();
                    Console.WriteLine(lex.ToString() + "\n");
                }
            }
            catch (Exception error)
            {
                Console.Error.WriteLine("error: " + error.Message);
            }
        }
    }
}
