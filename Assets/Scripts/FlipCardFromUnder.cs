using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlipCardFromUnder : MonoBehaviour
{
    static List<GameObject> cards;

    public void Awake()
    {
        cards = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name.Contains("CardAnimation")).ToList();
    }

    // Update is called once per frame
    public void FlipTopDeleteBottom()
    {
        var nClosest = cards.OrderBy(t => Vector3.Distance(t.transform.position, this.transform.position))
                           .Take(2).ToArray();
        foreach(var card in nClosest)
        {
            Debug.Log(card.name);
        }
        nClosest[1].transform.GetChild(0).GetComponent<Animator>().SetTrigger("CardFromTopSelected");
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
        cards.Remove(nClosest[0]);
        // Debug.Log("FlipDeleteCalled");
    }

    private void OnDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
}
