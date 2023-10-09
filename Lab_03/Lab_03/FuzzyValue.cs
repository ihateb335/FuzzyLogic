using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_03
{
    public class FuzzyValue
    {

        public FuzzyValue() { Value = 0; Description = ""; }
        public FuzzyValue(double value, string description) { Value = value; Description = description; }

        private double _value;

        public double Value
        {
            get => _value;
            set {
                if (value > 1.0 || value < 0) throw new ArgumentException($"Значення повинно входити у межі: 0 <= {value} <= 1");
                _value = value;
            }
        }

        public string Description { get; set; }

        public static FuzzyValue operator &(FuzzyValue A, FuzzyValue B) => new FuzzyValue(Math.Min(A.Value, B.Value), "");
        public static FuzzyValue operator |(FuzzyValue A, FuzzyValue B) => new FuzzyValue(Math.Max(A.Value, B.Value), "");
        public static FuzzyValue operator !(FuzzyValue A) => new FuzzyValue(1.0 - A.Value, "");

        public override string ToString()
        {
            return $"Value: {Value, 2:F2}; Description: {Description}";
        }

    }
}
