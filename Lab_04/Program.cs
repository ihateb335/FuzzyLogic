using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using FuzzyLib;

namespace Lab_04
{
    class Program
    {
        /// <summary>
        /// Метод обчислення значень істинності нечітких висловлювань.
        /// </summary>
        /// <param name="result_value">Шукане значення істинності аналізованого заданого нечіткого висловлювання.</param>
        /// <param name="rules">Масив причинних взаємозв'язків між безліччю передумов та безліччю наслідків.</param>
        /// <param name="situations">Масив ситуацій огляду автомобіля, що склалися.</param>
        static FuzzyValue[] Calculation(FuzzyValue[] rules, FuzzyValue[] situations)
        {
            var onRules = new FuzzyValue[rules.Length];
            for (int i = 0; i < rules.Length; i++)
            {
                onRules[i] = situations[i / 4] & rules[i];
                onRules[i].Description = rules[i].Description;
            }
            var Results = new FuzzyValue[situations.Length];
            int I(int i) => 4 * i;
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = Result(onRules[I(i)], onRules[I(i) + 1], onRules[I(i) + 2], onRules[I(i) + 3], situations[i]);
            }
            return Results;
        }
        /// <summary>
        /// Сформувати результат на огляду автомобіля і думки експерта
        /// </summary>
        /// <param name="left1">Застосування причини несправності акумулятора на основі результату огляду</param>
        /// <param name="left2">Застосування причини несправність карбюратора на основі результату огляду</param>
        /// <param name="right1">Застосування причини низька якість бензину на основі результату огляду</param>
        /// <param name="right2">Застосування причини несправність системи запалювання на основі результату огляду</param>
        /// <param name="situation">Результат огляду</param>
        /// <returns></returns>
        static FuzzyValue Result(FuzzyValue left1, FuzzyValue left2, FuzzyValue right1, FuzzyValue right2, FuzzyValue situation)
        {
            var left = left1 | left2;
            var right = right1 | right2;
            var result = left | right;

            result.Description = situation.Description + " " + left.Description + " ,або через " + right.Description ;
            return result;
        }

        /// <summary>
        /// Функція виведення консоль результатів обчислення.
        /// </summary>
        /// <param name="result">Результат</param>
        static void PrintResult(IEnumerable<FuzzyValue> Results)
        {
            var result = Results.Max();
            Console.WriteLine($"Значення істинності: {result.Value}\nРезультат: {result.Description}");
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;


            FuzzyValue.AndComparison = (A, B) => A.Value < B.Value ? A.Description : B.Description;
            FuzzyValue.OrComparison = (A, B) => A.Value < B.Value ? A.Description : B.Description;
            FuzzyValue.NotComparison = A => "НЕ " + A.Description;

        const string y_1 = "Двигун не запускається через";
            const string y_2 = "Двигун працює нестійко через";
            const string y_3 = "Двигун не розвиває повної потужності через";
            const string x_1 = "несправність акумулятора";
            const string x_2 = "несправність карбюратора";
            const string x_3 = "низьку якість бензину";
            const string x_4 = "несправність системи запалення";

            const double variant = 0.4;

            FuzzyValue[] rules = {
                new FuzzyValue(1.0, x_1),
                new FuzzyValue(0.8, x_2),
                new FuzzyValue(0.7, x_3),
                new FuzzyValue(1.0, x_4),
                new FuzzyValue(0.1, x_1),
                new FuzzyValue(0.9, x_2),
                new FuzzyValue(0.8, x_3),
                new FuzzyValue(0.5, x_4),
                new FuzzyValue(0.2, x_1),
                new FuzzyValue(1.0, x_2),
                new FuzzyValue(0.5, x_3),
                new FuzzyValue(0.2, x_4),
            };

            for (int i = 0; i < rules.Length; i++)
            {
                rules[i].Value = rules[i].Value - variant < 0 ? rules[i].Value + variant : rules[i].Value - variant;
            }

            FuzzyValue[] situations = { 
                new FuzzyValue(0.9, $"{y_1}"), 
                new FuzzyValue(0.1, $"{y_2}"), 
                new FuzzyValue(0.2, $"{y_3}") 
            };
                // Викликаємо метод обчислення.
            var result = Calculation(rules, situations);
            // Викликаємо метод виведення результатів на консоль.
            PrintResult(result);
        }
    }

}
