using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class GetLevel : MonoBehaviour
{
    public List<GameObject> levels = new List<GameObject>();
    public TextMeshPro tm;

    public void Awake()
    {
        GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));
        for (int i = 0; i < gos.Length; i++)
            if (gos[i].name.Contains("Level"))
            {
                levels.Add(gos[i]);
            }
        if (levels.Count > 0)
        {
            levels.Sort(delegate (GameObject a, GameObject b)
            {
                return (a.name).CompareTo(b.name);
            });
        }
        tm.text = levels[0].name;
    }
}