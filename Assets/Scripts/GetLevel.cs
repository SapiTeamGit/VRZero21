using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class GetLevel : MonoBehaviour
{
    public List<GameObject> levels = new List<GameObject>();
    public TextMeshPro tm;
    public int nr;

    public GameObject nextLevel;
    [SerializeField] private Transform spawnPoint;



    public void Awake()
    {
        GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
        for (int i = 0; i < gos.Length; i++) {
            if (gos[i].name.Contains("Level"))
            {
                levels.Add(gos[i]);
            }
        } 
            
        if (levels.Count > 0)
        {
            levels.Sort(delegate (GameObject a, GameObject b)
            {
                return (a.name).CompareTo(b.name);
            });
        }
        tm.text = levels[0].name;

        string resultString = Regex.Match(levels[0].name, @"\d+").Value;
        nr = Int32.Parse(resultString);
        nr++;
    }

    public void Start()
    {
        nextLevel = Resources.Load("Assets/Prefabs/Levels/Level " + nr) as GameObject;
        Debug.Log("the next level is: " + nextLevel.name + "ment");
    }

    public void Update()
    {
        CheckIfGameEnded();
    }

    private void CheckIfGameEnded()
    {
        
        //Debug.Log("The actual level is" + nr);
        if (levels[0].transform.childCount == 0)
        {
            Debug.Log("Game Ended!");
            Destroy(levels[0]);
        }
    }


}