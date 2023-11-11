using System.Globalization;
using math_parser.Scanner;

namespace math_parser.Evaluator;

public class Evaluator
{
    private readonly List<Token> _prn;

    public Evaluator(List<Token> prn)
    {
        _prn = prn;
    }

    public string Evaluate()
    {
        try
        {
            
            var workingStack = new Stack<double>();
            for (var i = _prn.Count - 1; i >= 0; i--)
            {
                double a, b;

                var token = _prn[i];
                switch (token.Type)
                {
                    case TokenType.PLUS:
                        a = workingStack.Pop();
                        b = workingStack.Pop();
                        workingStack.Push(a + b);
                        continue;
                    case TokenType.MINUS:
                        a = workingStack.Pop();
                        b = workingStack.Pop();
                        workingStack.Push(a - b);
                        continue;
                    case TokenType.STAR:
                        a = workingStack.Pop();
                        b = workingStack.Pop();
                        workingStack.Push(a * b);
                        continue;
                    case TokenType.SLASH:
                        a = workingStack.Pop();
                        b = workingStack.Pop();
                        workingStack.Push(a / b);
                        continue;
                    default:
                        workingStack.Push(double.Parse(_prn[i].Lexeme));
                        continue;
                }
            }
            return workingStack.Pop().ToString(CultureInfo.InvariantCulture);
        }
        catch (Exception e)
        {
            return e.ToString();
        }

        return string.Empty;
    }
}