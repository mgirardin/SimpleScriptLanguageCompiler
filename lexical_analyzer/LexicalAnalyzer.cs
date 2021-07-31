using System.Collections.Generic;

namespace SimpleScriptLanguageCompiler.LexicalAnalysis {
    public class LexicalAnalyzer {
        // TODO: return tokens
        public static IReadOnlyCollection<string> Run(string content) => content.Split(' ');
    }
}
