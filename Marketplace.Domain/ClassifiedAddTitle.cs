using Marketplace.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Marketplace.Domain
{
    public class ClassifiedAddTitle : Value<ClassifiedAddTitle>
    {
        public static ClassifiedAddTitle FromString(string title) => new ClassifiedAddTitle(title);

        private readonly string _value;
        private ClassifiedAddTitle(string value)
        {
            if (value.Length > 100) throw new ArgumentOutOfRangeException("Title cannot be longer that 100 characters", nameof(value));
            _value = value;
        }

        public static implicit operator string(ClassifiedAddTitle title) => title._value;
    }
}
