namespace math_parser.Scanner;

public class Scanner
{
    private readonly string _source;
    private readonly List<Token> _tokens;

    private int start = 0;
    private int current = 0;
    private int line = 1;


    private bool IsEnd() => current >= _source.Length;
    
    public Scanner(string source)
    {
        _source = source;
        _tokens = new List<Token>();
    }

    public IEnumerable<Token> ScanTokens()
    {
        while (!IsEnd())
        {
            start = current;
            ScanToken();
        }
        
        _tokens.Add(new Token(TokenType.EOF, "", null, line));
        return _tokens;
    }

    private void ScanToken()
    {
        var c = Consume();
        if (IsDigit(c))
        {
            Number();
            return;
        }

        switch (c)
        {
            case '(':
                AddToken(TokenType.LPAREN, '(');
                break;
            case ')':
                AddToken(TokenType.RPAREN, ')');
                break;
            case '.':
                AddToken(TokenType.DOT, '.');
                break;
            case '-':
                AddToken(TokenType.MINUS, '-');
                break;
            case '+':
                AddToken(TokenType.PLUS, '+');
                break;
            case '*':
                AddToken(TokenType.STAR, '*');
                break;
            case '/':
                AddToken(TokenType.SLASH, '/');
                break;
            case ' ':
            case '\r':
            case '\t':
                break;
            case '\n':
                line++;
                break;
            default:
                Console.WriteLine("Error");
                break;
        }
    }

    private void Number()
    {
        while (IsDigit(Peek())) Consume();

        if (Peek() == '.' && IsDigit(PeekNext()))
        {
            Consume();

            while (IsDigit(Peek())) Consume();
        }
        
        AddToken(TokenType.NUMBER, _source.Substring(start, current - start));
    }
    
    private static bool IsDigit(char c)
    {
        return c is >= '0' and <= '9';
    }

    private char Consume()
    {
        return _source[current++];
    }

    private char Peek()
    {
        return IsEnd() ? '\0' : _source[current];
    }
    
    private char PeekNext()
    {
        return current + 1 >= _source.Length ? '\0' : _source[current + 1];
    } 

    private void AddToken(TokenType type, object literal)
    {
        var text = _source.Substring(start, current - start);
        _tokens.Add(new Token(type, text, literal, line));
    }
}