namespace math_parser.Scanner;

public enum TokenType
{
    LPAREN,
    RPAREN,
    MINUS,
    PLUS,
    SLASH,
    STAR,
    NUMBER,
    EOF
}

public class Token
{
    public TokenType Type;
    public string Lexeme;
    public object? Literal;

    public Token(TokenType type, string lexeme, object literal)
    {
        Type = type;
        Lexeme = lexeme;
        Literal = literal;
    }

    public override string ToString()
    {
        return $"{Type}: {Lexeme} '{Literal}'";
    }
}