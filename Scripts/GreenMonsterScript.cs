using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMonsterScript : MonoBehaviour
{

    public float speed = 3f;
    private Rigidbody2D enemyRb;
    private GameObject player;
    private float health = 100;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        transform.position += lookDirection * Time.deltaTime * speed;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        enemyRb.rotation = angle;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FireBall")
        {
            health -= 25f;
            Debug.Log("hit");
        }
    }
}
