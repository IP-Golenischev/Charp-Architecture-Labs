using System;

namespace Incapsulation.RationalNumbers
{
    //Класс, представляющий собой обыкновенную дробь(рациональное число)
    public class Rational
    {
        //Свойство, доступное только для чтения, числитель дроби
        public int Numerator { get; }
        //Свойство, доступное только для чтения, знаменатель дроби
        public int Denominator { get; }
        // Прверка на то, что дробь некорретная, если знаменатель равен 0.
        // Стрелочка нужна, чтобы при обращению к свойству выполнялась проверка на 0
        //в знаменателе
        public bool IsNan => Denominator == 0;
        //Рекурсивная реализация алгоритма Евклида для поиска НОД двух чисел
        // для сокращения дроби и определения НОК
        private static int gcd(int first, int second) => second == 0 ? first : gcd(second, first % second);
        //Метод определения НОК чисел. По определению оно равно
        //Произведению этих чисел, делённое на НОД

        private static int lcd(int first, int second) => first * second / gcd(first, second);


        //Конструктор, создающий дробь, принимающий числитель и знаменатель
        public Rational(int numerator, int denominator)
        {
            //Если знаменатель отрицательный, то тогда берём числитель с
            //С противоположным знаком, а знаменатель берём по модул.
            if(denominator<0)
            {
                numerator = -numerator;
                denominator = -denominator;

            }
            //Определяем НОД чисел
            int nod = Math.Abs(denominator!= 0 ? gcd(numerator, denominator) : 1);
            // Присваиваем значения числителя и знаменателя и сокращаем дробь
            Numerator = numerator/nod;
            Denominator = denominator/nod;

        }
        //Конструктор от целого числа
        public Rational(int value) : this(value,1)
        {

        }
        //Перегруженный оператор умножения двух дробей в результате получается
        // Новая дробь, у которой числитель равень произведению числителей
        // А знаменатель произведению знаменателей
        // Если у одной из дробей знаменатель равен 0, то получаем некорректную
        // дробь
        public static Rational operator *(Rational first, Rational second)
            => new Rational(first.Numerator * second.Numerator, 
                first.Denominator ==0 || second.Denominator ==0 ? 0 : first.Denominator * second.Denominator);
        //Перегруженный оператор деления двух дробей в результате получается
        // Новая дробь, у которой числитель равень произведению числителя
        //первой дроби на знаменатель второй
        // А знаменатель произведению знаменателя первой на числитель второй
        // Если у одной из дробей знаменатель равен 0, то получаем некорректную
        // дробь
        public static Rational operator /(Rational first, Rational second)
    => new Rational(first.Numerator * second.Denominator,
        first.Denominator == 0 || second.Denominator == 0 ? 0 : first.Denominator * second.Numerator);
        //Перегруженный оператор сложения двух дробей в результате получается
        // Новая дробь по правилу сложения дробей
        // Если у одной из дробей знаменатель равен 0, то получаем некорректную
        // дробь
        public static Rational operator +(Rational first, Rational second)
        {
            //Получаем итоговый знаменаль равный НОК знаменателей
            int denominator = lcd(first.Denominator, second.Denominator);
            //Если НОК равен 0, то один из знаменателей равен 0, а значит 
            //Возращаем некорректную дробь
            if (denominator == 0) 
                return new Rational(0, 0);
            //Если один из знаменателей равен 0, то возвращаем некорректную дробь
            if (second.Denominator == 0 || first.Denominator == 0)
                return new Rational(0, 0);
            //Числитель дроби равен произведению числителя первой дроби на 
            // общий знаменатель, делённый на общий знаменатель, 
            // Числитель второй дроби равен произведению числителя первой дроби
            // На общий знаменатель, делённый на знаменатель второй дроби
            // И считаем сумму полученных числителей
            int numberator = first.Numerator * (denominator/first.Denominator) 
                + second.Numerator*(denominator/second.Denominator);
            // Возвращаем итоговую дробь
            return new Rational(numberator, denominator);
        }
        //Разность дробей - это сумма первой дроби плюс сумма второй дроби
        // с противоположным знаком, то есть умноженное на -1
        public static Rational operator -(Rational first, Rational second) => first + (second * new Rational(-1, 1));

        //Приведение к типу double, если дробь корректна, то частному числителя
        // на знаменатель, иначе нечислу
        public static implicit operator double(Rational r) => r.IsNan ? double.NaN : (double)r.Numerator / r.Denominator;
        //Приведение целого числа к дроби - просто вызов конструктора
        public static implicit operator Rational(int value)  => new Rational(value);
        //Приведение дроби к целому числу, если дробь некорректна или числитель
        // Не кратен знаменателю, то бросаем исключение
        // Иначе возвращаем числитель делённый на знаменатель

        public static explicit operator int(Rational rational)
        {
            //Если дробь некорректна или числитель не кратен знаменателю
            // То бросаем исключение
            if (rational.IsNan || rational.Numerator % rational.Denominator != 0)
            {
                throw new ArgumentException();
            }
            //Возвращаем частное числителя на знаменатель
            return rational.Numerator / rational.Denominator;
        }
    }
    
}
