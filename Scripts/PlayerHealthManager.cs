using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{

    public GameObject healthBar;
    public GameObject gameManager;
    
    private float maxHealth=100f;
    private float health;
    public float healthIncrese = 1f;
    private bool touchFirePuddle=false;
    private PlayerMovement playerMovment;
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        playerMovment = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.GetComponent<HealthBarScript>().SetHealth(health);

        if (health < maxHealth)
        {
            health += healthIncrese * Time.deltaTime;
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health <= 0)
        {
            health = 0f;
        }

        if (touchFirePuddle)
        {
            health -= 15f*Time.deltaTime;
        }

        if (health <= 0)
        {
            PlayerDead();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.tag == "Spider Monster")
        {
            health -= 40f;
            Debug.Log("Mnster spider touch");
        }

        if (collision.gameObject.tag == "Ghost")
        {
            health -= 60f;
            Debug.Log("Ghost touch");
        }
        if (collision.gameObject.tag == "Ninja Projectile")
        {
            health -= 20f;
            Debug.Log("nP touch");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FirePuddle")
        {
            touchFirePuddle = true;
            playerMovment.moveSpeed *= 0.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FirePuddle")
        {
            touchFirePuddle = false;
            playerMovment.moveSpeed *= 2f;
        }
    }

    private void PlayerDead()
    {
        Destroy(gameObject);
        gameManager.GetComponent<GameManagerScript>().GameOver();
        Destroy(gameObject);
    }

    public void AddHealth(float add)
    {
        health +=add;
    }

}
