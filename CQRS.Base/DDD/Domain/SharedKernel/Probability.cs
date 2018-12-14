using System;

namespace CQRS.Base.DDD.Domain.SharedKernel
{
    public struct Probability
    {
        private readonly decimal _value;

        private Probability(decimal value)
        {
            _value = value;
        }

        public static Probability FromDecimal(decimal value)
        {
            if (value.CompareTo(decimal.Zero) == -1 || value.CompareTo(decimal.One) == 1)
                throw new ArgumentException("Value must be in <0,1>");

            return new Probability(value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is Probability)
            {
                Probability percentage = (Probability) obj;
                return Math.Round(percentage._value).CompareTo(Math.Round(_value)) == 0;
            }

            return false;
        }

        public Probability CombinedWith(Probability percentage)
        {
            return new Probability(_value*percentage._value);
        }

        public Probability Not()
        {
            return new Probability(decimal.One - _value);
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}