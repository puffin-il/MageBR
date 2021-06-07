using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NInjaScript : MonoBehaviour
{

    public GameObject player;
    const string NINJA_IDLE = "NinjaIdle";
    const string NINJA_SHOOTING = "NinjaShooting";
    const string NINJA_BLUR = "NinjaBlur";
    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField]
    private float speed = 6f;
    [SerializeField]
    private float alertDistance = 6f;
    private bool isShooting = false;
    private bool isIdle = true;
    private bool isBlur=false;
    private Vector3 destination;
    public GameObject ninjaStar;
    public GameObject Shurikan;
    [SerializeField]
    private float projectileForce = 1f;
    private DamageMNGScript damageMngScript;
    public GameObject spawnManager;
    public GameObject damageMNGObj;
    [SerializeField]
    private float health = 100f;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        animator.Play(NINJA_IDLE);
        
        damageMngScript = damageMNGObj.GetComponent<DamageMNGScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isIdle && PlayerIsClose())
        {
            StartShooting();
        }
        if (isBlur)
        {
            if (Vector3.Distance(destination, transform.position) > 0.1)
            {
                transform.position += (destination - transform.position).normalized * Time.deltaTime * speed;
            }
            else EndOfBlur();
        }

        if (health <= 0)
        {
            
            Destroy(gameObject);
            spawnManager.GetComponent<GameManagerScript>().EnamyDead();
        }
    }



    private Vector3 GetPoint()
    {
        bool run = true;
        float randomDistance=0;
        float randomX=0;
        float randomY=0;
        while (run)
        {
            randomDistance = Random.Range(1, 10);
            randomX = Random.Range(-1f, 1f);
            randomY = Random.Range(-1f, 1f);
            Vector2 vec = new Vector2(randomX, randomY);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, vec, randomDistance, 2);
            if (hit.collider == null)
            {
                run = false;
            }
        }

            Vector3 returnPoint = new Vector3(randomX, randomY,0) * randomDistance;
            return returnPoint;
        }

    private void StartIdle()
    {
        animator.Play(NINJA_IDLE);
        isShooting = false;
        isIdle = true;
        isBlur = false;
}
    private void StartBlur()
    {
        animator.Play(NINJA_BLUR);
        isShooting = false;
        isIdle = false;
        isBlur = true;
        destination = GetPoint();

    }

    private void StartShooting()
    {
        animator.Play(NINJA_SHOOTING);
        isShooting = true;
        isIdle = false;
        isBlur = false;
    }

    private bool PlayerIsClose()
    {
        return(Vector3.Distance(player.transform.position, transform.position) < alertDistance);
       
    }

    private void EndOfBlur()
    {
        if (PlayerIsClose())
        {
            StartShooting();
        }
        else StartIdle();
    }

    private void Shoot()
    {
        int r = Random.Range(1,3);
        Vector3 shootDirection = (player.transform.position - transform.position).normalized;
        if (r == 1)
        {
            GameObject star = Instantiate(ninjaStar, transform.position, Quaternion.identity);
            Rigidbody2D starRb = star.GetComponent<Rigidbody2D>();
            starRb.AddForce(shootDirection * projectileForce, ForceMode2D.Impulse);
            Physics2D.IgnoreCollision(star.GetComponent<Collider2D>(),GetComponent<Collider2D>());
        }
        if (r == 2)
        {
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg - 90f;
            Quaternion relativeRotation = new Quaternion();
            relativeRotation.eulerAngles = new Vector3(0, 0, angle +135f);
            GameObject star = Instantiate(Shurikan, transform.position, relativeRotation);
            Rigidbody2D starRb = star.GetComponent<Rigidbody2D>();
            starRb.AddForce(shootDirection * projectileForce, ForceMode2D.Impulse);
            Physics2D.IgnoreCollision(star.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {



        if (collision.gameObject.tag == "Wall")
        {
            //Debug.Log("hit WAll")

        }
        if ((collision.gameObject.tag == "FireBall"))
        {
            // Debug.Log("hit by fire ball");

            health -= damageMngScript.GetFireBallDMG();
        }

        if ((collision.gameObject.tag == "Haduken"))
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
