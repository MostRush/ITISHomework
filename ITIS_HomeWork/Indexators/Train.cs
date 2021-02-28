using System;
using System.Collections.Generic;
using System.Text;

namespace Indexators
{
    class Train
    {
        public readonly string Text;

        public string Number { get; set; }
        public int CountOfVagons { get; set; }

        public List<Carriage> Carriages { get; set; }
        public List<Station> Stations { get; set; }

        public Train(List<Carriage> carriages, List<Station> stations)
        {
            Carriages = carriages;
            Stations = stations; 
        }

        public void PrinTrainData()
        {
            Console.Write($"Поезд с номером: {Number}");
            
            Console.Write($"\n\tУ поезда {Number} есть вагоны {Carriages.Count}:");

            foreach (var c in Carriages)
            {
                Console.Write($"\n\tИмя: {c.Name}");
            }

            Console.Write($"\n\nСтанции {Stations.Count}:");

            foreach (var s in Stations)
            {
                Console.WriteLine($"\n\tСтанция:{s.Name}" +
                    $"\n\t\tВремя прибытия: {s.TimeOfArrival}" +
                    $"\n\t\tВремя отправления: {s.TimeOfDeparture}");
            }

            Console.Write("\n\nВремени пути между станциями:");

            Station lastStation = null;

            foreach (var s in Stations)
            {
                if (lastStation is null)
                {
                    lastStation = s;
                    continue;
                }

                var t = CalcTimeOnWay(lastStation, s);

                Console.Write($"\n\tВремени в пути между станцией {lastStation.Name} и {s.Name}: ");
                Console.Write($"{t.Value.Days} d {t.Value.Hours}:{t.Value.Minutes}:{t.Value.Seconds}");

                lastStation = s;
            }
        }

        public TimeSpan? CalcTimeOnWay(Station station1, Station station2)
        {
            return station2.TimeOfArrival - station1.TimeOfDeparture;
        }
    }
}
