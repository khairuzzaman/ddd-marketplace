using Marketplace.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class ClassifiedAdText : Value<ClassifiedAdText>
    {
        public static ClassifiedAdText FromString(string text) => new ClassifiedAdText(text);

        private readonly string _value;
        private ClassifiedAdText(string value)
        {
            _value = value;
        }

        public static implicit operator string(ClassifiedAdText text) => text._value;
    }
}
