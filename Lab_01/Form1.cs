using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab_01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        #region Поля: змінні та константи.
        /// <summary>
        /// Ліва межа графіка функції.
        /// #region Поля: змінні та константи.

        /// </summary>
        private double XMin = 0;
        /// <summary>
        /// Права межа графіка функції.
        /// </summary>
        private double XMax = 45;
        /// <summary>
        /// Крок графіка функції.
        /// </summary>
        private double Step = 1;
        /// <summary>
        /// Масив значень X.
        /// </summary>
        private double[] x;
        /// <summary>
        /// Масиви значень Y.
        /// </summary>
        private double[] y;
        private double[] y2;
        private double[] ylogy;

        private const double MY_COMFORT_LEVEL = 4.5;
        #endregion

        #region Методи.
        /// <summary>
        /// Розрахунок значень графіка функції.
        /// </summary>
        private void Calculate()
        {

            // Створюємо масиви відповідних розмірів.
            x = new double[10].Select((x,i) => i * 5.0).ToArray();
            y = new double[] { 0, 0.25, 0.5, 0.75, 1, 1, 0.75, 0.5, 0.25, 0};
            y2 = new double[10];
            ylogy = new double[10];


            for (int i = 0; i < 10; i++)
            {
                y[i] += MY_COMFORT_LEVEL;
                y2[i] = Math.Pow(y[i], 2);
                ylogy[i] = y[i] * Math.Log(y[i]);
            }
        }

        /// <summary>
        /// Створити серію та додати її до карти
        /// </summary>
        /// <param name="SeriesName">Назва серії</param>
        /// <param name="values">Значення</param>
        /// <param name="MarkerStyle">Стиль маркеру</param>
        /// <param name="MarkerColor">Колір маркеру</param>
        private void AddSeries(string SeriesName, double[] values, MarkerStyle MarkerStyle, Color MarkerColor )
        {
            // Створюємо об'єкт для графіка.
            Series series = new Series();
            // Посилаємося область для побудови графіка.
            series.ChartArea = "myGraph";
            // Задаємо тип графіка – лінії.
            series.ChartType = SeriesChartType.Line;
            // Вказуємо ширину лінії графіка.
            series.BorderWidth = 4;
            // Назва графіка для відображення у легенді.
            series.LegendText = SeriesName;
            // Додаємо колекцію крапок.
            series.Points.Add(values);

            // Завдання іміджу точок.
            series.MarkerStyle = MarkerStyle;
            // Завдання розміру точок.
            series.MarkerSize = 10;
            // Завдання кроку між точками.
            series.MarkerStep = 1;
            // Завдання кольору крапок.
            series.MarkerColor = MarkerColor;

            // Додаємо до списку графіків діаграми.
            chart.Series.Add(series);
        }

        /// <summary>
        /// Створюємо елемент управління Chart та налаштовуємо його.
        /// </summary>
        private void CreateChart()
        {
            // Створюємо новий елемент керування Chart.
            chart = new Chart();
            // Поміщаємо його форму.
            chart.Parent = this;
            // Задаємо розміри елемента.
            chart.SetBounds(10, 10, ClientSize.Width - 20, ClientSize.Height - 20);
            DataPointCustomProperties points = new DataPointCustomProperties();
            // Створюємо нову область для побудови графіка.
            ChartArea area = new ChartArea();
            // Даємо їй ім'я (щоб потім додавати графіки).
            area.Name = "myGraph";
            // Задаємо ліву та праву межі осі X.
            area.AxisX.Minimum = XMin;
            area.AxisX.Maximum = XMax;
            // Визначаємо крок сітки.
            area.AxisX.MajorGrid.Interval = Math.Abs(Step);
            // Підписи до осей.
            area.AxisX.Title = "T,℃";
            area.AxisY.Title = "μ(T)";
            // Задаємо шрифт та стиль підписів осей графіка.
            area.AxisX.TitleFont = new Font("Times New Roman", 12,
            FontStyle.Bold);
            area.AxisY.TitleFont = new Font("Times New Roman", 12,
            FontStyle.Bold);
            // Додаємо область у діаграму.
            chart.ChartAreas.Add(area);


            // Аналогічні дії першого графіка.
            AddSeries("Рівень звичайного комфорту", y, MarkerStyle.Circle, Color.Red);
            // Аналогічні дії другого графіка.
            AddSeries("Підвищений комфорт.", y2, MarkerStyle.Square, Color.Green);
            // Аналогічні дії другого графіка.
            AddSeries("Більш-менш комфорт.", ylogy, MarkerStyle.Triangle, Color.Orange);
            

            // Створюємо легенду, яка показуватиме назви.
            Legend legend = new Legend();
            chart.Legends.Add(legend);
            // Задаємо шрифт та стиль легенди.
            chart.Legends[0].Font = new Font("Times New Roman", 12,
            FontStyle.Bold);
        }

        /// <summary>
        /// Обробник подій форми.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            // Створюємо елемент управління.
            CreateChart();
            // Розраховуємо значення точок графіка функції.
            Calculate();
            // Додаємо обчислені значення графіки функцій.
            chart.Series[0].Points.DataBindXY(x, y);
            chart.Series[1].Points.DataBindXY(x, y2);
            chart.Series[2].Points.DataBindXY(x, ylogy);

        }

    }
    #endregion
}
