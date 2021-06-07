using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePuddleScript : MonoBehaviour
{

    public float timeToWait = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine (Wait());  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Wait()
    {
        
        yield return new WaitForSeconds(timeToWait);
        
        Destroy(gameObject);
    }
}
