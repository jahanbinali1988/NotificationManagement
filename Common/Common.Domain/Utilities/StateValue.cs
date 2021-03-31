using System;

namespace Common.Domain.Utilities
{
    [AttributeUsage(AttributeTargets.Class)]
    public class StateValue : Attribute
    {
        public int Value { get; private set; }
        public string Text { get; private set; }
        public StateValue(int value, string text)
        {
            Value = value;
            Text = text;
        }
    }
}
