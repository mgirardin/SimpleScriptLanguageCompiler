using System.Collections.Generic;
using SimpleScriptLanguageCompiler.Tokens;

namespace SimpleScriptLanguageCompiler.LexicalAnalysis {
    public class Scanner {
        private static HashSet<char> Separators => new() { ' ', '\t', '\r', '\n', '\v', '\f' };
        private static TokenProcessor Processor => new TokenProcessor(); // TODO: Thread safe

        public static IReadOnlyCollection<TokenIdentifier> Run(string content) {
            var identifiers = new List<(int, char, string)>();
            var tokens = new List<TokenIdentifier>();
            TokenIdentifier token;
            int? nextChar = 0;
            while (nextChar.HasValue) {
                if (Separators.Contains(content[nextChar.Value])) {
                    nextChar++;
                    if (content.Length < nextChar) break;
                    continue;
                }
                (token, nextChar) = Processor.ReadToken(content, nextChar.Value);
                token.Consts = identifiers;
                tokens.Add(token);
            }
            return tokens;
        }
    }
}
