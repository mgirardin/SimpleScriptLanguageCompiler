using System.IO;
using SimpleScriptLanguageCompiler.LexicalAnalyzer;

var fileContent = File.ReadAllText(args[0]);
//TODO: Create .ForEach extension method in new project SimpleScriptLanguageCompiler.Common
var tokens = LexicalAnalyzer.Run(fileContent);
foreach (var token in tokens) {
    System.Console.WriteLine(token);
}
