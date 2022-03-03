using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetValueFromParent : MonoBehaviour
{
    public TextMesh tm;
    public int value;

    void Start()
    {
        value = GetComponentInParent<CardObject>().value;
        tm = GetComponent<TextMesh>();
        tm.text = value.ToString();
    }

}
