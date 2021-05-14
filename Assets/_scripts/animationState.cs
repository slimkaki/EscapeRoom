using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationState : MonoBehaviour
{
    Animator animator;
    int isWalkingHash,isRunningHash;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash  = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        Debug.Log(animator);
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w");

        bool isRunning = animator.GetBool(isRunningHash);
        bool runPressed = Input.GetKey("left shift");

        // idle -> walk
        if(!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash,true);
        }
        if(isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash,false);
        }
        
        // walk -> run
        if(!isRunning && (forwardPressed && runPressed))
        {
            animator.SetBool(isRunningHash,true);
        }
        if(isRunning && (forwardPressed || runPressed))
        {
            animator.SetBool(isRunningHash,false);
        }
        
    

    }
}
