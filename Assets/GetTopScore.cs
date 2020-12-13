using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetTopScore : MonoBehaviour
{
    private string _TextureName;
    private Text _TopScoreText;

    // Start is called before the first frame update
    void Start()
    {
        _TextureName = transform.GetChild(0)?.GetComponent<RawImage>()?.texture?.name;
        _TopScoreText = transform.GetChild(2)?.GetComponent<Text>();
        if (_TopScoreText != null)
        {
            _TopScoreText.text = PlayerPrefs.GetFloat(_TextureName, 0).ToString();
        }
    }
}
