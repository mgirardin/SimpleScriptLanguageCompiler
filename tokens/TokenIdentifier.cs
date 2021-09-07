using System.Collections.Generic;

namespace SimpleScriptLanguageCompiler.Tokens {
    public class TokenIdentifier {
        public TokenEnum Token { get; init; }
        public int? SecondaryToken { get; init; }
        /// <summary>
        /// Values of regular tokens <seealso cref="TokenEnum"/>
        /// </summary>
        public IReadOnlyCollection<(int, char, string)> Consts { get; set; }
    }
}

namespace System.Runtime.CompilerServices {
    internal static class IsExternalInit { }
}