using System;
using System.Globalization;
using System.Threading;

namespace Watch
{
    /*
     * P.s я возможно не совсем понял задания, т.к не понял где тут и зачем применять перегрузку методов.
     * Но уверяю, я знаю что это такое и как использовать :) Ну и тут видно, что я отошёл от начального ТЗ и внёс улучшения.
     */

    class Watch
    {
        public int Hour { get; private set; }
        public int Minute { get; private set; }
        public int Second { get; private set; }

        public double HourAngle { get; private set; }
        public double MinuteAngle { get; private set; }
        public double SecondAngle { get; private set; }

        public DateTime DateTime { get; private set; }

        public Watch(DateTime dateTime)
        {
            UpdateTime(dateTime);
        }

        public Watch UpdateTime(DateTime dateTime)
        {
            Hour = dateTime.Hour;
            Minute = dateTime.Minute;
            Second = dateTime.Second;

            DateTime = dateTime;

            CalculateAngles();

            return this;
        }

        private void CalculateAngles()
        {
            SecondAngle = Second * 6d;
            MinuteAngle = Minute * 6d + (6d / 360d * Second * 6d);
            HourAngle = FormatHours() * 30d + (30d / 360d * MinuteAngle);

            SecondAngle = SecondAngle >= 360 ? 0 : SecondAngle;
            MinuteAngle = MinuteAngle >= 360 ? 0 : MinuteAngle;
            HourAngle = HourAngle >= 360 ? 0 : HourAngle;
        }

        private int FormatHours()
        {
            var hours = Hour;

            if (Hour == 12) hours = 0;
            else if (Hour > 12) hours -= 12;

            return hours;
        }

        public override string ToString()
        {
            var hAng = string.Format("{0:F3}", HourAngle);
            var mAng = string.Format("{0:F3}", MinuteAngle);
            var sAng = string.Format("{0:F3}", SecondAngle);

            return $"Время: [{DateTime.ToString("HH:mm:ss")}]  Углы: [{hAng} {mAng} {sAng}]";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DateTime dateTime;

            string format = "H:m:s";
            CultureInfo invariant = CultureInfo.InvariantCulture;

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.Write("\n\tВведите время старта часов в формате [чч:мм:сс]: ");
            while (!DateTime.TryParseExact(Console.ReadLine(), format, invariant, DateTimeStyles.None, out dateTime))
            {
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                Console.WriteLine(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 2);
                Console.Write($"\n\tВведите время старта часов в формате [чч:мм:сс]: ");
            }

            Console.Clear();
            Console.CursorVisible = false;
            
            var watch = new Watch(dateTime);

            while (true)
            {
                var tab = new string(' ', 8);

                Console.SetCursorPosition(0, 0);
                Console.Write($"\n\t{watch.UpdateTime(dateTime += TimeSpan.FromSeconds(1))}{tab}");

                var diffHoursSeconds = string.Format("{0:F3}", Math.Abs(watch.HourAngle - watch.SecondAngle));
                var diffHoursMinutes = string.Format("{0:F3}", Math.Abs(watch.HourAngle - watch.MinuteAngle));
                var diffMinutesSeconds = string.Format("{0:F3}", Math.Abs(watch.MinuteAngle - watch.SecondAngle));

                Console.WriteLine($"\n\n\t\tУгол между Чс и Cc: {diffHoursSeconds}{tab}");
                Console.WriteLine($"\t\tУгол между Чс и Мс: {diffHoursMinutes}{tab}");
                Console.WriteLine($"\t\tУгол между Мс и Сс: {diffMinutesSeconds}{tab}");

                for (int i = 0; i < Console.WindowHeight - 6; i++)
                {
                    Console.Write(new string(' ', Console.WindowWidth));
                }

                /* Здесь можно установить здаержку инкрименции, что бы детальнее расмотреть изменение углов стрелок */
                /* Либо кликнуть по консоли что бы приостановить процесс вывода. (Например для проверки верности значений) */
                // Thread.Sleep(10);
            }
        }
    }
}
