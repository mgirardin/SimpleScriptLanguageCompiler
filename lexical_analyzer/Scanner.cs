using System.Collections.Generic;
using System.Linq;
using SimpleScriptLanguageCompiler.Tokens;

namespace SimpleScriptLanguageCompiler.LexicalAnalysis {
    public class Scanner {
        private static char[] Separators => new char[] { ' ', '\t', '\r', '\n', '\v', '\f' };

        public static IReadOnlyCollection<TokenIdentifier> Run(string content) {
            var identifiers = new List<(int, char, string)>();
            return content.Split(Separators)
                .Select(tokenSubset => {
                    var tokens = new List<TokenIdentifier>();
                    // TODO: Create non mocked TokenIdentifiers
                    var token = new TokenIdentifier {
                        Consts = identifiers
                    };
                    tokens.Add(token);
                    return tokens;
                })
                .SelectMany(x => x)
                .ToList();
        }
    }
}
