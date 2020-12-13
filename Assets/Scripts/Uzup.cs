using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Uzup : MonoBehaviour
{
    public Text clickCount;
    public Text poziom;
    public Text czas;
    public Text totalpoints;


    private void Awake()
    {
        clickCount.text = PlayerPrefs.GetInt("clickcount", 0).ToString();
        poziom.text = PlayerPrefs.GetString("poziom", "Easy");
        czas.text = PlayerPrefs.GetString("textTimer", "0:0");
        totalpoints.text = PlayerPrefs.GetFloat("totalPoints", 0).ToString();
    }
    

}
