using System;
using System.IO;

namespace SimpleScriptLanguageCompiler.Core {
    class Program {
        static void Main(string[] args) {
            var fileContent = File.ReadAllText(args[0]);
            Console.WriteLine(fileContent);
        }
    }
}
