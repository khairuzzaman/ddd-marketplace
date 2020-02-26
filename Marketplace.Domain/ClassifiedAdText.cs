using Marketplace.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class ClassifiedAdText : Value<ClassifiedAdText>
    {
        public string Value { get; }

        internal ClassifiedAdText(string text) => Value = text;
        public static ClassifiedAdText FromString(string text) => new ClassifiedAdText(text);

        public static implicit operator string(ClassifiedAdText text) => text.Value;
    }
}
