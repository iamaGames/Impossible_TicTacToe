using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [Header("TEST variables")]
    [SerializeField] bool botPlaying;
    [SerializeField] TestBot testBot;
    [Header("Board Elements")]
    [SerializeField] GameObject[] player1cells;
    [SerializeField] GameObject[] player2cells;
    [SerializeField] Cell[] cells;
    [SerializeField] BotImpossible bot;


    [Header("UI Animators")]
    [SerializeField] Animator gridAnim;
    [SerializeField] Animator botWinsAnim;
    [SerializeField] Animator youWinAnim;
    [SerializeField] Animator tieAnim;

    //Restart variables
    [SerializeField] float restartTime;
    private bool gameOver = false;

    private int[] MagicSquare = { 4, 9, 2, 3, 5, 7, 8, 1, 6};

    //Control variables
    private bool playerTurn = true;
    // Start is called before the first frame update
    void Start()
    {
        RestartCells();
         if(botPlaying){
            testBot.RandomMove(GetBoard());
        }
    }

    private void RestarStats(){
        PlayerPrefs.SetInt("losses", 0);
        PlayerPrefs.SetInt("wins", 0);
        PlayerPrefs.SetInt("ties", 0);
    }

    public bool GetPlayerTurn(){
        return playerTurn;
    }

    public void SetPlayerTurn(bool turn){
        this.playerTurn = turn;
    }

    //Checks if game is over
    public bool CheckGameOver(){
        string winner = CheckWinner(GetBoard());
        if(winner != null){
            gameOver = true;
            //Check if bot won
            if(winner == "O"){
                botWinsAnim.SetTrigger("gameover");
                PlayerPrefs.SetInt("losses", PlayerPrefs.GetInt("losses")+1);
            }
            else if(winner == "X"){
                //hahaha, yeah this ain't happening boyyy
                youWinAnim.SetTrigger("gameover");
                PlayerPrefs.SetInt("wins", PlayerPrefs.GetInt("wins")+1);
            }
            else if(winner == "tie"){
                tieAnim.SetTrigger("tie");
                PlayerPrefs.SetInt("ties", PlayerPrefs.GetInt("ties")+1);
            }
            //restart
            StartCoroutine(Restart());
            return true;
        }

        

        return false;
    }

    public string CheckWinner(Cell[] board){
        int sum = 0;
        for(int x = 0; x < 9; x++){
            for(int y = 0; y < 9; y++){
                for(int z = 0; z < 9; z++){
                    if(x != y && y!= z && z!= x){

                        //Check that all cells have same value 
                        //(all 1 or all -1, so sum should be 3 or -3)
                        sum = cells[x].GetCellValue()+
                                cells[y].GetCellValue() +
                                cells[z].GetCellValue();
                        if(Mathf.Abs(sum) == 3){
                             sum = cells[x].GetCellValue() * MagicSquare[x] +
                                cells[y].GetCellValue() * MagicSquare[y] +
                                cells[z].GetCellValue() * MagicSquare[z];
                            if(sum == -15){
                                return "X";
                            }
                            else if(sum == 15){
                                return "O";
                            }
                        }
                       
                    }
                   
                }
            }
        }
         //Check if there is a tie
        sum = 0;
        for(int i = 0; i < 9; i++){ 
            if (cells[i].GetCellValue() == 1 || cells[i].GetCellValue() == -1){
                sum += 1;
            }
        }
        if(sum == 9){
           
            return "tie";
        }

        return null;
    }


    private IEnumerator Restart(){
        yield return new WaitForSeconds(restartTime);
        SetPlayerTurn(true);
        
        gridAnim.SetTrigger("restart");
        if(botPlaying){
            yield return new WaitForSeconds(3);
            testBot.RandomMove(GetBoard());
        }
    }

    public void RestartCells(){
        for(int i = 0; i < player1cells.Length; i++)
        {
            player1cells[i].SetActive(false);
            player2cells[i].SetActive(false);
            cells[i].SetCellValue(0);
        }
        gameOver = false; //reset game over so cells can be used
    }

    public bool GetGameOver(){
        return gameOver;
    }

    public Cell[] GetBoard(){
        return cells;
    }
    public Cell[,] GetBoardMatrix(){
        Cell[,] boardMatrix = new Cell[3,3];
        for(int i = 0; i <3; i++){
            boardMatrix[0,i] = cells[i];
            boardMatrix[1,i] = cells[3 + i];
            boardMatrix[2,i] = cells[6 + i];
        }
        return boardMatrix;
    }

    public void BotTurn(){
        SetPlayerTurn(false);
        StartCoroutine(bot.Play(GetBoard()));
    }

    public bool GetBotPlaying(){
        return botPlaying;
    }
}
