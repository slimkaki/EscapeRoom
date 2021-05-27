// TUTORIAL USADO: https://www.youtube.com/watch?v=_QajrabyTJc&t=16s, por brakeys
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerVision : MonoBehaviour {

    public float mouseSense = 100f;
    public Transform playerBody;
    public GameObject lanterna;
    private Camera camera;
    // [SerializeField]
    // public GameObject player;
    // GameManager gm;

    float xRotation = 0f;
    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        camera = gameObject.GetComponent<Camera>();
        // gm = GameManager.GetInstance();
        lanterna.SetActive(false);
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
        
        // Cursor.lockState = CursorLockMode.Locked;
        float mouseX = Input.GetAxis("Mouse X") * mouseSense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSense * Time.deltaTime;

        xRotation -=mouseY;

        xRotation = Mathf.Clamp(xRotation, -70.0f, 70.0f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);

        
       
    }

     void LateUpdate()

        {
        RaycastHit hit;
        Debug.DrawRay(camera.transform.position, transform.forward*4, Color.magenta);
        if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 100.0f))
        {
            
            if(hit.collider.tag=="card" && Input.GetMouseButtonDown(0)){
                Destroy(hit.transform.gameObject);
                Debug.Log($"Peguei o objeto: {hit.collider.name}");
            }
            if(hit.collider.tag=="paraquedas" && Input.GetMouseButtonDown(0)){
                Destroy(hit.transform.gameObject);
                Debug.Log($"Peguei o objeto: {hit.collider.name}");
            }
            if(hit.collider.tag=="tool" && Input.GetMouseButtonDown(0)){
                Destroy(hit.transform.gameObject);
                Debug.Log($"Peguei o objeto: {hit.collider.name}");
            }
            if(hit.collider.tag=="lanterna" && Input.GetMouseButtonDown(0)){
                Destroy(hit.transform.gameObject);
                Debug.Log($"Peguei o objeto: {hit.collider.name}");
                lanterna.SetActive(true);
            }
        }
    }
       
}
