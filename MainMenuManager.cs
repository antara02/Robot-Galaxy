using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Start() {
        Time.timeScale = 1;
    }

    public void StartGame(){
        SceneManager.LoadScene("Level 1");
    }

    public void QuitGame(){
        Application.Quit();
    }
}
