namespace SimpleScriptLanguageCompiler.Tokens {
    public enum TokenEnum {
        // Reserved identifier
        ARRAY,
        BOOLEAN,
        BREAK,
        CHAR,
        CONTINUE,
        DO,
        ELSE,
        FALSE,
        FUNCTION,
        IF,
        INTEGER,
        OF,
        RETURN,
        STRING,
        STRUCT,
        TRUE,
        TYPE,
        VAR,
        WHILE,

        // Symbols
        COLON,
        SEMI_COLON,
        COMMA,
        EQUALS,
        LEFT_SQUARE,
        RIGHT_SQUARE,
        LEFT_BRACES,
        RIGHT_BRACES,
        LEFT_PARENTHESIS,
        RIGHT_PARENTHESIS,
        AND,
        OR,
        LESS_THAN,
        GREATER_THAN,
        LESS_OR_EQUAL,
        GREATER_OR_EQUAL,
        NOT_EQUAL,
        EQUAL_EQUAL,
        PLUS,
        PLUS_PLUS,
        MINUS,
        MINUS_MINUS,
        TIMES,
        DIVIDE,
        DOT,
        NOT,

        // Regular Tokens
        CHARACTER,
        NUMERAL,
        STRINGVAL,
        ID,
        UNKNOWN,
        ENDFILE
    }

    public enum SyntacticTokens {
        ACCEPT = 63,
        END,
        P,
        LDE,
        DE,
        DF,
        DT,
        DC,
        LI,
        DV,
        LP,
        B,
        LDV,
        LS,
        S,
        E,
        L,
        R,
        K,
        F,
        LE,
        LV,
        T,
        TRU,
        FALS,
        CHR,
        STR,
        NUM,
        IDD,
        IDU,
        IDR,
        NB,
        MF,
        MC,
        NF,
        MT,
        ME,
        MW,
        MA
    }
}
