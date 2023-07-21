using ChessChallenge.API;
using System;

public class MyBot : IChessBot
{
    int[] pieceValues = { 0, 100, 250, 300, 500, 900, 10000 };
    public Move Think(Board board, Timer timer)
    {
        Move[] allMoves = board.GetLegalMoves();
        Random rng = new();

        int evalC = eval(board);
        //System.Console.WriteLine("Momentane Eval am Anfang:" + evalC);
        Move safe = allMoves[0];

        foreach(Move move in allMoves){
            int i = eval(board);
            board.MakeMove(move);
            System.Console.WriteLine(board.GetFenString());
            System.Console.WriteLine(eval(board));
            i = eval(board);
            if(evalC > eval(board)){
                safe = move;
                //System.Console.WriteLine("Zunkunft" + eval(board));
            }
            board.UndoMove(move);
        }
        System.Console.WriteLine("----------------------------");
        return safe;
    }

    /*private Move calcMove(Board b, int evalC, Move bestMove){
        Move[] allMoves = b.GetLegalMoves();
        foreach (Move move in allMoves){
            b.MakeMove(move);
            //System.Console.WriteLine(eval(b));
            if(evalC < eval(b)){
                System.Console.WriteLine("Zunkunft" + eval(b));
            }
            b.UndoMove(move);
        }
        return bestMove;
    }*/

    private int eval(Board b){
        Move[] legalMoves = b.GetLegalMoves();
        PieceList[] pList = b.GetAllPieceLists();
        int output = 0;
        for(int i = 0; i <= 63; i++){
            Square temp = new Square(i);
            //System.Console.WriteLine("Figur" + pieceValues[(int)b.GetPiece(temp).PieceType]);
            if(b.GetPiece(temp).IsWhite){
                output = output + pieceValues[(int)b.GetPiece(temp).PieceType];
            }else{
                output = output - pieceValues[(int)b.GetPiece(temp).PieceType];
            }
        }
        return output; 
    }
}