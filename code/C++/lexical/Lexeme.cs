using System;
using System.Text;
public struct Lexeme {

	public string Token{ get; set;}
	public TokenType Type{get;set;}

	public Lexeme(string token, TokenType type){
		Token = token;
		Type = type;
	}
	public override string ToString(){
		StringBuilder sb = new StringBuilder();
        sb.Append("(\"");
        sb.Append(Token);
        sb.Append("\", ");
        sb.Append(TokenUtility.TokenTypeToString(Type));
        sb.Append(")");
        return sb.ToString();
	}
}
