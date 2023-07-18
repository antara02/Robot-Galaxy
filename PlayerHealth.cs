using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    float health;
    [SerializeField] float maxHealth;

    public Animator anim;

    private bool hasKey;

    public Slider healthBar;
    public GameManager gameManager;

    void Start()
    {
        health = maxHealth;
        healthBar.value = health/maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f) Die();
        healthBar.value = health/maxHealth;
    }

    private void Die() {
        anim.SetTrigger("die");
        Invoke("gamemgrded", 3f);
    }

    private void gamemgrded(){
        gameManager.dead();
    }

    public void gotKey(){
        hasKey = true;
    }

    public bool checkKey(){
        return hasKey;
    }

    public void heal(){
        health = maxHealth;
        healthBar.value = health/maxHealth;
    }
}
