using System;

namespace TriforceDrawing
{
    class Triforce
    { 
        public int Height { get; private set; }
        private string Teamplate { get; set; }

        public Triforce(int height)
        {
            Height = height % 2 == 0 ? height : height + 1;
            Teamplate = CreateTeamplate();
        }

        private string CreateTeamplate()
        {
            string triforceTeamplate = string.Empty;

            var lineLenght = 1;
            var spaceLenght = Height * 2;
            var emptiness = Height * 2 - 1;

            for (int i = 0; i < Height; i++)
            {
                for (int k = 0; k < spaceLenght * 0.5; k++)
                {
                    triforceTeamplate += " ";
                }

                for (int j = 0; j < lineLenght; j++)
                {
                    int firstTrig = Math.Abs((lineLenght - emptiness) / 2 - 1);

                    if ((i > Height * 0.5 - 1) && (j > firstTrig && j < emptiness + firstTrig + 1))
                    {
                        triforceTeamplate += " ";
                    }
                    else
                    {
                        triforceTeamplate += "*";
                    }
                }

                emptiness = emptiness - 2;
                spaceLenght = spaceLenght - 2;
                lineLenght = lineLenght + 2;
                triforceTeamplate += "\n";
            }

            return triforceTeamplate;
        }

        public string UpdateHeight(int height)
        {
            Height = height % 2 == 0 ? height : height + 1;
            Teamplate = CreateTeamplate();
            return Teamplate;
        }

        public override string ToString() => Teamplate;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Triforce(30));
            Console.ReadKey();
        }
    }
}
