using System.IO;
using SimpleScriptLanguageCompiler.Common;
using SimpleScriptLanguageCompiler.LexicalAnalyzer;

var fileContent = File.ReadAllText(args[0]);
LexicalAnalyzer.Run(fileContent)
    .ForEach(token => {
        System.Console.WriteLine(token);
    });