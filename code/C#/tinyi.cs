using System;

namespace TinyInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("num: " + args.Length);
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: " + args[0] + " [Tiny program]");
                Environment.Exit(1);
            }

            try
            {
               //Lexeme lex;
                using (LexicalAnalysis l = new LexicalAnalysis(args[0]))
                {
                    SyntaticAnalysis s = new SyntaticAnalysis(l);
                    Command cmd = s.Start();
                    //cmd.Execute();
                }
            }
            catch (Exception error)
            {
                Console.Error.WriteLine("error: " + error.Message);
            }
        }
    }
}
