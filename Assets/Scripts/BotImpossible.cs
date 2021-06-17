using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotImpossible : MonoBehaviour
{
    [SerializeField] TestBot testBot;
    [SerializeField] Game gameManager;
    [SerializeField] float maxTimeWait;
    [SerializeField] float minTimeWiat;

    Dictionary<string, int> scoreValues = new Dictionary<string, int>();

    void Start(){
        scoreValues.Add("X", -1);
        scoreValues.Add("O", 1);
        scoreValues.Add("tie", 0);

    }
    public IEnumerator Play(Cell[] board){
        float timeWait = Random.Range(minTimeWiat, maxTimeWait);
        yield return new WaitForSeconds(timeWait);
        BestMove(board);
        gameManager.SetPlayerTurn(true);
    }



    private void BestMove(Cell [] board){

        int bestScore = int.MinValue;
        Cell bestMove = null;
        //check all board cells
        for(int i = 0; i < 9; i++){
            if(board[i].IsEmpty()){
                board[i].SetCellValue(1);
                int score = Minimax(board, 0, false);
                board[i].SetCellValue(0);
                if(score > bestScore){
                    bestScore = score;
                    bestMove = board[i];
                }
            }
        }
        bestMove.SetOActive();
        //test
        if(!gameManager.CheckGameOver() && gameManager.GetBotPlaying()){
            testBot.RandomMove(board);
        }

       
    }

    //Returns the score of that move
    //score of 1 means we win, 0 tie, -1 lose
    //board is the current positioning of the tokens (or how the tokens would be placed when looking at future positions)
    //depth is the current depth in the tree
    //maximizer is true if it's maximizers turn
    private int Minimax(Cell[] board, int depth, bool maximizer){

        string checkWinner = gameManager.CheckWinner(board);
        //Check if there is a winner or if there is a tie
        //this is the final state of the tree (a leaf)
        if(checkWinner != null){
            int score = scoreValues[checkWinner];
            return score;
        }
        if(maximizer){
            int bestScore = int.MinValue;
            for(int i = 0; i < 9; i++){
                if(board[i].IsEmpty()){
                    board[i].SetCellValue(1);
                    int score = Minimax(board, depth + 1, false); //next turn is minimizer
                    board[i].SetCellValue(0);
                    bestScore = Mathf.Max(score, bestScore);
                }
                
            }
            return bestScore;
        }
        //if minimizer
        else{
            int worstScore = int.MaxValue;
            for(int i = 0; i < 9; i++){
                if(board[i].IsEmpty()){
                    board[i].SetCellValue(-1);
                    int score = Minimax(board, depth + 1, true); //next turn is maximizer
                    board[i].SetCellValue(0);
                    worstScore = Mathf.Min(score, worstScore);
                }
            }
            return worstScore;
        }
    }
}
