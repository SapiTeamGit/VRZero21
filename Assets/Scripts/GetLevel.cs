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
    public AudioSource GameOverAudio;
    public ParticleSystem ParticleSystemWin;

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
        if (ScoreManager.CheckIfGameOverByScoreValue() == false)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);
        InitializeLevel();
    }

    IEnumerator GameOver()
    {
        Debug.Log("Game Over Called");
        GameOverAudio.enabled = true;
        yield return new WaitForSeconds(2);
        //Game Over zene
        GameOverAudio.enabled = false;
        Destroy(levels[0]);
        LoadGame();
    }

    private void CheckIfGameEnded()
    {
        try
        {
            //Debug.Log("The actual level is" + nr);
            if (levels[0].transform.childCount == 0)
            {

                Debug.Log("Game Ended!");
                Destroy(levels[0]);
                nextLevel.transform.localScale = new Vector3(0.5f, 1f, 0.5f);

                //StartParticleSystem
                ParticleSystemWin.Play();
                //stop particle system after 2 sec

                DoDelayAction(2);
            
            }
        }
        catch
        {
            if(nr == 11)
            {
                nr = 1;
                StartCoroutine(Waiter());
            }
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

    void DoDelayAction(float delayTime)
    {
        StartCoroutine(DelayAction(delayTime));
    }

    IEnumerator DelayAction(float delayTime)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);
        if (nr == 11)
        {
            nr = 1;
            actualLevel = nr;
            Debug.Log("The game reseted the levels, there are no other levels");
            PlayerPrefs.SetInt("SavedLevel", nr);
            PlayerPrefs.Save();
            
        }
        else
        {
            
            PlayerPrefs.SetInt("SavedLevel", nr - 1);
            PlayerPrefs.Save();
        }

        InitializeLevel();
    }
}