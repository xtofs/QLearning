namespace ttt;
public class TicTacToeQLearning
{

    private const double LearningRate = 0.1;
    private const double DiscountFactor = 0.9;
    private const double ExplorationRate = 0.1;

    public Dictionary<string, double[]> qTable = [];

    public void Train(int episodes)
    {
        for (int episode = 0; episode < episodes; episode++)
        {
            // char[,] board = NewBoard();
            // bool isPlayerX = true;
            TicTacToeGame game = new();


            while (true)
            {
                string state = game.GetState();
                int action = ChooseAction(state);

                // TODO: move to game
                (int row, int col) = GetMove(action);
                game.board[row, col] = game.isPlayerX ? 'X' : 'O';
                game.isPlayerX = !game.isPlayerX;

                if (game.IsWinning())
                {
                    UpdateQTable(state, action, 1);
                    break;
                }
                else if (game.IsBoardFull())
                {
                    UpdateQTable(state, action, 0.5);
                    break;
                }
                else
                {
                    UpdateQTable(state, action, 0);
                }

            }
        }
    }


    const int GridSize = TicTacToeGame.GridSize;

    private int ChooseAction(string state)
    {
        if (!qTable.ContainsKey(state))
        {
            qTable[state] = new double[GridSize * GridSize];
        }

        if (new Random().NextDouble() < ExplorationRate)
        {
            return new Random().Next(GridSize * GridSize);
        }
        else
        {
            double[] qValues = qTable[state];
            int maxIndex = 0;
            for (int i = 1; i < qValues.Length; i++)
            {
                if (qValues[i] > qValues[maxIndex])
                {
                    maxIndex = i;
                }
            }
            return maxIndex;
        }
    }

    private (int, int) GetMove(int action)
    {
        return (action / GridSize, action % GridSize);
    }

    private void UpdateQTable(string state, int action, double reward)
    {
        // TODO: get or add to qTable

        if (!qTable.ContainsKey(state))
        {
            qTable[state] = new double[GridSize * GridSize];
        }

        double[] qValues = qTable[state];
        qValues[action] = qValues[action] + LearningRate * (reward + DiscountFactor * MaxQValue(state) - qValues[action]);
    }

    private double MaxQValue(string state)
    {
        if (qTable.TryGetValue(state, out double[]? qValues))
        {
            return qValues.Max();
        }

        return 0;
    }

}
