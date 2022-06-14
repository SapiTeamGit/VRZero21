using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FlipCardFromUnder : MonoBehaviour
{

    // Update is called once per frame
    public void FlipTopDeleteBottom()
    {
        if (!gameObject.transform.parent.parent.name.Contains("Level"))
        {
            gameObject.transform.parent.parent.GetChild(0).GetComponent<Animator>().SetTrigger("CardFromTopSelected");
        }
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
        waiter();
    }

    private void OnDestroy()
    {
        Destroy(transform.parent.gameObject);
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(2);
        
    }
}
