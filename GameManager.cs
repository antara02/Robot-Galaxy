using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // public GameObject gameOverCanvas;
    public Canvas winCanvas;
    public Canvas deadCanvas;

    private void Start() {
        Time.timeScale = 1;
        deadCanvas.enabled = false;
        Cursor.visible = false;
        winCanvas.enabled = false;
    }

    public void returnMenu(){
        SceneManager.LoadScene("mainMenu");
    }

    public void dead(){
        Time.timeScale = 0;
        Cursor.visible = true;
        deadCanvas.enabled = true;
    }

    public void restartLevel(){
        SceneManager.LoadScene("Level 1");
    }

    public void win(){
        winCanvas.enabled = true;
        Time.timeScale=0;
    }
}
