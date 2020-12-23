using System;

namespace LabyrinthGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.SetWindowSize(122, 61);

			var labyrinth = new Labyrinth(30, 30);
			labyrinth.Generate();
			labyrinth.Draw();

			Console.ReadKey();
		}
	}
}