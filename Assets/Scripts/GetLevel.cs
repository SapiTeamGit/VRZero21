using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class GetLevel : MonoBehaviour
{
    public List<GameObject> levels = new List<GameObject>();
    public TextMeshPro tm;
    public int nr;
    public int actualLevel;

    public GameObject nextLevel;
    [SerializeField] private Transform spawnPoint;


    public void Awake()
    {
        GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
        for (int i = 0; i < gos.Length; i++)
        {
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
        levels = levels.Skip(nr).ToList();
    }

    public void Start()
    {
        LoadGame();
        nextLevel = Resources.Load("Prefabs/Levels/Level " + nr) as GameObject;
        //Debug.Log("the next level is: " + nextLevel.name + "ment");
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
            nextLevel.transform.localScale = new Vector3(0.5f, 1f, 0.5f);

            nextLevel = Instantiate(nextLevel, spawnPoint.position, spawnPoint.rotation);
            if (nextLevel.name.Contains("(Clone)"))
            {
                int pos = nextLevel.name.IndexOf("(Clone)");
                nextLevel.name = nextLevel.name.Remove(pos);
            }
            levels[0] = nextLevel;

            tm.text = nextLevel.name;
            nextLevel = Resources.Load($"Prefabs/Levels/Level {++nr}") as GameObject;
            PlayerPrefs.SetInt("SavedLevel", nr - 1);
            PlayerPrefs.Save();
        }
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            nr = PlayerPrefs.GetInt("SavedLevel");
            actualLevel = nr;
            Debug.Log("Game data loaded!");
        }
        else
        {
            nr = 1;
            actualLevel = nr;
            Debug.LogError("There is no save data!");
        }

        InitializeLevel();
    }

    private void InitializeLevel()
    {
        nextLevel = Resources.Load("Prefabs/Levels/Level " + nr) as GameObject;
        nextLevel.transform.localScale = new Vector3(0.5f, 1f, 0.5f);

        nextLevel = Instantiate(nextLevel, spawnPoint.position, spawnPoint.rotation);
        if (nextLevel.name.Contains("(Clone)"))
        {
            int pos = nextLevel.name.IndexOf("(Clone)");
            nextLevel.name = nextLevel.name.Remove(pos);
        }
        levels[0] = nextLevel;

        tm.text = nextLevel.name;
        nextLevel = Resources.Load($"Prefabs/Levels/Level {++nr}") as GameObject;
    }
}