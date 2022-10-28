using System;

namespace Incapsulation.RationalNumbers
{
    public class Rational
    {
        public int Numerator { get; }
        public int Denominator { get; }
        public bool IsNan => Denominator == 0;
        private static int gcd(int first, int second) => second == 0 ? first : gcd(second, first % second);

        private static int lcd(int first, int second) => first * second / gcd(first, second);


        public Rational(int numerator, int denominator)
        {
            if(denominator<0)
            {
                numerator = -numerator;
                denominator = -denominator;

            }
            int nod = Math.Abs(denominator!= 0 ? gcd(numerator, denominator) : 1);
            Numerator = numerator/nod;
            Denominator = denominator/nod;

        }
        public Rational(int value) : this(value,1)
        {

        }
        public static Rational operator *(Rational first, Rational second)
            => new Rational(first.Numerator * second.Numerator, 
                first.Denominator ==0 || second.Denominator ==0 ? 0 : first.Denominator * second.Denominator);
        public static Rational operator /(Rational first, Rational second)
    => new Rational(first.Numerator * second.Denominator,
        first.Denominator == 0 || second.Denominator == 0 ? 0 : first.Denominator * second.Numerator);
        public static Rational operator +(Rational first, Rational second)
        {
            int denominator = lcd(first.Denominator, second.Denominator);
            if (denominator == 0) 
                return new Rational(0, 0);
            if (second.Denominator == 0 || first.Denominator == 0)
                return new Rational(0, 0);
            int numberator = first.Numerator * (denominator/first.Denominator) 
                + second.Numerator*(denominator/second.Denominator);
            return new Rational(numberator, denominator);
        }
        public static Rational operator -(Rational first, Rational second) => first + (second * new Rational(-1, 1));

        public static implicit operator double(Rational r) => r.IsNan ? double.NaN : (double)r.Numerator / r.Denominator;

        public static implicit operator Rational(int value)  => new Rational(value);

        public static explicit operator int(Rational rational)
        {
            if (rational.IsNan || rational.Numerator % rational.Denominator != 0)
            {
                throw new ArgumentException();
            }
            return rational.Numerator / rational.Denominator;
        }
    }
    
}
