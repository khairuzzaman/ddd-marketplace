using Marketplace.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Marketplace.Domain
{
    public class ClassifiedAddTitle : Value<ClassifiedAddTitle>
    {
        public static ClassifiedAddTitle FromString(string title)
        {
            CheckValidity(title);
            return new ClassifiedAddTitle(title);
        }

        public static ClassifiedAddTitle FromHtml(string htmlTitle)
        {
            var supportedTagsReplaced = htmlTitle
                .Replace("<i>", "*")
                .Replace("</i>", "*")
                .Replace("<b>", "*")
                .Replace("</b>", "*");

            var value = Regex.Replace(supportedTagsReplaced, "<.*?>", string.Empty);
            CheckValidity(value);
            return new ClassifiedAddTitle(value);
        }

        public string Value { get; }
        internal ClassifiedAddTitle(string value) => Value = value;

        public static implicit operator string(ClassifiedAddTitle title) => title.Value;

        private static void CheckValidity(string value)
        {
            if (value.Length > 100) throw new ArgumentOutOfRangeException("Title cannot be longer than 100 characters", nameof(value));
        }
    }
}
