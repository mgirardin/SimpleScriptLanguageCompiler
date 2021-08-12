using System.Collections.Generic;
using SimpleScriptLanguageCompiler.Common;
using SimpleScriptLanguageCompiler.Tokens;

namespace SimpleScriptLanguageCompiler.LexicalAnalysis {
    public class Scanner {
        private static char[] Separators => new char[] { ' ', '\t', '\r', '\n', '\v', '\f' };
        private static TokenProcessor Processor => new TokenProcessor(); // TODO: Thread safe

        public static IReadOnlyCollection<TokenIdentifier> Run(string content) {
            var identifiers = new List<(int, char, string)>();
            var tokens = new List<TokenIdentifier>();
            content.Split(Separators)
                .ForEach(tokenSubset => {
                    TokenIdentifier token;
                    int? nextChar = 0;
                    while (nextChar.HasValue) {
                        (token, nextChar) = Processor.ReadToken(tokenSubset, nextChar.Value);
                        token.Consts = identifiers;
                        tokens.Add(token);
                    }
                });
            return tokens;
        }
    }
}
