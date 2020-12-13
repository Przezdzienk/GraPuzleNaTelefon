using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Przyciski : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Easy()
    {
        PlayerPrefs.SetInt("blocksPerLine", 4);
        PlayerPrefs.SetInt("shuffleLength", 30);
        PlayerPrefs.SetString("poziom","Easy");

    }
    public void Hard()
    {
        PlayerPrefs.SetInt("blocksPerLine", 8);
        PlayerPrefs.SetInt("shuffleLength", 60);
        PlayerPrefs.SetString("poziom", "Hard");
    }

    public void ZapiszObraz(GameObject gameObject)
    {
        PlayerPrefs.SetString("image", gameObject.GetComponent<RawImage>().texture.name);
    }
    //Nie wiem jak sie odniesc do tablicy
    //public void ZapiszKtory(GameObject gameObject)
    // {
    //   PlayerPrefs.SetInt("ktory", );
    // }
    public void OdPocz()
    {
        SceneManager.LoadScene(0);
    }
    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void jej()
    {
        PlayerPrefs.SetInt("blocksPerLine", 6);
        PlayerPrefs.SetInt("shuffleLength", 45);
        PlayerPrefs.SetString("poziom", "jej");
    }
    
}
