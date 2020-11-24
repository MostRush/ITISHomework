using System;

namespace TicTakToe
{
    public enum Mark
    {
        Empty,
        Cross,
        Circle
    }

    public enum GameResult
    {
        CrossWin,
        CircleWin,
        Draw
    }

    class Program
    {
        static void Main(string[] args)
        {

        }

        public static GameResult GetGameResult(Mark[,] field)
        {
            if (HasWinSequence(field, Mark.Cross) &&
                HasWinSequence(field, Mark.Circle))
                return GameResult.Draw;

            if (HasWinSequence(field, Mark.Cross))
                return GameResult.CrossWin;

            if (HasWinSequence(field, Mark.Circle))
                return GameResult.CircleWin;

            return GameResult.Draw;
        }

        static Mark[] GetMarksWithIndexes(Mark[,] field)
        {
            int index = 0;
            var marks = new Mark[field.Length];

            var x = field.GetLength(0);
            var y = field.GetLength(1);

            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    marks[index++] = field[i, j];

            return marks;
        }

        static bool HasWinSequence(Mark[,] field, Mark mark)
        {
            int[][] combinations = {
        new int[] { 1, 2, 3 },
        new int[] { 4, 5, 6 },
        new int[] { 7, 8, 9 },
        new int[] { 1, 4, 7 },
        new int[] { 2, 5, 8 },
        new int[] { 3, 6, 9 },
        new int[] { 1, 5, 9 },
        new int[] { 3, 5, 7 }
    };

            var marksByIndex = GetMarksWithIndexes(field);

            for (int i = 0; i < combinations.Length; i++)
                if (marksByIndex[combinations[i][0] - 1] == mark &&
                    marksByIndex[combinations[i][1] - 1] == mark &&
                    marksByIndex[combinations[i][2] - 1] == mark)
                    return true;

            return false;
        }
    }
}
