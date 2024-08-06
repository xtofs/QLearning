namespace ttt;

using System.Runtime.InteropServices;

internal class TicTacToeGame
{
    public const int GridSize = 3;

    public char[,] board = NewBoard();

    public bool isPlayerX = true;

    private static char[,] NewBoard()
    {
        var array = new char[GridSize, GridSize];
        var span = MemoryMarshal.CreateSpan(ref array[0, 0], array.Length);
        span.Fill('.');
        return array;

        // char[,] board = new char[GridSize, GridSize];
        // for (int i = 0; i < GridSize; i++)
        // {
        //     for (int j = 0; j < GridSize; j++)
        //     {
        //         board[i, j] = '.';
        //     }
        // }

        // return board;
    }


    public string GetState() // char[,] board)
    {
        return string.Create(GridSize * GridSize, board, (span, board) =>
        {
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    span[i * GridSize + j] = board[i, j];
                }
            }
        });
    }

    public bool IsWinning()
    {
        char player = isPlayerX ? 'X' : 'O';
        // Check rows, columns, and diagonals for a winning move
        for (int i = 0; i < GridSize; i++)
        {
            if ((board[i, 0] == player && board[i, 1] == player && board[i, 2] == player) ||
                (board[0, i] == player && board[1, i] == player && board[2, i] == player))
            {
                return true;
            }
        }
        return (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player) ||
               (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player);
    }

    public bool IsBoardFull()
    {
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                if (board[i, j] == '.')
                {
                    return false;
                }
            }
        }
        return true;
    }
}