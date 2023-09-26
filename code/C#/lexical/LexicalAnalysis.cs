using System;
using System.IO;
using System.Collections.Generic;
public class LexicalAnalysis : IDisposable
{
	private int m_line;
	private SymbolTable m_st;
	private StreamReader m_input;
	public int Line { get { return m_line; } } 
	public LexicalAnalysis(string filename)
	{
		m_line = 1;
		m_st = new SymbolTable();
		try
		{
			m_input = new StreamReader(filename);
		}
		catch (FileNotFoundException)
		{
			throw new Exception("Unable to open file");
		}
	}
	public void Dispose()
	{
		m_input.Dispose();
	}
	public Lexeme NextToken()
	{
		int state;
		Lexeme lex = new Lexeme();

		state = 1;
		while (state != 7 && state != 8)
		{
			int c = m_input.Read();
			// std::cout << "[" << state << ", " << c
			//           << " ('" << (char) c << "')]" << std::endl;

			switch (state)
			{
				case 1:
					if (c == ' ' || c == '\t' || c == '\r')
					{
						state = 1;
					}
					else if (c == '\n')
					{
						m_line++;
						state = 1;
					}
					else if (c == '#')
					{
						state = 2;
					}
					else if (c == '=' || c == '<' || c == '>')
					{
						lex.Token += (char)c;
						state = 3;
					}
					else if (c == '!')
					{
						lex.Token += (char)c;
						state = 4;
					}
					else if (c == ';' || c == '+' || c == '-' ||
							c == '*' || c == '/' || c == '%')
					{
						lex.Token += (char)c;
						state = 7;
					}
					else if (c == '_' || char.IsLetter((char)c))
					{
						lex.Token += (char)c;
						state = 5;
					}
					else if (char.IsDigit((char)c))
					{
						lex.Token += (char)c;
						state = 6;
					}
					else
					{
						if (c == -1)
						{
							lex.Type = TokenType.TT_END_OF_FILE;
							state = 8;
						}
						else
						{
							lex.Token += (char)c;
							lex.Type = TokenType.TT_INVALID_TOKEN;
							state = 8;
						}
					}
					break;
				case 2:
					if (c == '\n')
					{
						m_line++;
						state = 1;
					}
					else if (c == -1)
					{
						lex.Type = TokenType.TT_END_OF_FILE;
						state = 8;
					}
					else
					{
						state = 2;
					}
					break;
				case 3:
					if (c == '=')
					{
						lex.Token += (char)c;
						state = 7;
					}
					else
					{
						if (c != -1)
							m_input.BaseStream.Seek(-1, SeekOrigin.Current);

						state = 7;
					}
					break;
				case 4:
					if (c == '=')
					{
						lex.Token += (char)c;
						state = 7;
					}
					else
					{
						if (c == -1)
						{
							lex.Type = TokenType.TT_UNEXPECTED_EOF;
							state = 8;
						}
						else
						{
							lex.Type = TokenType.TT_INVALID_TOKEN;
							state = 8;
						}
					}
					break;
				case 5:
					if (c == '_' || char.IsLetterOrDigit((char)c))
					{
						lex.Token += (char)c;
						state = 5;
					}
					else
					{
						if (c != -1)
							m_input.BaseStream.Seek(-1, SeekOrigin.Current);

						state = 7;
					}
					break;
				case 6:
					if (char.IsDigit((char)c))
					{
						lex.Token += (char)c;
						state = 6;
					}
					else
					{
						if (c != -1)
							m_input.BaseStream.Seek(-1, SeekOrigin.Current);

						lex.Type = TokenType.TT_NUMBER;
						state = 8;
					}
					break;
				default:
					throw new Exception("invalid state");
			}
		}

		if (state == 7)
			lex.Type = m_st.Find(lex.Token);

		return lex;

	}
}

