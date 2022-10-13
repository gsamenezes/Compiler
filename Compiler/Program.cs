using Compiler;

var file = File.OpenRead("./Program.txt");
var analyzer = new LexicalAnalyzer(file);
var isGood = analyzer.Analyze();
if (!isGood) {
    Console.WriteLine("There are {0} errors", analyzer.ErrorsCount);
    return;
}

Console.WriteLine("The syntax is fine");