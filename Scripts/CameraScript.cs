using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public float followSpeed = 0.4f;
    //public float yOffset = 3f;
    public float xBoundry = 3f;
    public float yBoundry = 4f;
    public float jj=5;
    public bool cameraOnPLayer = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

            if ((Mathf.Abs(transform.position.x - player.transform.position.x) > xBoundry))
            {

                if (player.transform.position.x - transform.position.x > 0)
                {

                    transform.position = new Vector3(player.transform.position.x - xBoundry, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(player.transform.position.x + xBoundry, transform.position.y, transform.position.z);
                }
            }



            if ((Mathf.Abs(transform.position.y - player.transform.position.y) > yBoundry))
            {

                if (player.transform.position.y - transform.position.y > 0)
                {

                    transform.position = new Vector3(transform.position.x, player.transform.position.y - yBoundry, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, player.transform.position.y + yBoundry, transform.position.z);
                }
            }







        }
    
}
