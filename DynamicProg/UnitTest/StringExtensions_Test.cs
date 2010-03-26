using System;
using DynamicProg.Extensions;
using NUnit.Framework;
using LaTrompa.Extensions;

namespace UnitTest
{
    [TestFixture]
    public class StringExtensions_Test
    {
        private const string STRING_TO_CHANGE = "this is the string to capitalize";

        [Test]
        public void Calling_Capitalize_should_return_the_first_letter_of_a_string_uppercase_and_the_rest_lowercase()
        {
            var result = STRING_TO_CHANGE.Capitalize();
            var control = "This is the string to capitalize";

            Assert.AreEqual(control, result);
        }

        [Test]
        public void Remove_given_numbers_of_characters_from_the_end_of_a_string()
        {
            var result = STRING_TO_CHANGE.TruncateLast(4);
            var control = "this is the string to capita";

            Assert.AreEqual(control, result);
        }

        [Test]
        public void Remove_characters_from_the_end_of_a_string_from_a_given_starting_char()
        {
            var result = STRING_TO_CHANGE.TruncateFrom("o");
            var control = "this is the string t";

            Assert.AreEqual(control, result);
        }
        
        [Test]
        public void Remove_given_numbers_of_characters_from_the_beginning_of_a_string()
        {
            var result = STRING_TO_CHANGE.Clip(4);
            var control = " is the string to capitalize";

            Assert.AreEqual(control, result);
        }

        [Test]
        public void Remove_characters_from_the_beginning_of_a_string_up_to_a_given_starting_char()
        {
            var result = STRING_TO_CHANGE.Clip("o");
            var control = "o capitalize";

            Assert.AreEqual(control, result);
        }

        [Test]
        public void Returns_the_middle_of_a_string_between_to_tokens()
        {
            var result = STRING_TO_CHANGE.Mid(" is", "to");
            var control = " the string ";

            Assert.AreEqual(control, result);
        }

        [Test]
        public void Replace_br_tags_with_NewLines()
        {
            var result = "This is <br> full of <BR/> tags <bR/>".HtmlBRToNewLine();
            Assert.That(result.ToLower().IndexOf("br") == -1);
            Assert.That(result.ToLower().IndexOf(Environment.NewLine) > -1);
        }

        [Test]
        public void Replace_NewLines_with_br_tags()
        {
            var result = "This is \r\n full of " + Environment.NewLine + " tags " + Environment.NewLine + " ";
            Assert.That(result.NewLineToHtmlBR().ToLower().IndexOf("br") > -1);
            Assert.That(result.NewLineToHtmlBR().ToLower().IndexOf(Environment.NewLine) == -1);
        }

        [Test]
        public void Remove_extra_white_space_in_a_string()
        {
            var result = "This is   a   lot of white     space  in here  .    ".Squeeze();
            var control = "This is a lot of white space in here.";
            Assert.AreEqual(control, result);
        }

        [Test]
        public void Finds_the_if_a_string_starts_with_one_of_many_items_in_an_array()
        {
            var a = new []{"This","is","an","array"};

            Assert.IsTrue(STRING_TO_CHANGE.StartsWith(a));
            Assert.IsFalse(STRING_TO_CHANGE.StartsWith(new []{"not","in","string"}));
        }

        [Test]
        public void Cut_down_a_string_to_a_given_length_and_add_three_points()
        {
            var result = STRING_TO_CHANGE.FitInto(10);
            var control = "this is...";
            Assert.AreEqual(control, result);
        }

        [Test]
        public void Removes_non_alphanumeric_characters()
        {
            var result = "Lots of -)(&*^%$%#54!@ characters".ToAlphaNumeric();
            var control = "Lots of 54 characters";
            Assert.AreEqual(control, result);
        }

        [Test]
        public void Removes_any_whitespace_in_a_string()
        {
            var result = STRING_TO_CHANGE.StripWhitespace();
            var control = "thisisthestringtocapitalize";
            Assert.AreEqual(control,result);
        }

        [Test]
        public void Returns_an_array_with_the_words_in_a_phrase()
        {
            var result = STRING_TO_CHANGE.ToWords();
            var control = new[] {"this","is","the","string","to","capitalize"};

            Assert.AreEqual(control,result);
        }

        [Test]
        public void Encode_html_entities()
        {
            var result = "this is & and ? make some <entities/> more easy to !@".HtmlEncode();
            var control = "this is &amp; and ? make some &lt;entities/&gt; more easy to !@";

            Assert.AreEqual(control,result);
        }

        [Test]
        public void Decode_html_entities_into_html()
        {
            var result = "this is &amp; and ? make some &lt;entities/&gt; more easy to !@".HtmlDecode();
            var control = "this is & and ? make some <entities/> more easy to !@";

            Assert.AreEqual(control, result);
        }

        [Test]
        public void Removes_html_tags()
        {
            var result = "<br>A breakline<br/><p>Paragraph<P/><HEAD><Title>The title</TitLe>".StripsHtml();
            var control = "A breaklineParagraphThe title";
            Assert.AreEqual(control,result);
        }

        [Test]
        public void Replaces_html_tags_with_placeholder()
        {
            var result = "<br>A breakline<br/><p>Paragraph<P/><HEAD><Title>The title</TitLe>".ReplacesHtml("[TAG]");
            var control = "[TAG]A breakline[TAG][TAG]Paragraph[TAG][TAG][TAG]The title[TAG]";
            Assert.AreEqual(control, result);
        }
    }
}
