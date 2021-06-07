using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{

    public GameObject hitEffect;
    private Collider2D colliderO;
    
    // Start is called before the first frame update

    private void Start()
    {
        colliderO = GetComponent<Collider2D>();
        Physics2D.IgnoreLayerCollision(8, 9);
    }


    private void Update()
    {
         
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        

        if (collision.gameObject.tag == "Player")
        {
           //Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), colliderO,true);
           // Physics2D.IgnoreLayerCollision(8, 9);

            Debug.Log(colliderO);
            Debug.Log(collision.gameObject.GetComponent<Collider2D>());
        }

        if (collision.gameObject.tag != "Player")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(effect, 3f);
        }


       
    }

    
    
}
