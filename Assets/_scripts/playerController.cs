// https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    [SerializeField]
    public animationState anim;
    
    void Start() {
        controller = gameObject.GetComponent<CharacterController>();
        // anim = GetComponent<animationState>();
    }

    void Update() {
        
        anim.forwardPressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");
        anim.runPressed = anim.forwardPressed && Input.GetKey("left shift");

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward*z;

        if (Input.GetButton("Fire3")){
            playerSpeed = 4f;          
        }else {
            playerSpeed = 2f;
        }

        controller.Move(move * Time.deltaTime * playerSpeed);

        
    }
}
