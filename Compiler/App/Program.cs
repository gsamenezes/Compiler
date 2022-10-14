using Compiler;
using Compiler.Syntactic;

var fileContent = File.ReadAllText(args[0]);

Scanner.Run(fileContent)
       .Parse();