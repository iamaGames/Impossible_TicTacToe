using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowStats : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI wins;
    [SerializeField] TextMeshProUGUI losses;
    [SerializeField] TextMeshProUGUI ties;

    // Start is called before the first frame update
    void Start()
    {
        wins.text = "Wins: " + PlayerPrefs.GetInt("wins").ToString();
        losses.text = "Losses: " + PlayerPrefs.GetInt("losses").ToString();
        ties.text = "Ties: " + PlayerPrefs.GetInt("ties").ToString();
    }

}
