using System;
using System.Globalization;
using CQRS.Base.DDD.Domain.Annotations;

namespace CQRS.Base.DDD.Domain.SharedKernel
{
    [DomainValueObject]
    public struct Money
    {
        public static readonly NumberFormatInfo DefaultCurrencyFormat = CultureInfo.CurrentCulture.NumberFormat;
        public static readonly Money Zero = new Money(0);

        private readonly decimal _value;
        private readonly string _currencyCode;

        public string CurrencyCode
        {
            get { return _currencyCode; }
        }

        public decimal Value
        {
            get { return _value; }
        }

        public Money(decimal value, string currencyCode)
        {
            _value = value;
            _currencyCode = currencyCode;
        }

        public Money(decimal value, NumberFormatInfo currencyFormat)
            : this(value, currencyFormat.CurrencySymbol)
        {
        }

        public Money(decimal value) :
            this(value, DefaultCurrencyFormat)
        {
        }

        public Money(double value) :
            this((decimal)value, DefaultCurrencyFormat)
        {
        }
        
        public Money(int value) :
            this(value, DefaultCurrencyFormat)
        {
        }

        public override bool Equals(object obj)
        {
            if ( obj is Money)
            {
                var m = (Money) obj;
                return AreCompatibleCurrencies(this, m) || Value.Equals(m.Value);
            }

            return false;
        }

        
        public static Money operator +(Money m, Money m2)
        {
            if (!AreCompatibleCurrencies(m, m2))
            {
                throw new ArgumentException("Currency mismatch");
            }
            return new Money(m.Value + m2.Value, m.CurrencyCode);
        }

        public static Money operator -(Money m, Money m2)
        {
            if (!AreCompatibleCurrencies(m, m2))
            {
                throw new ArgumentException("Currency mismatch");
            }
            return new Money(m.Value - m2.Value, m.CurrencyCode);
        }

        public Money MultiplyBy(double multiplier)
        {
            return MultiplyBy((decimal)multiplier);
        }
        public Money MultiplyBy(int multiplier)
        {
            return MultiplyBy((decimal)multiplier);
        }

        public Money MultiplyBy(decimal multiplier)
        {
            return new Money(Value * multiplier, CurrencyCode);
        }

        //public static implicit operator Money(decimal value)
        //{
        //    return new Money(value);
        //}

        //public static implicit operator Money(double value)
        //{
        //    return (decimal)value;
        //}

        /// <summary>
        /// Currency is compatible if the same or either money object has zero value.
        /// </summary>
        private static bool AreCompatibleCurrencies(Money m, Money m2)
        {
            return IsZero(m.Value) || IsZero(m2.Value) || m.CurrencyCode.Equals(m2.CurrencyCode);
        }

        private static bool IsZero(decimal testedValue)
        {
            return decimal.Zero.CompareTo(testedValue) == 0;
        }
        
        public static bool operator <(Money m, Money m2)
        {
            return m.Value.CompareTo(m2.Value) < 0;
        }

        public static bool operator >(Money m, Money m2)
        {
            return m.Value.CompareTo(m2.Value) > 0;
        }

        public static bool operator >=(Money m, Money m2)
        {
            return m.Value.CompareTo(m2.Value) >= 0;
        }

        public static bool operator <=(Money m, Money m2)
        {
            return m.Value.CompareTo(m2.Value) <= 0;
        }

        public override string ToString()
        {
            return string.Format("{0}.2f {1}", Value, CurrencyCode);
        }
    }
}
