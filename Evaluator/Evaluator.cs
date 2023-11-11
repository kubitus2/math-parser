using math_parser.Scanner;

namespace math_parser.Evaluator;

public class Evaluator
{
    private readonly Stack<Token> _stack;

    public Evaluator(Stack<Token> stack)
    {
        _stack = stack;
    }

    public string Evaluate()
    {
        return "";
    }
}