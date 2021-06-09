// https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;
public class playerController : MonoBehaviour {
    private CharacterController controller;
    public Camera camera;
    private Vector3 velocity;
    // private bool groundedPlayer;
    private float playerSpeed = 3.0f;
    // private float jumpHeight = 1.0f;
    private float gravity = -9.81f;
    [SerializeField]
    public animationState anim;
    public AudioSource playerWhistle;
    
    void Start() {
        // Cursor.lockState = CursorLockMode.Locked;
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
            playerSpeed = 7f;          
        }else {
            playerSpeed = 5f;
        }
        
        if (Input.GetKey("e")){
            NearEnemies();
            playerWhistle.Play();
        }

       
        

        controller.Move(move * Time.deltaTime * playerSpeed);

        velocity.y += gravity * Time.deltaTime;
      
        controller.Move(velocity * Time.deltaTime);
    }

    private void NearEnemies() {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach(GameObject enemy in enemies) {
            if (Vector3.Distance(this.transform.position, enemy.transform.position) <= 35f) {
                float xR = Random.Range(-5f, 5f);
                float yR = Random.Range(-2.5f, 2.5f);
                float zR = Random.Range(-5f, 5f);

                Vector3 noise_pos = new Vector3(this.transform.position.x+xR, this.transform.position.y+yR, this.transform.position.z+zR);
                enemy.GetComponent<enemyController>().walkPointSet = true;
                enemy.GetComponent<enemyController>().lastTimeIWalked = Time.time;
                enemy.GetComponent<enemyController>().walkPoint = noise_pos;
            }
        }
    }
   
}
