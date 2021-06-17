using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] Game gameManager;
    
    private void RestartCells(){
        gameManager.RestartCells();
    }
}
