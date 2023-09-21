using System;

namespace TinyInterpreter
{
    public class SyntaticAnalysis
    {
        private LexicalAnalysis m_lex;
        private Lexeme m_current;

        public SyntaticAnalysis(LexicalAnalysis lex)
        {
            m_lex = lex;
            m_current = lex.NextToken();
        }

        public Command Start()
        {
            Command cmd = ProcProgram();
            Eat(TokenType.TT_END_OF_FILE);
            return cmd;
        }

        private void Advance()
        {
            m_current = m_lex.NextToken();
        }

        private void Eat(TokenType type)
        {
            if (type == m_current.Type)
            {
                Advance();
            }
            else
            {
                ShowError();
            }
        }

        private void ShowError()
        {
            Console.WriteLine($"{m_lex.Line:D2}: ");

            switch (m_current.Type)
            {
                case TokenType.TT_INVALID_TOKEN:
                    Console.WriteLine($"Lexema inválido [{m_current.Token}]");
                    break;
                case TokenType.TT_UNEXPECTED_EOF:
                case TokenType.TT_END_OF_FILE:
                    Console.WriteLine("Fim de arquivo inesperado");
                    break;
                default:
                    Console.WriteLine($"Lexema não esperado [{m_current.Token}]");
                    break;
            }

            Environment.Exit(1);
        }

        // <program> ::= program <cmdlist>
        private Command ProcProgram()
        {
            Eat(TokenType.TT_PROGRAM);
            Command cmd = ProcCmdList();
            return cmd;
        }

        // <cmdlist> ::= <cmd> { <cmd> }
        private BlocksCommand ProcCmdList()
        {
            int line = m_lex.Line;
            BlocksCommand cmds = new BlocksCommand(line);

            Command cmd = ProcCmd();
            cmds.AddCommand(cmd);

            while (m_current.Type == TokenType.TT_VAR ||
                   m_current.Type == TokenType.TT_OUTPUT ||
                   m_current.Type == TokenType.TT_IF ||
                   m_current.Type == TokenType.TT_WHILE)
            {
                cmd = ProcCmd();
                cmds.AddCommand(cmd);
            }

            return cmds;
        }

        // <cmd> ::= (<assign> | <output> | <if> | <while>) ;
        private Command ProcCmd()
        {
            Command cmd = null;

            if (m_current.Type == TokenType.TT_VAR)
            {
                cmd = ProcAssign();
            }
            else if (m_current.Type == TokenType.TT_OUTPUT)
            {
                cmd = ProcOutput();
            }
            else if (m_current.Type == TokenType.TT_IF)
            {
                cmd = ProcIf();
            }
            else if (m_current.Type == TokenType.TT_WHILE)
            {
                cmd = ProcWhile();
            }
            else
            {
                ShowError();
            }

            Eat(TokenType.TT_SEMICOLON);
            return cmd;
        }

        // <assign> ::= <var> = <intexpr>
        private AssignCommand ProcAssign()
        {
            int line = m_lex.Line;

            Variable var = ProcVar();
            Eat(TokenType.TT_ASSIGN);
            IntExpr expr = ProcIntExpr();

            AssignCommand cmd = new AssignCommand(line, var, expr);
            return cmd;
        }

        // <output> ::= output <intexpr>
        private OutputCommand ProcOutput()
        {
            Eat(TokenType.TT_OUTPUT);
            int line = m_lex.Line;

            IntExpr expr = ProcIntExpr();
            OutputCommand cmd = new OutputCommand(line, expr);
            return cmd;
        }

        // <if> ::= if <boolexpr> then <cmdlist> [ else <cmdlist> ] done
        private IfCommand ProcIf()
        {
            Eat(TokenType.TT_IF);
            int line = m_lex.Line;

            BoolExpr cond = ProcBoolExpr();
            Eat(TokenType.TT_THEN);
            Command thenCmds = ProcCmdList();
            Command elseCmds = null;

            if (m_current.Type == TokenType.TT_ELSE)
            {
                Advance();
                elseCmds = ProcCmdList();
            }

            Eat(TokenType.TT_DONE);

            IfCommand cmd = new IfCommand(line, cond, thenCmds, elseCmds);
            return cmd;
        }

        // <while> ::= while <boolexpr> do <cmdlist> done
        private WhileCommand ProcWhile()
        {
            Eat(TokenType.TT_WHILE);
            int line = m_lex.Line;

            BoolExpr expr = ProcBoolExpr();
            Eat(TokenType.TT_DO);
            Command cmds = ProcCmdList();
            Eat(TokenType.TT_DONE);

            WhileCommand cmd = new WhileCommand(line, expr, cmds);
            return cmd;
        }

        // <boolexpr> ::= false | true |
        //                not <boolexpr> |
        //                <intterm> (== | != | < | > | <= | >=) <intterm>
        private BoolExpr ProcBoolExpr()
        {
            if (m_current.Type == TokenType.TT_FALSE)
            {
                Advance();
                return new ConstBoolExpr(m_lex.Line, false);
            }
            else if (m_current.Type == TokenType.TT_TRUE)
            {
                Advance();
                return new ConstBoolExpr(m_lex.Line, true);
            }
            else if (m_current.Type == TokenType.TT_NOT)
            {
                Advance();
                int line = m_lex.Line;
                BoolExpr expr = ProcBoolExpr();
                return new NotBoolExpr(line, expr);
            }
            else
            {
                int line = m_lex.Line;
                IntExpr left = ProcIntTerm();

                SingleBoolExpr.Op op = SingleBoolExpr.Op.EQUAL;
                switch (m_current.Type)
                {
                    case TokenType.TT_EQUAL:
                        op = SingleBoolExpr.Op.EQUAL;
                        Advance();
                        break;
                    case TokenType.TT_NOT_EQUAL:
                        op = SingleBoolExpr.Op.NOT_EQUAL;
                        Advance();
                        break;
                    case TokenType.TT_LOWER:
                        op = SingleBoolExpr.Op.LOWER;
                        Advance();
                        break;
                    case TokenType.TT_GREATER:
                        op = SingleBoolExpr.Op.GREATER;
                        Advance();
                        break;
                    case TokenType.TT_LOWER_EQUAL:
                        op = SingleBoolExpr.Op.LOWER_EQUAL;
                        Advance();
                        break;
                    case TokenType.TT_GREATER_EQUAL:
                        op = SingleBoolExpr.Op.GREATER_EQUAL;
                        Advance();
                        break;
                    default:
                        ShowError();
                        break;
                }

                IntExpr right = ProcIntTerm();

                BoolExpr expr = new SingleBoolExpr(line, left, op, right);
                return expr;
            }
        }

        // <intexpr> ::= [ + | - ] <intterm> [ (+ | - | * | / | %) <intterm> ]
        private IntExpr ProcIntExpr()
        {
            bool isNegative = false;
            if (m_current.Type == TokenType.TT_ADD)
            {
                Advance();
            }
            else if (m_current.Type == TokenType.TT_SUB)
            {
                Advance();
                isNegative = true;
            }

            IntExpr left;
            if (isNegative)
            {
                int line = m_lex.Line;
                IntExpr tmp = ProcIntTerm();
                left = new NegIntExpr(line, tmp);
            }
            else
            {
                left = ProcIntTerm();
            }

            if (m_current.Type == TokenType.TT_ADD ||
                m_current.Type == TokenType.TT_SUB ||
                m_current.Type == TokenType.TT_MUL ||
                m_current.Type == TokenType.TT_DIV ||
                m_current.Type == TokenType.TT_MOD)
            {
                int line = m_lex.Line;

                BinaryIntExpr.Op op;
                switch (m_current.Type)
                {
                    case TokenType.TT_ADD:
                        op = BinaryIntExpr.Op.ADD;
                        Advance();
                        break;
                    case TokenType.TT_SUB:
                        op = BinaryIntExpr.Op.SUB;
                        Advance();
                        break;
                    case TokenType.TT_MUL:
                        op = BinaryIntExpr.Op.MUL;
                        Advance();
                        break;
                    case TokenType.TT_DIV:
                        op = BinaryIntExpr.Op.DIV;
                        Advance();
                        break;
                    case TokenType.TT_MOD:
                    default:
                        op = BinaryIntExpr.Op.MOD;
                        Advance();
                        break;
                }

                IntExpr right = ProcIntTerm();

                left = new BinaryIntExpr(line, left, op, right);
            }

            return left;
        }

        // <intterm> ::= <var> | <const> | read
        private IntExpr ProcIntTerm()
        {
            if (m_current.Type == TokenType.TT_VAR)
            {
                return ProcVar();
            }
            else if (m_current.Type == TokenType.TT_NUMBER)
            {
                return ProcConst();
            }
            else
            {
                Eat(TokenType.TT_READ);
                int line = m_lex.Line;
                ReadIntExpr expr = new ReadIntExpr(line);
                return expr;
            }
        }

        // <var> ::= id
        private Variable ProcVar()
        {
            string tmp = m_current.Token;
            Eat(TokenType.TT_VAR);
            int line = m_lex.Line;
            Variable var = new Variable(line, tmp);
            return var;
        }

        // <const> ::= number
        private ConstIntExpr ProcConst()
        {
            string tmp = m_current.Token;
            Eat(TokenType.TT_NUMBER);
            int line = m_lex.Line;
            int value = int.Parse(tmp);
            ConstIntExpr expr = new ConstIntExpr(line, value);
            return expr;
        }
    }
}
