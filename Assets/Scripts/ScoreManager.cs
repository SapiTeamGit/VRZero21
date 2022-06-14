using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class ScoreManager : MonoBehaviour
{
    static int score = 10;

    // Start is called before the first frame update
    void Start()
    {
        Screen.brightness = 1;
    }

    public void ModifyScore(GameObject card)
    {
        CardObject cardObject = card.GetComponent<CardObject>();
        //Debug.Log("ModifyScore called");
        score += cardObject.value;
        GameObject tm = GameObject.Find("Score");
        if(!tm)
        {
            return;
        }
        else
        {
            tm.GetComponent<ShowScore>().ShowScoree(score);
        }
    }
    public static bool CheckIfGameOverByScoreValue()
    {
        if(score < 0 || score > 21)
        {
            
            score = 10;
            GameObject tm = GameObject.Find("Score");
            tm.GetComponent<ShowScore>().ShowScoree(score);
            return false;
        }
        else
        {
            return true;
        }
    }
    
}
