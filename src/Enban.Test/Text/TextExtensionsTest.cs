using Enban.Text;
using Xunit;

namespace Enban.Test.Text
{
    public class TextExtensionsTest
    {
        [Fact]
        public void RemoveWhitespaces_GivenTextWithoutWhitespaces_ThenNotCharRemoved()
        {
            TestRemoveWhitespaces("Foobar","Foobar");
        }

        [Fact]
        public void RemoveWhitespaces_GivenTextWithSingleWhitespace_ThenWhitespaceRemoved()
        {
            TestRemoveWhitespaces("Foo bar", "Foobar");
        }

        [Fact]
        public void RemoveWhitespaces_GivenTextWithConsecutiveWhitespaces_ThenWhitespacesRemoved()
        {
            TestRemoveWhitespaces("Foo   bar", "Foobar");
        }

        [Fact]
        public void RemoveWhitespaces_GivenTextWithWhitespacesAtBeginning_ThenWhitespacesRemoved()
        {
            TestRemoveWhitespaces("   Foobar", "Foobar");
        }

        [Fact]
        public void RemoveWhitespaces_GivenTextWithWhitespacesAtEnd_ThenWhitespacesRemoved()
        {
            TestRemoveWhitespaces("Foobar    ", "Foobar");
        }

        [Fact]
        public void RemoveWhitespaces_GivenTextWithWhitespacesOnly_ThenEmptyText()
        {
            TestRemoveWhitespaces("    ", "");
        }

        [Fact]
        public void RemoveWhitespaces_GivenEmptyText_ThenEmptyText()
        {
            TestRemoveWhitespaces("", "");
        }

        private void TestRemoveWhitespaces(string t, string expected)
        {
            var actual = new string(t.ToCharArray().RemoveWhitespaces());
            Assert.Equal(expected, actual);
        }
    }
}
