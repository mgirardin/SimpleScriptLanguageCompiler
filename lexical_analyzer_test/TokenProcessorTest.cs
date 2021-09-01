using System.Linq;
using NUnit.Framework;
using SimpleScriptLanguageCompiler.Tokens;

namespace SimpleScriptLanguageCompiler.LexicalAnalysis.Test {
    [Parallelizable(ParallelScope.Self)]
    public class TokenProcessorTest {
        [Test]
        [TestCase(TokenEnum.ARRAY, "array")]
        [TestCase(TokenEnum.BOOLEAN, "boolean")]
        [TestCase(TokenEnum.BREAK, "break")]
        [TestCase(TokenEnum.CHAR, "char")]
        [TestCase(TokenEnum.CONTINUE, "continue")]
        [TestCase(TokenEnum.DO, "do")]
        [TestCase(TokenEnum.ELSE, "else")]
        [TestCase(TokenEnum.FALSE, "false")]
        [TestCase(TokenEnum.FUNCTION, "function")]
        [TestCase(TokenEnum.IF, "if")]
        [TestCase(TokenEnum.INTEGER, "integer")]
        [TestCase(TokenEnum.OF, "of")]
        [TestCase(TokenEnum.RETURN, "return")]
        [TestCase(TokenEnum.STRING, "string")]
        [TestCase(TokenEnum.STRUCT, "struct")]
        [TestCase(TokenEnum.TRUE, "true")]
        [TestCase(TokenEnum.TYPE, "type")]
        [TestCase(TokenEnum.VAR, "var")]
        [TestCase(TokenEnum.WHILE, "while")]
        public void ShouldIdentifyKeyWords(TokenEnum token, string content) {
            var tokens = Scanner.Run(content);

            Assert.AreEqual(1, tokens.Count);
            Assert.AreEqual(token, tokens.First().Token);
        }

        [Test]
        [TestCase(TokenEnum.NUMERAL, "1")]
        [TestCase(TokenEnum.NUMERAL, "123456789")]
        [TestCase(TokenEnum.NUMERAL, "123.456")]
        [TestCase(TokenEnum.NUMERAL, "12345.6789")]
        [TestCase(TokenEnum.NUMERAL, "1.23456789")]
        public void ShouldIdentifyNumerals(TokenEnum token, string content) {
            var tokens = Scanner.Run(content);

            Assert.AreEqual(1, tokens.Count);
            Assert.AreEqual(token, tokens.First().Token);
        }

        [Test]
        [TestCase(TokenEnum.NUMERAL, "1..1234")]
        [TestCase(TokenEnum.NUMERAL, "12.23.23")]
        [TestCase(TokenEnum.NUMERAL, ".456")]
        public void ShouldNotIdentifyNumerals(TokenEnum token, string content) {
            var tokens = Scanner.Run(content);
            var check = (tokens.Count != 1 || tokens.First().Token == token);
            Assert.IsTrue(check);
        }

        [Test]
        [TestCase(TokenEnum.COMMA, ",")]
        [TestCase(TokenEnum.DOT, ".")]
        [TestCase(TokenEnum.COLON, ":")]
        [TestCase(TokenEnum.SEMI_COLON, ";")]
        [TestCase(TokenEnum.LEFT_SQUARE, "[")]
        [TestCase(TokenEnum.RIGHT_SQUARE, "]")]
        [TestCase(TokenEnum.LEFT_BRACES, "{")]
        [TestCase(TokenEnum.RIGHT_BRACES, "}")]
        [TestCase(TokenEnum.TIMES, "*")]
        [TestCase(TokenEnum.LEFT_PARENTHESIS, "(")]
        [TestCase(TokenEnum.RIGHT_PARENTHESIS, ")")]
        [TestCase(TokenEnum.DIVIDE, "/")]
        public void ShouldIdentifyRegularTokens(TokenEnum token, string content) {
            var tokens = Scanner.Run(content);

            Assert.AreEqual(1, tokens.Count);
            Assert.AreEqual(token, tokens.First().Token);
        }

        [Test]
        [TestCase(TokenEnum.CHARACTER, "'a'")]
        [TestCase(TokenEnum.CHARACTER, "'A'")]
        [TestCase(TokenEnum.CHARACTER, "'z'")]
        [TestCase(TokenEnum.CHARACTER, "'Z'")]
        [TestCase(TokenEnum.CHARACTER, "'0'")]
        [TestCase(TokenEnum.CHARACTER, "'9'")]
        [TestCase(TokenEnum.CHARACTER, "'*'")]
        [TestCase(TokenEnum.CHARACTER, "'&'")]
        public void ShouldIdentifyCharacters(TokenEnum token, string content) {
            var tokens = Scanner.Run(content);

            Assert.AreEqual(1, tokens.Count);
            Assert.AreEqual(token, tokens.First().Token);
        }

        [Test]
        [TestCase(TokenEnum.STRINGVAL, "\"a\"")]
        [TestCase(TokenEnum.STRINGVAL, "\"A\"")]
        [TestCase(TokenEnum.STRINGVAL, "\"Ab\"")]
        [TestCase(TokenEnum.STRINGVAL, "\"A2\"")]
        [TestCase(TokenEnum.STRINGVAL, "\"A 2\"")]
        [TestCase(TokenEnum.STRINGVAL, "\"A simple string, but not so much.\n\"")]
        public void ShouldIdentifyStrings(TokenEnum token, string content) {
            var tokens = Scanner.Run(content);

            Assert.AreEqual(1, tokens.Count);
            Assert.AreEqual(token, tokens.First().Token);
        }

        [Test]
        public void ShouldIdentifyStringDeclaration() {
            var tokens = Scanner.Run("var variable: string = \"Simple String\"");

            Assert.AreEqual(6, tokens.Count());
            Assert.AreEqual(TokenEnum.VAR, tokens.First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(1).First().Token);
            Assert.AreEqual(TokenEnum.COLON, tokens.Skip(2).First().Token);
            Assert.AreEqual(TokenEnum.STRING, tokens.Skip(3).First().Token);
            Assert.AreEqual(TokenEnum.EQUALS, tokens.Skip(4).First().Token);
            Assert.AreEqual(TokenEnum.STRINGVAL, tokens.Skip(5).First().Token);
        }

        [Test]
        public void ShouldIdentifyIntegerDeclaration() {
            var tokens = Scanner.Run("var variavel: integer = 50");

            Assert.AreEqual(6, tokens.Count());
            Assert.AreEqual(TokenEnum.VAR, tokens.First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(1).First().Token);
            Assert.AreEqual(TokenEnum.COLON, tokens.Skip(2).First().Token);
            Assert.AreEqual(TokenEnum.INTEGER, tokens.Skip(3).First().Token);
            Assert.AreEqual(TokenEnum.EQUALS, tokens.Skip(4).First().Token);
            Assert.AreEqual(TokenEnum.NUMERAL, tokens.Skip(5).First().Token);
        }

        [Test]
        public void ShouldIdentifyFunction() {
            var tokens = Scanner.Run("function sqrt(n: integer) : integer { return 10 }");

            Assert.AreEqual(13, tokens.Count());
            Assert.AreEqual(TokenEnum.FUNCTION, tokens.First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(1).First().Token);
            Assert.AreEqual(TokenEnum.LEFT_PARENTHESIS, tokens.Skip(2).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(3).First().Token);
            Assert.AreEqual(TokenEnum.COLON, tokens.Skip(4).First().Token);
            Assert.AreEqual(TokenEnum.INTEGER, tokens.Skip(5).First().Token);
            Assert.AreEqual(TokenEnum.RIGHT_PARENTHESIS, tokens.Skip(6).First().Token);
            Assert.AreEqual(TokenEnum.COLON, tokens.Skip(7).First().Token);
            Assert.AreEqual(TokenEnum.INTEGER, tokens.Skip(8).First().Token);
            Assert.AreEqual(TokenEnum.LEFT_BRACES, tokens.Skip(9).First().Token);
            Assert.AreEqual(TokenEnum.RETURN, tokens.Skip(10).First().Token);
            Assert.AreEqual(TokenEnum.NUMERAL, tokens.Skip(11).First().Token);
            Assert.AreEqual(TokenEnum.RIGHT_BRACES, tokens.Skip(12).First().Token);
        }

        [Test]
        public void ShouldIdentifyCompleteFunction() {
            var tokens = Scanner.Run(@"
                function sqrt(n: integer) : integer {
                    var diff, media, aux, min, max: integer;
                    do {
                        media = max + min;
                        media = media / 2;
                        aux = media * media;
                        if (aux > n) {
                            max = media;
                        } else {
                            min = media;
                        }
                        diff = max - min;
                    } while (diff > 1);
                    return media;
                }"
            );

            Assert.AreEqual(80, tokens.Count());
            // function sqrt(n: integer) : integer {
            Assert.AreEqual(TokenEnum.FUNCTION, tokens.First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(1).First().Token);
            Assert.AreEqual(TokenEnum.LEFT_PARENTHESIS, tokens.Skip(2).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(3).First().Token);
            Assert.AreEqual(TokenEnum.COLON, tokens.Skip(4).First().Token);
            Assert.AreEqual(TokenEnum.INTEGER, tokens.Skip(5).First().Token);
            Assert.AreEqual(TokenEnum.RIGHT_PARENTHESIS, tokens.Skip(6).First().Token);
            Assert.AreEqual(TokenEnum.COLON, tokens.Skip(7).First().Token);
            Assert.AreEqual(TokenEnum.INTEGER, tokens.Skip(8).First().Token);
            Assert.AreEqual(TokenEnum.LEFT_BRACES, tokens.Skip(9).First().Token);
            // var diff, media, aux, min, max: integer;
            Assert.AreEqual(TokenEnum.VAR, tokens.Skip(10).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(11).First().Token);
            Assert.AreEqual(TokenEnum.COMMA, tokens.Skip(12).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(13).First().Token);
            Assert.AreEqual(TokenEnum.COMMA, tokens.Skip(14).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(15).First().Token);
            Assert.AreEqual(TokenEnum.COMMA, tokens.Skip(16).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(17).First().Token);
            Assert.AreEqual(TokenEnum.COMMA, tokens.Skip(18).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(19).First().Token);
            Assert.AreEqual(TokenEnum.COLON, tokens.Skip(20).First().Token);
            Assert.AreEqual(TokenEnum.INTEGER, tokens.Skip(21).First().Token);
            Assert.AreEqual(TokenEnum.SEMI_COLON, tokens.Skip(22).First().Token);
            // do {
            Assert.AreEqual(TokenEnum.DO, tokens.Skip(23).First().Token);
            Assert.AreEqual(TokenEnum.LEFT_BRACES, tokens.Skip(24).First().Token);
            // media = max + min;
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(25).First().Token);
            Assert.AreEqual(TokenEnum.EQUALS, tokens.Skip(26).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(27).First().Token);
            Assert.AreEqual(TokenEnum.PLUS, tokens.Skip(28).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(29).First().Token);
            Assert.AreEqual(TokenEnum.SEMI_COLON, tokens.Skip(30).First().Token);
            // media = media / 2;
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(31).First().Token);
            Assert.AreEqual(TokenEnum.EQUALS, tokens.Skip(32).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(33).First().Token);
            Assert.AreEqual(TokenEnum.DIVIDE, tokens.Skip(34).First().Token);
            Assert.AreEqual(TokenEnum.NUMERAL, tokens.Skip(35).First().Token);
            Assert.AreEqual(TokenEnum.SEMI_COLON, tokens.Skip(36).First().Token);
            // aux = media * media;
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(37).First().Token);
            Assert.AreEqual(TokenEnum.EQUALS, tokens.Skip(38).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(39).First().Token);
            Assert.AreEqual(TokenEnum.TIMES, tokens.Skip(40).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(41).First().Token);
            Assert.AreEqual(TokenEnum.SEMI_COLON, tokens.Skip(42).First().Token);
            // if (aux > n) {
            Assert.AreEqual(TokenEnum.IF, tokens.Skip(43).First().Token);
            Assert.AreEqual(TokenEnum.LEFT_PARENTHESIS, tokens.Skip(44).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(45).First().Token);
            Assert.AreEqual(TokenEnum.GREATER_THAN, tokens.Skip(46).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(47).First().Token);
            Assert.AreEqual(TokenEnum.RIGHT_PARENTHESIS, tokens.Skip(48).First().Token);
            Assert.AreEqual(TokenEnum.LEFT_BRACES, tokens.Skip(49).First().Token);
            // max = media;
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(50).First().Token);
            Assert.AreEqual(TokenEnum.EQUALS, tokens.Skip(51).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(52).First().Token);
            Assert.AreEqual(TokenEnum.SEMI_COLON, tokens.Skip(53).First().Token);
            // } else {
            Assert.AreEqual(TokenEnum.RIGHT_BRACES, tokens.Skip(54).First().Token);
            Assert.AreEqual(TokenEnum.ELSE, tokens.Skip(55).First().Token);
            Assert.AreEqual(TokenEnum.LEFT_BRACES, tokens.Skip(56).First().Token);
            // min = media;
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(57).First().Token);
            Assert.AreEqual(TokenEnum.EQUALS, tokens.Skip(58).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(59).First().Token);
            Assert.AreEqual(TokenEnum.SEMI_COLON, tokens.Skip(60).First().Token);
            // }
            Assert.AreEqual(TokenEnum.RIGHT_BRACES, tokens.Skip(61).First().Token);
            // diff = max - min;
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(62).First().Token);
            Assert.AreEqual(TokenEnum.EQUALS, tokens.Skip(63).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(64).First().Token);
            Assert.AreEqual(TokenEnum.MINUS, tokens.Skip(65).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(66).First().Token);
            Assert.AreEqual(TokenEnum.SEMI_COLON, tokens.Skip(67).First().Token);
            // } while (diff > 1);
            Assert.AreEqual(TokenEnum.RIGHT_BRACES, tokens.Skip(68).First().Token);
            Assert.AreEqual(TokenEnum.WHILE, tokens.Skip(69).First().Token);
            Assert.AreEqual(TokenEnum.LEFT_PARENTHESIS, tokens.Skip(70).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(71).First().Token);
            Assert.AreEqual(TokenEnum.GREATER_THAN, tokens.Skip(72).First().Token);
            Assert.AreEqual(TokenEnum.NUMERAL, tokens.Skip(73).First().Token);
            Assert.AreEqual(TokenEnum.RIGHT_PARENTHESIS, tokens.Skip(74).First().Token);
            Assert.AreEqual(TokenEnum.SEMI_COLON, tokens.Skip(75).First().Token);
            // return media;
            Assert.AreEqual(TokenEnum.RETURN, tokens.Skip(76).First().Token);
            Assert.AreEqual(TokenEnum.ID, tokens.Skip(77).First().Token);
            Assert.AreEqual(TokenEnum.SEMI_COLON, tokens.Skip(78).First().Token);
            // }
            Assert.AreEqual(TokenEnum.RIGHT_BRACES, tokens.Skip(79).First().Token);
        }
    }
}