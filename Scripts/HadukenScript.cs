using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadukenScript : MonoBehaviour
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



       if (collision.gameObject.tag != "Player")
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
