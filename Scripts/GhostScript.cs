using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScript : MonoBehaviour
{

    public GameObject player;
    public float distanceFromPlayer;
    public float health = 100f;
    public GameObject damageMNGObj;
    public float rangeToChase = 5f;
    public float ghostSpeed = 2f;
    private DamageMNGScript damageMngScript;
    public GameObject spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
        damageMngScript = damageMNGObj.GetComponent<DamageMNGScript>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
        //Debug.Log(distanceFromPlayer);
        if (distanceFromPlayer <= rangeToChase)
        {
            //Debug.Log("in range");
            transform.position += (player.transform.position - transform.position).normalized * Time.deltaTime * ghostSpeed;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
            spawnManager.GetComponent<GameManagerScript>().EnamyDead();
        }

        
        transform.rotation = Quaternion.Euler(0, (player.transform.position.x < transform.position.x) ? 0 : 180, 0);
        


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {



        if (collision.gameObject.tag == "Wall")
        {
            //Debug.Log("hit WAll");
            
        }
        if ((collision.gameObject.tag == "FireBall") )
        {
           // Debug.Log("hit by fire ball");

            health -= damageMngScript.GetFireBallDMG();
        }

        if ((collision.gameObject.tag == "Haduken") )
        {
            //Debug.Log("hit by haduken");

            health -= damageMngScript.GetHadukenDMG();
        }


    }
    private void OnParticleCollision(GameObject other)
    {
        if ((other.gameObject.name == "FireBreath"))
        {
            Debug.Log("hit by fb");

            health -= damageMngScript.GetFireBreathDPS();
        }
    }
}
