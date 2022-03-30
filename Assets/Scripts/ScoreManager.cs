using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static int score = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
