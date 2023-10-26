using System;

public enum TokenType
{
	// Specials
	TT_UNEXPECTED_EOF = -2,
	TT_INVALID_TOKEN = -1,
	TT_END_OF_FILE = 0,

	// Symbols
	TT_SEMICOLON,     // ;
	TT_ASSIGN,        // =

	// Logic operators
	TT_EQUAL,         // ==
	TT_NOT_EQUAL,     // !=
	TT_LOWER,         // <
	TT_LOWER_EQUAL,   // <=
	TT_GREATER,       // >
	TT_GREATER_EQUAL, // >=

	// Arithmetic operators
	TT_ADD,           // +
	TT_SUB,           // -
	TT_MUL,           // *
	TT_DIV,           // /
	TT_MOD,           // %
	TT_POW, 		  // ^

	// Keywords
	TT_PROGRAM,       // program
	TT_WHILE,         // while
	TT_DO,            // do
	TT_DONE,          // done
	TT_IF,            // if
	TT_THEN,          // then
	TT_ELSE,          // else
	TT_OUTPUT,        // output
	TT_TRUE,          // true
	TT_FALSE,         // false
	TT_READ,          // read
	TT_NOT,           // not

	// Others
	TT_NUMBER,        // number
	TT_VAR            // variable

}

public static class TokenUtility
{
	public static string TokenTypeToString(TokenType type)
	{
		switch (type)
		{
			// Specials
			case TokenType.TT_UNEXPECTED_EOF:
				return "UNEXPECTED_EOF";
			case TokenType.TT_INVALID_TOKEN:
				return "INVALID_TOKEN";
			case TokenType.TT_END_OF_FILE:
				return "END_OF_FILE";

			// Symbols
			case TokenType.TT_SEMICOLON:
				return "SEMICOLON";
			case TokenType.TT_ASSIGN:
				return "ASSIGN";

			// Logic operators
			case TokenType.TT_EQUAL:
				return "EQUAL";
			case TokenType.TT_NOT_EQUAL:
				return "NOT_EQUAL";
			case TokenType.TT_LOWER:
				return "LOWER";
			case TokenType.TT_LOWER_EQUAL:
				return "LOWER_EQUAL";
			case TokenType.TT_GREATER:
				return "GREATER";
			case TokenType.TT_GREATER_EQUAL:
				return "GREATER_EQUAL";

			// Arithmetic operators
			case TokenType.TT_ADD:
				return "ADD";
			case TokenType.TT_SUB:
				return "SUB";
			case TokenType.TT_MUL:
				return "MUL";
			case TokenType.TT_DIV:
				return "DIV";
			case TokenType.TT_MOD:
				return "MOD";
			case TokenType.TT_POW:
				return "POW";

			// Keywords
			case TokenType.TT_PROGRAM:
				return "PROGRAM";
			case TokenType.TT_WHILE:
				return "WHILE";
			case TokenType.TT_DO:
				return "DO";
			case TokenType.TT_DONE:
				return "DONE";
			case TokenType.TT_IF:
				return "IF";
			case TokenType.TT_THEN:
				return "THEN";
			case TokenType.TT_ELSE:
				return "ELSE";
			case TokenType.TT_OUTPUT:
				return "OUTPUT";
			case TokenType.TT_TRUE:
				return "TRUE";
			case TokenType.TT_FALSE:
				return "FALSE";
			case TokenType.TT_READ:
				return "READ";
			case TokenType.TT_NOT:
				return "NOT";

			// Others
			case TokenType.TT_NUMBER:
				return "NUMBER";
			case TokenType.TT_VAR:
				return "VAR";

			default:
				throw new Exception("Invalid token type");
		}

	}
}
