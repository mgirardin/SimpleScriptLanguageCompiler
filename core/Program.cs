using System;
using System.IO;
using System.Linq;
using SimpleScriptLanguageCompiler.Common;
using SimpleScriptLanguageCompiler.LexicalAnalysis;

var fileContent = File.ReadAllText(args[0]);

Scanner.Run(fileContent)
    .Select(token => token)
    .ForEach(token => {
        Console.Write(token.Token.ToString());
        if (token.SecondaryToken.HasValue)
            Console.WriteLine($" - {token.SecondaryToken}");
        else
            Console.WriteLine();
    });