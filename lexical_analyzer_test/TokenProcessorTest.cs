using System.Linq;
using NUnit.Framework;
using SimpleScriptLanguageCompiler.Tokens;

namespace SimpleScriptLanguageCompiler.LexicalAnalysis.Test {
    public class Tests {
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
        public void ShouldIdentifyReservedIdentifiers(TokenEnum token, string content) {
            var tokens = Scanner.Run(content);

            Assert.AreEqual(1, tokens.Count);
            Assert.AreEqual(token, tokens.First().Token);
        }
    }
}