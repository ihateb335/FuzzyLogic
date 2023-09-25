using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_02
{
    internal class Program
    {
        /// <summary>
        /// Виведення матриці композиції на консоль.
        /// </summary>
        /// <param name="Q">Матриця, що виводиться</param>
        /// <param name="labelsX">Масив міток, для осі X</param>
        /// <param name="labelsY">Масив міток, для осі Y</param>
        /// <param name="description">Опис таблиці</param>
        static void PrintMatrix(double[,] Q, string[] labelsX, string[] labelsY, string description)
        {
            int T_Rows = Q.GetUpperBound(0) + 1;
            int T_Columns = Q.Length / T_Rows;

            if (T_Rows != labelsY.Length) throw new ArgumentException($"{labelsY.Length} != {T_Rows}");
            if (T_Columns != labelsX.Length) throw new ArgumentException($"{labelsX.Length} != {T_Columns}");

            Console.WriteLine(description);
            Console.WriteLine(labelsX.Aggregate("\t", (prev, next) => $"{prev}\t{next}" ));
            for (int i = 0; i < T_Rows; i++)
            {
                Console.Write($"{labelsY[i]}\t");
                for (int j = 0; j < T_Columns; j++)
                {
                    Console.Write($"{Q[i, j], 2:F2}\t\t");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Обчислюємо матрицю композиції.
        /// </summary>
        /// <returns>Нечітке відношення Q рекомендацій кандидатом відповідних спеціальностей.</returns>
        /// <param name="S">Перша матриця нечітких відносин.</param>
        /// <param name="T">Друга матриця нечітких відносин.</param>
        /// <param name="Q">Результуюча матриця процесу максимінної згортки.</param>
        static double[,] CalculateQ(double[,] S, double[,] T)
        {
            // T_Rows = S_Columns, S_Rows = T_Columns.
            int T_Rows = T.GetUpperBound(0) + 1;
            int T_Columns = T.Length / T_Rows;
            int S_Rows = S.GetUpperBound(0) + 1;
            int S_Columns = S.Length / S_Rows;

            double[,] Q = new double[S_Rows, T_Columns];
            // Проміжна змінна, яка зберігає число-претендент у результуючий масив.
            double tmp;
            for (int i = 0; i < T_Columns; i++)
            {
                for (int j = 0; j < S_Rows; j++)
                {
                    // Максимальний елемент масиву tmp.
                    double MaxElem = 0;
                    // Цикл, у якому відбувається (max-min)-композиція або ще так звана максимінна згортка.
                    for (int k = 0; k < S_Columns; k++)
                    {
                        tmp = Math.Min(T[k, i], S[j, k]);
                        if (tmp > MaxElem)
                        {
                            MaxElem = tmp;
                        }
                    }
                    // Запис отриманого значення результуючу матрицю.
                    Q[j, i] = MaxElem;
                }
            }

            return Q;
        }


        static void Main(string[] args)
        {

            const string SEPARATOR = "\t\t\t----------------------------------------------";
            const double VARIANT = 0.04;

            /// <summary>
            /// Нечітке відношення S профіль спеціальностей.
            /// </summary> // Y - безліч психофізіологічних характеристик.
            double[,] S = { 
                { 0.9, 0.9, 0.8, 0.4, 0.5},
                { 0.8, 0.5, 0.9, 0.3, 0.1},
                { 0.3, 0.9, 0.6, 0.5, 0.9},
                { 0.5, 0.4, 0.5, 0.5, 0.2},
                { 0.7, 0.8, 0.8, 0.2, 0.6}
            };
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    S[i, j] += VARIANT;
                }
            }
            // Х - безліч спеціальностей, якими проводиться набір.
            /// <summary>
            /// Нечітке відношення T профіль кандидатів.
            /// </summary> // Z – безліч кандидатів на навчання.
            double[,] T = { 
                { 0.9, 0.8, 0.7, 0.9, 1.0},
                { 0.6, 0.4, 0.8, 0.5, 0.6},
                { 0.5, 0.2, 0.3, 0.8, 0.7},
                { 0.5, 0.9, 0.5, 0.8, 0.4},
                { 1.0, 0.6, 0.5, 0.7, 0.4}
            };

            // Викликаємо метод обчислення матриці композиції.
            var Q = CalculateQ(S, T);

            string[] professions = { "Менеджер", "Програмiст", "Водiй\t", "Секретар", "Перекладач" };
            string[] candidates = { "Петренко", "Iваненко", "Сидоренко", "Василенко", "Григоренко " };
            string[] qualities = { "Швидкiсть", "Розсудливiсть", "Концентрацiя", "Пам'ять\t", "Реакцiя " };

            PrintMatrix(S, qualities, professions, "Нечiтке вiдношення S-профiлювання спецiальностей навчання:");
            Console.WriteLine(SEPARATOR);
            PrintMatrix(T, candidates, qualities, "Нечiтке вiдношення T-профiлювання кандидатiв на навчання:");
            Console.WriteLine(SEPARATOR);
            // Викликаємо метод виведення матриці композиції.
            PrintMatrix(Q, candidates, professions, "Матриця Q - це бiнарна композиця матриць S i T. Висновок матрицi Q:");
        }
    }
}
