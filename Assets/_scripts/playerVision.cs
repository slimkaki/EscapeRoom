// TUTORIAL USADO: https://www.youtube.com/watch?v=_QajrabyTJc&t=16s, por brakeys
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        RaycastHit hit;
        Debug.DrawRay(camera.transform.position, transform.forward*4, Color.magenta);
        if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 100.0f))
        {
            
            if(hit.collider.tag=="card" && Input.GetMouseButtonDown(0)){
                Destroy(hit.transform.gameObject);
                items[0] = true; 
                Debug.Log($"Peguei o objeto: {hit.collider.name}");
            }
            if(hit.collider.tag=="paraquedas" && Input.GetMouseButtonDown(0)){
                Destroy(hit.transform.gameObject);
                items[3] = true;
                Debug.Log($"Peguei o objeto: {hit.collider.name}");
            }
            if(hit.collider.tag=="tool" && Input.GetMouseButtonDown(0)){
                Destroy(hit.transform.gameObject);
                items[2] = true;
                Debug.Log($"Peguei o objeto: {hit.collider.name}");
            }
            if(hit.collider.tag=="lanterna" && Input.GetMouseButtonDown(0)){
                Destroy(hit.transform.gameObject);
                items[1] = true; 
                Debug.Log($"Peguei o objeto: {hit.collider.name}");
                lanterna.SetActive(true);
                lanternaIsOn = true;
            }
            if(hit.collider.tag=="objetivo" && Input.GetMouseButtonDown(0)){
                if (items[3]) {
                    Debug.Log("Fim de Jogo");
                    Cursor.lockState = CursorLockMode.None; 
                    SceneManager.LoadScene(2); 
                    
                } else {
                    Debug.Log("Hint: Você precisa de um paraquedas");
                }
            }
            if(hit.collider.tag=="paraquedasCard" && Input.GetMouseButtonDown(0)){
                if (items[0]) {
                    Debug.Log("Fim de Jogo");
                    Cursor.lockState = CursorLockMode.None; 
                    SceneManager.LoadScene(2); 
                    
                } else {
                    Debug.Log("Hint: Você precisa de um paraquedas");
                }
            }
            if(hit.collider.tag=="cabine" && Input.GetMouseButtonDown(0)){
                if (items[2]) {
                    Debug.Log("Fim de Jogo");
                    Cursor.lockState = CursorLockMode.None; 
                    SceneManager.LoadScene(2); 
                    
                } else {
                    Debug.Log("Hint: Você precisa de um paraquedas");
                }
            }
        }
    }
       
}
