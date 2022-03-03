using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    public GameObject card;
    public int value;
    public CardObject(int value, GameObject card)
    {
        this.value = value;
        this.card = card;
    }

    private void Start()
    {
        GameObject instance = Instantiate(card, new Vector3(0, 0, 0), Quaternion.identity);
        instance.GetComponent<CardObject>().value = 0;
    }
}
