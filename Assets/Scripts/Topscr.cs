using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Topscr : MonoBehaviour
{
    public float pobieraj;
    public int ktory;
    public Text[] topScore;
    private float staryrekord;

    private void Awake()
    {
        ktory=PlayerPrefs.GetInt("ktory");
        pobieraj = PlayerPrefs.GetFloat("totalPoints");
    }
    /* public void Set()
    {
        staryrekord = topScore[ktory].Tofloat();
        if (staryrekord < pobieraj)
            topScore[ktory] = pobieraj;
    }
    */

}
