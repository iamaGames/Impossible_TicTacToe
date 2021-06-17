using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] GameObject x;
    [SerializeField] GameObject o;

    //Control variables
    private int cellValue = 0; //stores 1 if O, and -1 if X
    //suscribete si estás leyendo esto :)

    //Components
    [SerializeField] Game gameManager;

    void start(){
        cellValue = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerPlay(){
        //Check if cell is empty, if player is playing and if game hasnt finished
        if(IsEmpty() && !gameManager.GetGameOver() && gameManager.GetPlayerTurn()){
            SetXActive();
            gameManager.SetPlayerTurn(false);
            if(gameManager.CheckGameOver()){
                Debug.Log("Game Over");
            }
            else{
                //switch turns
                gameManager.BotTurn();
            }
        }
    }

    public bool IsEmpty(){
        if(GetCellValue() == 0){
            return true;
        }
        else{
            return false;
        }
    }

    public int GetCellValue(){
        return cellValue;
    }

    public void SetCellValue(int cellValue){
        this.cellValue = cellValue;
    }

    public void SetOActive(){
        o.SetActive(true);
        SetCellValue(1);
    }

    public void SetXActive(){
        x.SetActive(true);
        SetCellValue(-1);
    }

    public void RestartCell(){
        x.SetActive(false);
        o.SetActive(false);
        SetCellValue(0);
    }
}
