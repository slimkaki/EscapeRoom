// https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;
public class playerController : MonoBehaviour {
    private CharacterController controller;
    public Camera camera;
    private Vector3 velocity;
    private bool groundedPlayer;
    private float playerSpeed = 3.0f;
    private float jumpHeight = 1.0f;
    private float gravity = -9.81f;
    [SerializeField]
    public animationState anim;
    
    void Start() {
        controller = gameObject.GetComponent<CharacterController>();
        // camera = gameObject.GetComponent<Camera>();
        // anim = GetComponent<animationState>();
    }

    void Update() {
        
        anim.forwardPressed = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");
        anim.runPressed = anim.forwardPressed && Input.GetKey("left shift");

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward*z;

        if (Input.GetButton("Fire3")){
            // Time.timeScale = 0;
            playerSpeed = 5f;          
        }else {
            playerSpeed = 3f;
        }

        if (Input.GetKeyDown(KeyCode.Escape)){
            Time.timeScale = 0;
            
          
         }//else {
        //     Time.timeScale = 1;
        // }
        

        controller.Move(move * Time.deltaTime * playerSpeed);

        velocity.y += gravity * Time.deltaTime;
      
        controller.Move(velocity * Time.deltaTime);
    }
   
}
