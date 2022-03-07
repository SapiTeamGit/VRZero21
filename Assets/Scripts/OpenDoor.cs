using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Activate()
    {
        animator.SetTrigger("Activate");
        StartWalking(3);
    }
    IEnumerator StartWalking(float seconds)
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(seconds);

        //After we have waited 3 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        var playerAnimator = GameObject.Find("VrBase").GetComponent<Animator>();
        playerAnimator.enabled = true;
        playerAnimator.SetTrigger("Play");
    }
}
