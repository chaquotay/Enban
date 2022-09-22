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

        [Theory]
        [InlineData("Foobar", true, true, true, "Foobar")]
        [InlineData("", true, true, true, "")]
        [InlineData("", false, false, false, "")]
        [InlineData("   Foo bar  ", true, true, true, "Foobar")]
        [InlineData("   Foo bar  ", false, true, true, "   Foobar")]
        [InlineData("   Foo bar  ", true, false, true, "Foo bar")]
        [InlineData("   Foo bar  ", true, true, false, "Foobar  ")]
        
        [InlineData("   Foo bar  ", true, false, false, "Foo bar  ")]
        [InlineData("   Foo bar  ", false, true, false, "   Foobar  ")]
        [InlineData("   Foo bar  ", false, false, true, "   Foo bar")]
        public void TestTestRemoveWhitespaces(string t, bool start, bool middle, bool end, string expected)
        {
            var actual = new string(t.ToTrimmedCharArray(start, middle, end));
            Assert.Equal(expected, actual);
        }

        private void TestRemoveWhitespaces(string t, string expected)
        {
            TestRemoveWhitespaces(t, expected, true, true, true);
        }
        
        private void TestRemoveWhitespaces(string t, string expected, bool start, bool middle, bool end)
        {
            var actual = new string(t.ToCharArray().Trim());
            Assert.Equal(expected, actual);
        }
    }
}
