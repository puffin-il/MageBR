using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDomeScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;



    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(spriteRenderer.sprite.name=="ice dome25")
        {
            Destroy(gameObject);
        }
    }
}
