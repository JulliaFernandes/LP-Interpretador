using System;

namespace TinyInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: " + args[0] + " [Tiny program]");
                Environment.Exit(1);
            }

            try
            {
               Lexeme lex;
                using (LexicalAnalysis l = new LexicalAnalysis(args[1]))
                {
                    SyntaticAnalysis s = new SyntaticAnalysis(l);
                    Command cmd = s.Start();
                    cmd.Execute();
                }
            }
            catch (Exception error)
            {
                Console.Error.WriteLine("error: " + error.Message);
            }
        }
    }
}
