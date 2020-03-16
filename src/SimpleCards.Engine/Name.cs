using System;

using ByValue;

namespace SimpleCards.Engine
{
    public sealed class Name : SingleValueObject<string>
    {
        public Name(string value)
            : base(value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value not set", nameof(value));
        }

        public string Initial => Value.ToUpperInvariant()[0].ToString();

        public static implicit operator string(Name name) => name.Value;

        public static explicit operator Name(string value) => new Name(value);

        public override bool Equals(object obj)
        {
            var otherString = obj as string
                ?? (obj as Name)?.Value;

            return Value.Equals(otherString, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}