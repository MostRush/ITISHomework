using System;
using System.Collections.Generic;

namespace Indexators
{
    class Program
    {
        static void Main(string[] args)
        {
            /*var carriages = new List<Carriage>();

            Console.Write("\n\tВведи кол-во вагонов: ");
            var countOfCarriages = int.Parse(Console.ReadLine());

            for (int i = 1; i <= countOfCarriages; i++)
            {
                Console.Write($"\t\tВведи название вагона #{i}: ");
                var carriage = new Carriage(Console.ReadLine());

                carriages.Add(carriage);
            }

            Console.Write("\n\tВведи кол-во станций: ");
            var countOfStations = int.Parse(Console.ReadLine());

            var stations = new List<Station>();

            for (int i = 1; i <= countOfStations; i++)
            {
                Console.WriteLine($"\n\tЗаполните данные станции #{i}");

                var station = new Station();

                Console.Write("\t\tИмя санции: ");
                station.Name = Console.ReadLine();

                Console.Write("\t\tВремя прибытия: ");
                station.TimeOfArrival = DateTime.Parse(Console.ReadLine());

                Console.Write("\t\tВремя отправленя: ");
                station.TimeOfDeparture = DateTime.Parse(Console.ReadLine());

                stations.Add(station); 

            
                Console.Write("\n\tВведите номер поезда: ");
                train.Number = Console.ReadLine();
            }*/

            var carriages = new List<Carriage>();
            var stations = new List<Station>();

            carriages.Add(new Carriage("A-1"));
            carriages.Add(new Carriage("A-2"));
            carriages.Add(new Carriage("A-3"));
            carriages.Add(new Carriage("A-4"));

            var s1 = new Station();
            s1.Name = "Алматы";
            s1.TimeOfArrival = null;
            s1.TimeOfDeparture = DateTime.Now;

            var s2 = new Station();
            s2.Name = "Петропавлоск";
            s2.TimeOfArrival = s1.TimeOfDeparture + TimeSpan.FromHours(1);
            s2.TimeOfDeparture = s2.TimeOfArrival + TimeSpan.FromMinutes(10);

            var s3 = new Station();
            s3.Name = "ЕкатеренбурК";
            s3.TimeOfArrival = s2.TimeOfDeparture + TimeSpan.FromHours(1);
            s3.TimeOfDeparture = s3.TimeOfArrival + TimeSpan.FromMinutes(10);

            var s4 = new Station();
            s3.Name = "Казань";
            s3.TimeOfArrival = s3.TimeOfDeparture + TimeSpan.FromHours(1);
            s3.TimeOfDeparture = s4.TimeOfArrival + TimeSpan.FromMinutes(10);

            stations.AddRange(new Station[] { s1, s2, s3, s4 });

            var train = new Train(carriages, stations);
            train.Number = "A-10";
            train.Stations = stations;
            train.Carriages = carriages;

            var v = train.Text;

            train.Text = "";

            //Console.Clear();

            train.PrinTrainData();

            Console.ReadKey();
        }
    }
}
