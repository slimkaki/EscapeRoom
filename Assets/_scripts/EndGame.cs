using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndGame : MonoBehaviour
{
    // public static bool GameEnd = false;
    // public GameObject EndGameUI;

    // void Update()
    // {
    //     Debug.Log("Acabou o jogo!");
    //     EndGameFunc();
    // } 
    // void EndGameFunc(){
    //     // Cursor.lockState = CursorLockMode.None; 
    //     // EndGameUI.SetActive(true);
    //     // Time.timeScale = 0f;
    //     // PlayerVision.endGameBool = false;
    //     Debug.Log("loadMenu");
    //     SceneManager.LoadScene(2); 
    // }
    public void LoadMenu(){
        Debug.Log("loadMenu");
        SceneManager.LoadScene(0);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame(){
        Debug.Log("quit");
        Application.Quit();
    }

}
