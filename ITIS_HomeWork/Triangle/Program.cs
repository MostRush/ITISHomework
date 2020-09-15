using System;
using System.Collections.Generic;

namespace Triangle
{
    public class Triangle
    {
        /// <summary>
        /// Сторона A треугольника
        /// </summary>
        public double SideA { get; }
        /// <summary>
        /// Сторона B треугольника
        /// </summary>
        public double SideB { get; }
        /// <summary>
        /// Сторона C треугольника
        /// </summary>
        public double SideC { get; }
        /// <summary>
        /// Является ли фигура треугольником
        /// </summary>
        public bool IsTriangle { get; }
        /// <summary>
        /// Периметр треугольника
        /// </summary>
        public double Perimeter { get; }

        /// <summary>
        /// Создать треуголльник по трём сторонам. Отрицательные значения буду приведены к значениям по модулю!
        /// </summary>
        /// <param name="sideA">Сторона a</param>
        /// <param name="sideB">Сторона b</param>
        /// <param name="sideC">Сторона c</param>
        public Triangle(double sideA, double sideB, double sideC)
        {
            SideA = Math.Abs(sideA);
            SideB = Math.Abs(sideB);
            SideC = Math.Abs(sideC);

            /* Проверка фигуры на треугольник */

            IsTriangle = SideA + SideB > SideC && SideA + SideC > SideB && SideB + SideC > SideA;

            /* Получение периметра треугольника */

            if (IsTriangle) Perimeter = SideA + SideB + SideC;
        }

        /// <summary>
        /// Создать треугольник по трём сторонам через кортеж.
        /// </summary>
        /// <param name="sides">Кортеж с значениями сторон. Отрицательные значения буду приведены к значениям по модулю!</param>
        public Triangle((double, double, double) sides)
            : this(sides.Item1, sides.Item2, sides.Item3) { }
        
        /// <summary>
        /// Получить все углы треугольника.
        /// </summary>
        /// <returns>Возвращает кортеж с значениями уголв треугольника.</returns>
        public (double, double, double) GetAngles()
        {
            if (!IsTriangle) return (0, 0, 0);

            var angA = Math.Acos((SideB * SideB + SideC * SideC - SideA * SideA) / (2 * SideB * SideC)) * 180 / Math.PI;
            var angB = Math.Acos((SideA * SideA + SideC * SideC - SideB * SideB) / (2 * SideA * SideC)) * 180 / Math.PI;
            var angC = Math.Acos((SideA * SideA + SideB * SideB - SideC * SideC) / (2 * SideA * SideB)) * 180 / Math.PI;

            return (angA, angB, angC);
        }

        /// <summary>
        /// Получить наибольшую выстоу треугольника.
        /// </summary>
        /// <returns>Возвращает значение типа double</returns>
        public double GetHighestHeight()
        {
            if (!IsTriangle) return 0;

            var fewest = SideA;
            fewest = fewest > SideB ? SideB : fewest;
            fewest = fewest > SideC ? SideC : fewest;

            return GetArea() * 2 / fewest;
        }

        /// <summary>
        /// Получить наименьшую высоту треугольника.
        /// </summary>
        /// <returns>Возвращает значение типа double</returns>
        public double GetLowestHeght()
        {
            if (!IsTriangle) return 0;

            var greatest = SideA;
            greatest = greatest < SideB ? SideB : greatest;
            greatest = greatest < SideC ? SideC : greatest;

            return GetArea() * 2 / greatest;
        }

        /// <summary>
        /// Получить площадь треугольника.
        /// </summary>
        /// <returns>Возвращает значение типа double</returns>
        public double GetArea()
        {
            if (!IsTriangle) return 0;

            var semiPerim = Perimeter / 2;

            return Math.Sqrt(semiPerim * (semiPerim - SideA) * (semiPerim - SideB) * (semiPerim - SideC));
        }

        /// <summary>
        /// Получить площадь вписанной в треугольник окружности.
        /// </summary>
        /// <returns>Возвращает значение типа double</returns>
        public double GetAreaOfInscrCircle()
        {
            var radius = GetArea() / (Perimeter / 2);

            return Math.PI * (radius * radius);
        }

        /// <summary>
        /// Вывод всей информации о треугольнике.
        /// </summary>
        /// <returns>Возвращает форматированную строку данных о треугольнике</returns>
        public override string ToString()
        {
            var validTriang = IsTriangle ? "Треугольник" : "Фигура";

            return $"{validTriang} со сторонами: [{SideA}, {SideB}, {SideC}]";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            /* Предикат определяющий работу цикла сицитывающего клавишу на строке 211 */
            bool onCreateNewTriangle = false;

            /* Программа выполняется бесконечно, пока пользователь не решит выйти из программы */
            while (true)
            {
                /* Выполняем процедуру ввода строн и создания треугольника */
                var triangle = CreateNewTriangle();

                /* Пока фигура не будет являться треугольником повторяем ввод */
                while (!triangle.IsTriangle)
                    triangle = CreateNewTriangle();

                /* Вывод сторон получившегося треугольника */
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"\n\t{triangle}");
                Console.ResetColor();

                /* Вывод инофрмационной карточки */
                Console.WriteLine("\n\tВыберите действие:" +
                    "\n\n\t1. Найти углы треугольника" +
                    "\n\t2. Найти площадь треугольника" +
                    "\n\t3. Найти наибольшую и наименьшую высотру треугольника" +
                    "\n\t4. Найти площать вписанной в треугольник окружности" +
                    "\n\n\t5. Ввести новый треугольник" +
                    "\n\t6. Выйти из программы");

                Console.Write("\n\n\t[Нажмите на соответсвующую цифре клавишу клавиатуры] \n\n\n");

                /* Нам не нужно создавать новый треугольник */
                onCreateNewTriangle = false;

                /* Создаём словарь с ключами клавиши клавиатруы и делегатами типа Action */
                var keyActions = new Dictionary<ConsoleKey, Action>();

                /* Действие нахождения углов треугольника */
                keyActions.Add(ConsoleKey.D1, new Action(() =>
                {
                    var angs = triangle.GetAngles();
                    var ang1 = string.Format("{0:F1}", angs.Item1);
                    var ang2 = string.Format("{0:F1}", angs.Item2);
                    var ang3 = string.Format("{0:F1}", angs.Item3);

                    WriteAnswer($"\n\tУглы треугольника: [{ang1}, {ang2}, {ang3}] ");
                }));

                /* Действие нахождения площади треугольника */
                keyActions.Add(ConsoleKey.D2, new Action(() =>
                {
                    WriteAnswer($"\n\tПлощадь треугольника: [{string.Format("{0:F1}", triangle.GetArea())}] ");
                }));

                /* Действие нахождения наиб и наим высоты треугольника */
                keyActions.Add(ConsoleKey.D3, new Action(() =>
                {
                    var height = string.Format("{0:F1}", triangle.GetHighestHeight());
                    var lowest = string.Format("{0:F1}", triangle.GetLowestHeght());

                    WriteAnswer($"\n\t[наибольшая, наименьшая] высота треугольника: [{height}, {lowest}] ");
                }));

                /* Действие нахождения площади вписанной окружности */
                keyActions.Add(ConsoleKey.D4, new Action(() =>
                {
                    WriteAnswer($"\n\tПлощадь вписанной окружности: [{string.Format("{0:F1}", triangle.GetAreaOfInscrCircle())}] ");
                }));

                /* Действие создания нового треугольника */
                keyActions.Add(ConsoleKey.D5, new Action(() =>
                {
                    Console.Clear();
                    onCreateNewTriangle = true;
                }));

                /* Действие выхода из программы */
                keyActions.Add(ConsoleKey.D6, new Action(() =>
                {
                    Console.ResetColor();
                    Environment.Exit(0);
                }));

                Action action = null;

                /* Цикл чтения клавиши будет выполняться до тех пор,
                 * пока не потребуется создать новый треугольник*/
                while (!onCreateNewTriangle)
                {
                    action = Console.ReadKey().Key switch
                    {
                        ConsoleKey.D1 => keyActions.GetValueOrDefault(ConsoleKey.D1),
                        ConsoleKey.D2 => keyActions.GetValueOrDefault(ConsoleKey.D2),
                        ConsoleKey.D3 => keyActions.GetValueOrDefault(ConsoleKey.D3),
                        ConsoleKey.D4 => keyActions.GetValueOrDefault(ConsoleKey.D4),
                        ConsoleKey.D5 => keyActions.GetValueOrDefault(ConsoleKey.D5),
                        ConsoleKey.D6 => keyActions.GetValueOrDefault(ConsoleKey.D6),
                        _ => null
                    };

                    Console.Write("\b \b");

                    if (action != null) action.Invoke();
                }
            }
        }

        /// <summary>
        /// Вывод форматированного ответа
        /// </summary>
        /// <param name="output">Входные данные</param>
        static void WriteAnswer(string output)
        {
            /* Вывод ответа не переходя на навую строку */
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            Console.WriteLine(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 2);

            Console.ForegroundColor = ConsoleColor.DarkGreen;

            Console.WriteLine(output);
            Console.ResetColor();
        }

        /// <summary>
        /// Создание треугольника путём ввода в консоль.
        /// </summary>
        /// <returns>Экземпляр класса Triangle</returns>
        static Triangle CreateNewTriangle()
        {
            /* Очистка консоли и вывод подсказки */
            Console.Clear();
            Console.WriteLine("\n\tВведите длины сторон треугольника: \n");

            /* Сокращаем код созданием массивов для похожих действий */
            string[] sideNames = { "A", "B", "C" };
            double[] sides = new double[3];

            int count = 0;

            /* Переьираем массив по всем сторонам, вывод записываем в sides[] */
            foreach (var side in sideNames)
            {
                /* В случае неудачной валидации заставить вводить число загого */
                Console.Write($"\t\tВведите сторону {side}: ");
                while (!double.TryParse(Console.ReadLine(), out sides[count]))
                {
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                    Console.Write($"\t\tВведите сторону {side}: ");
                }

                count++;
            }

            return new Triangle(sides[0], sides[1], sides[2]);
        }
    }
}