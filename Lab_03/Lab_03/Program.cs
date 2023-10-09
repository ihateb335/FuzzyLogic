using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_03
{
    class Program
    {
        /// <summary>
        /// Метод обчислення значень істинності нечітких висловлювань.
        /// </summary>
        /// <returns>Шукане значення істинності розглянутих нечітких висловлювань </returns>
        /// <param name="Rules">Масив наявних правил на митниці.</param>
        /// <param name="Situations">Масив ситуацій, що склалися на митниці.</param>
        static FuzzyValue[] Calculation(
            FuzzyValue[] Rules, 
            FuzzyValue[] Situations)
        {
            FuzzyValue 
                onRule1 = !Situations[0] & Rules[0], // НЕ чиновник І Правило 1
                onRule2 = Situations[0] & Rules[1], // Чиновник І Правило 2
                onRule3 = onRule2 & Rules[2], // Підлягає огляду І Правило 3
                onRule4 = !Situations[1] & Rules[3], // НЕ Мало людей І Правило 4
                onRule5 = onRule4 & Rules[4], // Втомився І Правило 5
                onRule6 = onRule1 & !Situations[3] & Rules[5], // Пілдлягає І Присутня інформація І Правило 6
                onRule7 = onRule1 & Situations[2] & Rules[6]; // Підлягає І Оснащений І Правило 6

            onRule1.Description = "Громадяни будуть піддані митному огляду.";
            onRule2.Description = "Громадяни не будуть піддані митному огляду.";
            onRule3.Description = "Не виключається можливість перевезення наркотиків.";
            onRule4.Description = "Контролер відчуває втому.";
            onRule5.Description = "Не виключається можливість перевезення наркотиків.";
            onRule6.Description = "Виключається можливість перевезення наркотиків";
            onRule7.Description = "Виключається можливість перевезення наркотиків. ";

            
            var result_1_value = onRule6 | onRule7; // АБО виключається за 6-им АБО за 7-мим
            var result_2_value = onRule3 | onRule5; // АБО НЕ виключається за 3-им АБО за 5-мим

            result_1_value.Description = "Виключається можливість перевезення наркотиків.";
            result_2_value.Description = "Не виключається можливість перевезення наркотиків.";

            return new FuzzyValue[] { result_1_value, result_2_value };
        }

        /// <summary>
        /// Функція виведення консоль результатів обчислення.
        /// </summary>
        /// <param name="results">Масив результатів</param>
        static void PrintResult(FuzzyValue[] results)
        {
            for (int i = 0; i < results.Length; ++i)
            {
                Console.WriteLine($"Значення істинності: {results[i].Value} першого нечіткого висловлювання: {results[i].Description}");
            }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            FuzzyValue[] rules = { 
                new FuzzyValue(1.0, "ЯКЩО громадянин не є високопоставленим чиновником, ТО він піддається митному огляду."),
                new FuzzyValue(0.9, "ЯКЩО громадянин є високопоставленим чиновником, ТО він не піддається митному огляду."),
                new FuzzyValue(0.8, "ЯКЩО громадянин не піддається митному огляду, ТО не виключається можливість перевезення наркотиків."),
                new FuzzyValue(0.6, "ЯКЩО кількість громадян, що проходять митний огляд, велика, ТО контролер відчуває втому."),
                new FuzzyValue(0.7, "ЯКЩО контролер відчуває втому, ТО не виключається можливість провезення наркотиків."),
                new FuzzyValue(0.95, "ЯКЩО громадянин піддається митному огляду І щодо цього громадянина є агентурна інформація, ТО виключається можливість перевезення наркотиків."),
                new FuzzyValue(0.95, "ЯКЩО громадянин піддається митному огляду І контролер використовує новітні технічні засоби, ТО виключається можливість перевезення наркотиків."),
            };

            for (int i = 0; i < rules.Length; i++)
            {
                rules[i].Value -= 0.4;
            }

            FuzzyValue[] situations = {
                new FuzzyValue(0.2, "Серед громадян, які в'їжджають у країну, знаходяться високопосадовці."),
                new FuzzyValue(0.1, "Кількість громадян, які проходять митний огляд невелика."),
                new FuzzyValue(0.8, "Митний пункт контролю оснащений новітніми технічними засобами."),
                new FuzzyValue(0.9, "Яка-небудь попередня інформація про наявність наркотиків в окремих громадян відсутня."),
            };

            // Викликаємо метод обчислення.
            var results = Calculation( rules, situations);

            // Викликаємо метод виведення результатів на консоль.
            PrintResult(results);
        }
    }

}
