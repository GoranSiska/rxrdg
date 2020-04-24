using System;
using NUnit.Framework;
using RegularExpressionDataGenerator;
using System.Linq;
using System.Collections.Generic;
namespace RxrdgTests
{
    [TestFixture]
    public class LexerTests
    {
        private readonly NodeBuilder nodeBuilder = new NodeBuilder(); 
        #region Literal state

        [Test]
        public void LiteralStateTokenizeParenthesisLeft()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("(");
            var expected = new ParenthesisLeftToken();
            Assert.AreEqual(tokens.First(), expected);
        }

        [Test]
        public void LiteralStateTokenizeParenthesisRight()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize(")");
            var expected = new ParenthesisRightToken();
            Assert.AreEqual(tokens.First(), expected);
        }

        [Test]
        public void LiteralStateTokenizeBracketLeft()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("[");
            var expected = new BracketLeftToken();
            Assert.AreEqual(tokens.First(), expected);
        }

        [Test]
        public void LiteralStateTokenizeAny()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize(".");
            var expected = new AnyToken();
            Assert.AreEqual(tokens.First(), expected);
        }

        [Test]
        public void LiteralStateTokenizeZeroOrMore()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("*");
            var expected = new RepetitionToken {MinOccurs = 0, MaxOccurs = -1};
            Assert.AreEqual(tokens.First(), expected);
        }

        [Test]
        public void LiteralStateTokenizeZeroOrOne()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("?");
            var expected = new RepetitionToken { MinOccurs = 0, MaxOccurs = 1 };
            Assert.AreEqual(tokens.First(), expected);
        }

        [Test]
        public void LiteralStateTokenizeOneOrMore()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("+");
            var expected = new RepetitionToken { MinOccurs = 1, MaxOccurs = -1 };
            Assert.AreEqual(tokens.First(), expected);
        }

        [Test]
        public void LiteralStateTokenizeAlteration()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("|");
            var expected = new AlternationToken();
            Assert.AreEqual(tokens.First(), expected);
        }

        [Test]
        public void LiteralStateTokenizeLiterals()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("abc");
            var expected = new List<IToken>
                                     {
                                         new LiteralToken {Character = 'a'}
                                         , new LiteralToken {Character = 'b'}
                                         ,  new LiteralToken {Character = 'c'}
                                     };

            Assert.IsTrue(expected.SequenceEqual(tokens));
        }

        #endregion

        #region Begin Set state

        [Test]
        public void BeginSetStateTokenizeNot()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("[^");
            var expected = new NotToken();
            Assert.AreEqual(tokens.Last(), expected);
        }

        #endregion

        #region Set state

        [Test]
        public void SetStateTokenizeBracketRight()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("[a]");
            var expected = new BracketRightToken();
            Assert.AreEqual(tokens.Last(), expected);
        }

        [Test]
        public void SetStateTokenizeRange()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("[a-");
            var expected = new RangeToken();
            Assert.AreEqual(tokens.Last(), expected);
        }

        [Test]
        public void SetStateTokenizeLiteral()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("[e");
            var expected = new LiteralToken {Character = 'e'};
            Assert.AreEqual(tokens.Last(), expected);
        }

        #endregion

        #region Escape state

        [Test]
        public void EscapeStateTokenizeNumeric()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("\\d");
            var expected = new NumericToken();
            Assert.AreEqual(tokens.Last(), expected);
        }

        [Test]
        public void EscapeStateTokenizeWord()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("\\w");
            var expected = new WordToken();
            Assert.AreEqual(tokens.Last(), expected);
        }

        [Test]
        public void EscapeStateTokenizeWhitespace()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("\\s");
            var expected = new WhitespaceToken();
            Assert.AreEqual(tokens.Last(), expected);
        }

        [Test]
        public void EscapeStateTokenizeNonNumeric()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("\\D");
            var expected = new NonNumericToken();
            Assert.AreEqual(tokens.Last(), expected);
        }

        [Test]
        public void EscapeStateTokenizeNonWord()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("\\W");
            var expected = new NonWordToken();
            Assert.AreEqual(tokens.Last(), expected);
        }

        [Test]
        public void EscapeStateTokenizeNonWhitespace()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("\\S");
            var expected = new NonWhitespaceToken();
            Assert.AreEqual(tokens.Last(), expected);
        }

        [Test]
        public void EscapeStateTokenizeLiteral()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("\\z");
            var expected = new LiteralToken { Character = 'z' };
            Assert.AreEqual(tokens.Last(), expected);
        }

        #endregion

        #region Repetition state

        [Test]
        public void RepetitionStateTokenizeMinOnly()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("{2,}");
            var expected = new RepetitionToken {MinOccurs = 2, MaxOccurs = -1};
            Assert.AreEqual(tokens.First(), expected);
        }

        [Test]
        public void RepetitionStateTokenizeMinMax()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("{3,5}");
            var expected = new RepetitionToken { MinOccurs = 3, MaxOccurs = 5 };
            Assert.AreEqual(tokens.First(), expected);
        }

        [Test]
        public void RepetitionStateTokenizeShort()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            var tokens = lexer.Tokenize("{7}");
            var expected = new RepetitionToken { MinOccurs = 7, MaxOccurs = 7 };
            Assert.AreEqual(tokens.First(), expected);
        }

        #endregion

        #region Repetition state errors

        [Test]
        public void RepetitionStateTokenizeMissingMin()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            Assert.Throws<ArgumentException>(() => lexer.Tokenize("{,5}").ToList());
        }

        [Test]
        public void RepetitionStateTokenizeToManyCommas()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            Assert.Throws<ArgumentException>(() => lexer.Tokenize("{1,,5}").ToList());
        }

        [Test]
        public void RepetitionStateTokenizeMissingValues()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            Assert.Throws<ArgumentException>(() => lexer.Tokenize("{}").ToList());
        }

        [Test]
        public void RepetitionStateTokenizeInvalid()
        {
            var lexer = new Lexer(nodeBuilder.TokenBuilder);
            Assert.Throws<ArgumentException>(() => lexer.Tokenize("{1,a}").ToList());
        }

        #endregion
    }
}
