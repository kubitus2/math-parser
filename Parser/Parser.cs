using System.Text;
using math_parser.Scanner;

namespace math_parser.Parser;

public class Operator
{
    public string Name { get; set; }
    public int Precedence { get; set; }
    public bool RightAssociative { get; set; }
}

public class Parser
{
    private readonly List<Token> _tokens;
    private Stack<Token> _output;
    private Stack<Token> _operators;
    
    private Dictionary<TokenType, int> precedence = new()
    {
        { TokenType.MINUS, 0 },
        { TokenType.PLUS, 0 },
        { TokenType.STAR, 1 },
        { TokenType.SLASH, 1}
    };

    private bool TakesPrecedence(Token a, Token b)
    {
        return precedence[a.Type] <= precedence[b.Type];
    }

    public Parser(List<Token> tokens)
    {
        _tokens = tokens;
        _output = new Stack<Token>();
        _operators = new Stack<Token>();
    }

    public Stack<Token> GenerateRPN()
    {
        foreach (var token in _tokens)
        {
            switch (token.Type)
            {
                case TokenType.EOF:
                    break;
                case TokenType.NUMBER:
                    _output.Push(token);
                    break;
                case TokenType.PLUS:
                case TokenType.MINUS:
                case TokenType.STAR:
                case TokenType.SLASH:
                    while (_operators.Any()
                           && (_operators.Peek().Type == TokenType.PLUS ||
                               _operators.Peek().Type == TokenType.MINUS ||
                               _operators.Peek().Type == TokenType.STAR ||
                               _operators.Peek().Type == TokenType.SLASH)
                           && TakesPrecedence(token, _operators.Peek()))
                    {
                        _output.Push(_operators.Pop());
                    }
                    _operators.Push(token);
                    break;
                case TokenType.LPAREN:
                    _operators.Push(token);
                    break;
                case TokenType.RPAREN:
                    while (_operators.Any() && _operators.Peek().Type != TokenType.LPAREN)
                    {
                        _output.Push(_operators.Pop());
                    }

                    _operators.Pop();
                    break;
                default:
                    throw new Exception("Error");
            }
        }

        while (_operators.Any())
        {
            if (_operators.Peek().Type == TokenType.LPAREN)
                throw new Exception("Mismatched parentheses");
            
            _output.Push(_operators.Pop());
        }

        var output = _output.ToList();
        Console.Write("Reverese Polish Notation: ");
        for (var i = _output.Count - 1; i >= 0; i--)
        {
            Console.Write(output[i].Literal);
            Console.Write(" ");
        }
        Console.WriteLine();

        return _output;
    }
}