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
    }
}