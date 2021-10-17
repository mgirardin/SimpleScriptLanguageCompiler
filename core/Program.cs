using System.IO;
using SimpleScriptLanguageCompiler.LexicalAnalysis;
using SimpleScriptLanguageCompiler.SyntacticAnalysis;

var fileContent = File.ReadAllText(args[0]);

Scanner.Run(fileContent)
    .Parse();