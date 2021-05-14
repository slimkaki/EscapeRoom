using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerVision : MonoBehaviour {

    public float mouseSense = 100f;
    public Transform playerBody;
    [SerializeField]
    // public GameObject player;
    // GameManager gm;

    float xRotation = 0f;
    void Start(){

        // gm = GameManager.GetInstance();
    }
    void Update(){
        // if(gm.gameState != GameManager.GameState.GAME) {
        //     Cursor.lockState = CursorLockMode.None;
        //     return;
        // } else {
        //     // Debug.Log("To locked");
        //     Cursor.lockState = CursorLockMode.Locked;
        // }

        // if(Input.GetKeyDown(KeyCode.Escape) && gm.gameState == GameManager.GameState.GAME) {
        //     gm.ChangeState(GameManager.GameState.PAUSE);
        // }
        
        Cursor.lockState = CursorLockMode.Locked;
        float mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;

        xRotation -=mouseY;

        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);

       
    }
}