using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    public int value;
    public TextMeshPro tm;

    private void Start()
    {

    }

    private void Awake()
    {
        value = Random.Range(-9, 9);
        tm.text = value.ToString(); 
    }
}
