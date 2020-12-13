using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollCos : MonoBehaviour
{
    public RectTransform Panel;
    public Button[]  bttn;
    public RectTransform center;
    public float[] distReposition;
    public int ileprzy;

    public float[] distanc;
    private bool dragging = false;
    private int bttnDistance;
    private int minButtonNum;
    private int bttnLength;
    private int odleglosckoncow;
    void Start()
    {
        bttnLength = bttn.Length;
        distanc = new float[bttnLength];
        distReposition = new float[bttnLength];

        bttnDistance=(int)Mathf.Abs(bttn[1].GetComponent<RectTransform>().anchoredPosition.x - bttn[0].GetComponent<RectTransform>().anchoredPosition.x);
        if (ileprzy <3)
            odleglosckoncow = 7000;
        else
            odleglosckoncow = 2600;
    }

    void Update()
    {
        for(int i=0; i < bttn.Length; i++)
        {
            distReposition[i] = center.GetComponent<RectTransform>().position.x - bttn[i].GetComponent<RectTransform>().position.x;
            distanc[i] = Mathf.Abs(distReposition[i]);

            

            if(distReposition[i]>odleglosckoncow)
            {
                float curX = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2(curX + (bttnLength * bttnDistance), curY);
                bttn[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
            }
            if (distReposition[i]<(odleglosckoncow *-1))
            {
                float curX = bttn[i].GetComponent<RectTransform>().anchoredPosition.x;
                float curY = bttn[i].GetComponent<RectTransform>().anchoredPosition.y;

                Vector2 newAnchoredPos = new Vector2(curX - (bttnLength * bttnDistance), curY);
                bttn[i].GetComponent<RectTransform>().anchoredPosition = newAnchoredPos;
            }
        }

        float minDistance = Mathf.Min(distanc);

        for(int a=0;a <bttn.Length; a++)
        {
            if(minDistance==distanc[a])
            {
                minButtonNum = a;
            }
        }
        if (!dragging)
        {
            //   LerpToBttn(minButtonNum * -bttnDistance);
            LerpToBttn (-bttn[minButtonNum].GetComponent<RectTransform>().anchoredPosition.x);
        }
    }
    void LerpToBttn(float position)
    {
        float newX = Mathf.Lerp(Panel.anchoredPosition.x, position, Time.deltaTime * 2f);
        Vector2 newPosition = new Vector2(newX, Panel.anchoredPosition.y);

        Panel.anchoredPosition = newPosition;
    }
    public void StartDrag()
    {
        dragging = true;
    }
    public void EndDrag()
    {
        dragging = false;
    }
}
