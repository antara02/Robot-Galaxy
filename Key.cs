using UnityEngine;
using UnityEngine.UI;

class Key : MonoBehaviour 
{
    bool playerNearKey = false;
    public PlayerHealth player;

    public Canvas interactCanvas;

    private void Start() {
        interactCanvas.enabled = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            playerNearKey = true;
            interactCanvas.enabled = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            playerNearKey = false;
            interactCanvas.enabled = false;
        }
    }

    void Update()
    {
        if(playerNearKey && !player.checkKey() && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Key Obtained");
            player.gotKey();
            Destroy(this.gameObject);
            interactCanvas.enabled = false;
            player.heal();
        }
    }

}