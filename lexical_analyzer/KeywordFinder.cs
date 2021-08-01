using System.Collections.Generic;
using SimpleScriptLanguageCompiler.Tokens;

namespace SimpleScriptLanguageCompiler.LexicalAnalysis {
    public static class KeywordFinder {
        private static Dictionary<string, TokenEnum> Mapper => new() {
            { "array", TokenEnum.ARRAY },
            { "boolean", TokenEnum.BOOLEAN },
            { "break", TokenEnum.BREAK },
            { "char", TokenEnum.CHAR },
            { "continue", TokenEnum.CONTINUE },
            { "do", TokenEnum.DO },
            { "else", TokenEnum.ELSE },
            { "false", TokenEnum.FALSE },
            { "function", TokenEnum.FUNCTION },
            { "if", TokenEnum.IF },
            { "integer", TokenEnum.INTEGER },
            { "of", TokenEnum.OF },
            { "string", TokenEnum.STRING },
            { "struct", TokenEnum.STRUCT },
            { "true", TokenEnum.TRUE },
            { "type", TokenEnum.TYPE },
            { "var", TokenEnum.VAR },
            { "while", TokenEnum.WHILE }
        };

        public static TokenEnum Find(string lexeme) {
            if (!Mapper.ContainsKey(lexeme)) return TokenEnum.ID;
            return Mapper[lexeme];
        }
    }
}