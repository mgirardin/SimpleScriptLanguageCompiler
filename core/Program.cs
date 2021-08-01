using System;
using System.IO;
using SimpleScriptLanguageCompiler.Common;
using SimpleScriptLanguageCompiler.LexicalAnalysis;

var fileContent = File.ReadAllText(args[0]);
Scanner.Run(fileContent)
    .ForEach(Console.WriteLine);