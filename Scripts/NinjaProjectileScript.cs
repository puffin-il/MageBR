using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaProjectileScript : MonoBehaviour
{
    public GameObject hitmarker;
   
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitmarker, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(effect, 1f);
    }
}
