using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBot : MonoBehaviour
{
    
    [SerializeField] Game gameManager;

    public void RandomMove(Cell [] board){

        StartCoroutine(Play(board));
        
       
    }

    private IEnumerator Play(Cell[] board){
        yield return new WaitForSeconds(0.3f);
        if(gameManager.GetPlayerTurn()){
            //choose a random cell
            int random = Random.Range(0, 9);
            while(!board[random].IsEmpty()){
                random = Random.Range(0, 9);
            }
            
            board[random].SetXActive();
            if(!gameManager.CheckGameOver()){
                gameManager.BotTurn();
            }
        }
    }

    
}
