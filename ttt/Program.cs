using ttt;

class Program
{
    static void Main()
    {
        TicTacToeQLearning qLearning = new TicTacToeQLearning();
        qLearning.Train(10000);
        Console.WriteLine("Training completed!");

        foreach (var state in qLearning.qTable.Keys.OrderBy(s => s))
        {

            var values = qLearning.qTable[state];
            Console.WriteLine($"{state}: {string.Join(", ", values)}");
        }
    }
}
