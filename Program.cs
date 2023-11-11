using math_parser.Evaluator;
using math_parser.Parser;
using math_parser.Scanner;

RunPrompt();

void RunPrompt()
{
    for (;;)
    {
        Console.Write("> ");
        var line = Console.ReadLine();
        if (string.IsNullOrEmpty(line)) break;
        
        Run(line);


    }
}

void Run(string line)
{
    var scanner = new Scanner(line);
    var tokens = scanner.ScanTokens();
    foreach (var token in tokens)
    {
        Console.WriteLine(token.ToString());
    }
    
    Console.WriteLine("Parsing");

    var parser = new Parser(tokens);
    var rpn = parser.GenerateRPN();

    var eval = new Evaluator(rpn);
    
}
