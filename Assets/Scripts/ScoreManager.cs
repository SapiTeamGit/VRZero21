using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class ScoreManager : MonoBehaviour
{
    static int score = 10;

    GetLevel getLevel;

    public GameObject nextLevel;
    [SerializeField] private Transform spawnPoint;
    public List<GameObject> actualLevel = new List<GameObject>();
    int numberOfLevel;

    // Start is called before the first frame update
    void Start()
    {
        Screen.brightness = 1;
        getLevel = GameObject.FindGameObjectWithTag("Level").GetComponent<GetLevel>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfScoreIsCorrect();
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
    public void CheckIfScoreIsCorrect()
    {
        if(score < 0 || score > 21)
        {
            //reload level
            GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
            for (int i = 0; i < gos.Length; i++)
            {
                if (gos[i].name.Contains("Level "))
                {
                    actualLevel.Add(gos[i]);
                }
            }

            if (actualLevel.Count > 0)
            {
                actualLevel.Sort(delegate (GameObject a, GameObject b)
                {
                    return (a.name).CompareTo(b.name);
                });
            }

            numberOfLevel = Int32.Parse(Regex.Match(actualLevel[0].name, @"\d+").Value);

            //Debug.Log($"The actual level is {numberOfLevel}");
            Destroy(actualLevel[0]);
            score = 10;
            GameObject tm = GameObject.Find("Score");
            if (!tm)
            {
                return;
            }
            else
            {
                tm.GetComponent<ShowScore>().ShowScoree(score);
            }
            getLevel.LoadGame();
        }
    }
    
}
