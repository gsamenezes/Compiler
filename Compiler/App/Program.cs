using Compiler;
using Compiler.Semantic;
using Compiler.Syntactic;

var fileContent = File.ReadAllText(args[0]);

Scanner.Run(fileContent)
       .Parse();

await File.WriteAllTextAsync("Code.txt", AttributeGrammar.GeneratedCode);
    