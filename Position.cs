namespace eight_queens;
using static Constants;

public class Position {
    public int Line { get; set; }
    public int Column { get; set; }
    public List<Position> NextPositions { get; set; }

    public Position(int line, int column) {
        Line = line;
        Column = column;
        NextPositions = [];
    }

    public void RemoveInvalidNextPosition(Position position){
        NextPositions.Remove(position);
    }

    public void DefineNextPositions(char[,] chessboard){
        for (int i = 0; i < BOARD_SIZE; i++){
            var nextPosition = new Position(Line + 1, i);

            if (!CheckIfTrapped(nextPosition, chessboard)){
                NextPositions.Add(nextPosition);
            }
        }
    }

    private bool CheckIfTrapped(Position position, char[,] chessboard){
        for (int i = 0; i < BOARD_SIZE; i++){
            if (chessboard[i, position.Column] == PLACED_QUEEN)
                return true;
        }

        for (int i = 0; i < BOARD_SIZE; i++){
            if (chessboard[position.Line, i] == PLACED_QUEEN)
                return true;
        }

        for (int i = 1; i < BOARD_SIZE; i++){
            if (position.Line + i < BOARD_SIZE && position.Column + i < BOARD_SIZE && chessboard[position.Line + i, position.Column + i] == PLACED_QUEEN)
                return true;

            if (position.Line + i < BOARD_SIZE && position.Column - i >= 0 && chessboard[position.Line + i, position.Column - i] == PLACED_QUEEN)
                return true;

            if (position.Line - i >= 0 && position.Column - i >= 0 && chessboard[position.Line - i, position.Column - i] == PLACED_QUEEN)
                return true;

            if (position.Line - i >= 0 && position.Column + i < BOARD_SIZE && chessboard[position.Line - i, position.Column + i] == PLACED_QUEEN)
                return true;
        }

        return false;
    }
}