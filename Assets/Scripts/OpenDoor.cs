using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Animator animator;
    Animator playerAnimator;
    bool triggered;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerAnimator = GameObject.Find("VrBase").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !playerAnimator.IsInTransition(0))
        {
            playerAnimator.enabled = false;
        }
    }
    public void Activate()
    {
        if(!triggered)
        {
            animator.SetTrigger("Activate");
            StartCoroutine(StartWalking(3));
            triggered = true;
        }
      
    }
    IEnumerator StartWalking(float seconds)
    {
        //Print the time of when the function is first called.
        //Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(seconds);

        //After we have waited 3 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        playerAnimator.enabled = true;
        playerAnimator.SetTrigger("Play");
        
    }
}
