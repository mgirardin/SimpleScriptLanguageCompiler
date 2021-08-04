using System.Collections.Generic;
using SimpleScriptLanguageCompiler.Common;
using SimpleScriptLanguageCompiler.Tokens;

namespace SimpleScriptLanguageCompiler.LexicalAnalysis {
    public class Scanner {
        private static char[] Separators => new char[] { ' ', '\t', '\r', '\n', '\v', '\f' };

        public static IReadOnlyCollection<TokenIdentifier> Run(string content) {
            var identifiers = new List<(int, char, string)>();
            var tokens = new List<TokenIdentifier>();
            content.Split(Separators)
                .ForEach(tokenSubset => {
                    // TODO: Create non mocked TokenIdentifiers
                    var token = new TokenIdentifier {
                        Consts = identifiers
                    };
                    tokens.Add(token);
                });
            return tokens;
        }


    }
}
