// TUTORIAL USADO: https://www.youtube.com/watch?v=_QajrabyTJc&t=16s, por brakeys
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class playerVision : MonoBehaviour {

    public float mouseSense = 100f;
    public Transform playerBody;
    public GameObject lanterna;
    public GameObject luzLanterna;
    private Camera camera;
    // 0 - Card ; 1 - Lanterna ; 2 - Tool ; 3 - Paraquedas
    private bool[] items = {false, false, false, false};
    private bool lanternaIsOn;
    public bool endGameBool = false;
    
    [SerializeField]
    private Animator paraquedasDoor, cabineDoor1, cabineDoor2;
    private Text hint;

    // [SerializeField]
    // public GameObject player;
    // GameManager gm;

    float xRotation = 0f;
    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        camera = gameObject.GetComponent<Camera>();
        // gm = GameManager.GetInstance();
        lanterna.SetActive(false);
        items[0] = false;
        items[1] = false;
        items[2] = false;
        items[3] = false;
        endGameBool = false;
        hint = GameObject.FindWithTag("HintText").GetComponent<Text>();
        hint.enabled = false;
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

        if (Input.GetKeyDown(KeyCode.F) && items[1]) {
            if (lanternaIsOn) {
                luzLanterna.SetActive(false);
                lanternaIsOn = false;
            } else {
                luzLanterna.SetActive(true);
                lanternaIsOn = true;
            }
        }
       
    }

     void LateUpdate()
        {
        hint.enabled = false;

        RaycastHit hit;
        Debug.DrawRay(camera.transform.position, transform.forward, Color.magenta);
        if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 5.0f))
        {
            
            if(hit.collider.tag=="card"){
                if (Input.GetMouseButtonDown(0)) {
                    Destroy(hit.transform.gameObject);
                    items[0] = true; 
                    Debug.Log($"Peguei o objeto: {hit.collider.name}");
                }
                hint.text = "Mouse Left Click to pick up the card";
                hint.enabled = true;
                
            }
            if(hit.collider.tag=="paraquedas"){
                if (Input.GetMouseButtonDown(0)) {
                    Destroy(hit.transform.gameObject);
                    items[3] = true;
                    Debug.Log($"Peguei o objeto: {hit.collider.name}");
                }
            
                hint.text = "Mouse Left Click to pick up the parachute";
                hint.enabled = true;
            }
            if(hit.collider.tag=="tool"){
                if (Input.GetMouseButtonDown(0)) {
                    Destroy(hit.transform.gameObject);
                    items[2] = true;
                    Debug.Log($"Peguei o objeto: {hit.collider.name}");
                }
                hint.text = "Mouse Left Click to pick up the card";
                hint.enabled = true;
            }
            if(hit.collider.tag=="lanterna"){
                if (Input.GetMouseButtonDown(0)) {
                    Destroy(hit.transform.gameObject);
                    items[1] = true; 
                    Debug.Log($"Peguei o objeto: {hit.collider.name}");
                    lanterna.SetActive(true);
                    lanternaIsOn = true;
                }
                // Debug.Log($"Cursor lock state: {Cursor.lockState}");
                // if (Cursor.lockState == "Locked") {
                hint.text = "Mouse Left Click to pick up the flashlight";
                hint.enabled = true;
                // }
            }
            if(hit.collider.tag=="objetivo"){
                hint.text = "Mouse Left Click to open door";
                hint.enabled = true;

                if (Input.GetMouseButtonDown(0) && items[3]) {
                    Debug.Log("Fim de Jogo");
                    Cursor.lockState = CursorLockMode.None; 
                    SceneManager.LoadScene(2); 
                    
                } else if (Input.GetMouseButtonDown(0)) {
                    // Colocar um tempo para isso aparecer na tela
                    // Do jeito que tá, vai flicar na tela só
                    hint.text = "Dean: I better get a parachute to jump the plane...";
                    hint.enabled = true;
                    // Debug.Log("Hint: Você precisa de um paraquedas");
                }
            }

            if(hit.collider.tag=="paraquedasCard"){
                hint.enabled = true;
                if (!items[0]){
                    hint.text = "Dean: I guess i need a key to open this door...";
                } else {
                    hint.text = "Mouse Left Click to open the door";
                    Debug.Log($"Mouse: {Input.GetMouseButtonDown(0)}");
                    if (Input.GetMouseButtonDown(0)) {
                        Debug.Log("Abrir porta paraquedas");
                        paraquedasDoor.SetBool("OpenDoor", true);
                    } 
                }
            }
            if(hit.collider.tag == "cabine1"){
                if (!items[2]) {
                    hint.text = "Dean: I need a key to open this door...";
                } else {
                    hint.text = "Mouse Left Click to open the door...";
                
                }
                hint.enabled = true;
                if (Input.GetMouseButtonDown(0) && items[2]) {
                    cabineDoor1.SetBool("OpenDoor1", true);
                    hint.enabled = false;
                } else {
                    // cabineDoor1.SetBool("OpenDoor1", false);
                    Debug.Log("Hint1: Você precisa de um CARD CABINE");
                }
            }

            if (hit.collider.tag == "cabine2") {
                if (!items[2]) {
                    hint.text = "Dean: I need a key to open this door...";
                } else {
                    hint.text = "Mouse Left Click to open the door...";
                
                }
                hint.enabled = true;
                if (Input.GetMouseButtonDown(0) && items[2]) {
                    cabineDoor2.SetBool("OpenDoor2", true);
                    hint.enabled = false;
                } else {
                    // cabineDoor2.SetBool("OpenDoor2", false);
                    Debug.Log("Hint2: Você precisa de um CARD CABINE");
                }
            }
        }
    }
       
}
