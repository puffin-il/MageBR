using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadesSymbolScript : MonoBehaviour
{
    public GameObject gameManager;
    
    
    
   private void DestroyMe()
    {
        gameManager.GetComponent<GameManagerScript>().CreateEnamy(transform.position);
        Destroy(gameObject);
    }
}
