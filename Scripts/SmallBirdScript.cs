using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBirdScript : MonoBehaviour
{

    public GameObject player;
    [SerializeField]
    private float speed = 6f;
    private Vector3 playerPosition;
    public GameObject firePuddle;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.Euler(0, (playerPosition.x < transform.position.x) ? 180 : 0, 0);

        transform.position += (playerPosition - transform.position).normalized * Time.deltaTime * speed;
        if (Vector3.Distance(playerPosition, transform.position) <= 0.02f)
        {
            Instantiate(firePuddle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
