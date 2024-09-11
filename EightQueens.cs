namespace eight_queens;
using static Constants;

class Program {
    static char[,] chessboard = {{EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                                 {EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                                 {EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                                 {EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                                 {EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                                 {EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                                 {EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY},
                                 {EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY, EMPTY}};
    static List<Position> positions = new List<Position>();
    static int stepsCount = 0;

    static void Main(string[] args){
        const int startingLine = 0;
        var startingColumn = 0;

        do {
            Console.WriteLine("Please choose the starting column with a number from 0 to 7.");
            startingColumn = Convert.ToInt32(Console.ReadLine());
        } while (startingColumn < 0 || startingColumn >= BOARD_SIZE);

        var lastPosition = new Position(startingLine, startingColumn);

        PlaceQueen(lastPosition);
        PrintBoard(lastPosition);

        while (!CheckIfAllLinesHaveQueens()){
            lastPosition.DefineNextPositions(chessboard);

            var nextPosition = lastPosition.NextPositions.FirstOrDefault();

            while (nextPosition is null){
                RemoveQueen(lastPosition);
                PrintBoard(lastPosition, isRemoving: true);
                var lastButOnePosition = positions.Last();
                lastButOnePosition.RemoveInvalidNextPosition(lastPosition);
                lastPosition = lastButOnePosition;
                nextPosition = lastPosition.NextPositions.FirstOrDefault();
            }

            PlaceQueen(nextPosition!);
            PrintBoard(nextPosition);

            lastPosition = nextPosition;
        }

        Console.WriteLine("{0} steps were made to get to this solution.", stepsCount);
    }

    static bool CheckIfAllLinesHaveQueens(){
        for (int i = 0; i < BOARD_SIZE; i++){
            var anyQueens = false;

            for (int j = 0; j < BOARD_SIZE; j++){
                if (chessboard[i,j] == PLACED_QUEEN){
                    anyQueens = true;
                }
            }

            if (!anyQueens) return false;
        }

        return true;
    }

    static void PlaceQueen(Position position){
        chessboard[position.Line, position.Column] = PLACED_QUEEN;
        positions.Add(position);
    }

    static void RemoveQueen(Position position){
        chessboard[position.Line, position.Column] = EMPTY;
        positions.Remove(position);
    }

    static void PrintBoard(Position position, bool isRemoving = false){
        int rowLength = chessboard.GetLength(0);
        int colLength = chessboard.GetLength(1);
        stepsCount++;

        switch (isRemoving){
            case true:
                Console.WriteLine("Removing from: [{0},{1}]", position.Line, position.Column);
                break;
            default:
                Console.WriteLine("Placing at: [{0},{1}]", position.Line, position.Column);
                break;
        }

        for (int i = 0; i < rowLength; i++)
        {
            Console.Write("[ ");

            for (int j = 0; j < colLength; j++)
            {
                Console.Write(string.Format("{0} ", chessboard[i, j]));
            }
            
            Console.Write("]" + Environment.NewLine);
        }

        Console.Write(Environment.NewLine + Environment.NewLine);
        Thread.Sleep(1000);
    }
}
