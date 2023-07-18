using UnityEngine;
using UnityEngine.UI;

class Lock : MonoBehaviour 
{
    bool playerNearLock = false;
    public PlayerHealth player;

    public Canvas canvas;
    public GameObject keyObj;

    public GameManager gameManager;

    private void Start() {
        canvas.enabled = false;
        keyObj.SetActive(false);
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            playerNearLock = true;
        canvas.enabled = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            playerNearLock = false;
            canvas.enabled = false;
        }
    }

    void Update()
    {
        if(playerNearLock && player.checkKey() && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Lock Opens");
            canvas.enabled = false;
            keyObj.SetActive(true);
            gameManager.win();
        }
    }

}