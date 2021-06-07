using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoenixScript : MonoBehaviour
{

    public GameObject player;
    private Animator animator;
    public float health=100f;
    public GameObject damageMNGObj;
    private DamageMNGScript damageMngScript;
    public GameObject smallBird;
    [SerializeField]
    private float reviveMaxDelay=6f;
    [SerializeField]
    private float reviveMinDelay=4f;
    private bool reviveActive = false;
    [SerializeField]
    private float smallBirdsRate = 1f;
    private bool shotReady = true;
    public GameObject playerButtom;
    private bool canShoot = true;
    public GameObject spawnManager;



    const string PHEONIX_IDLE = "PhoenixIdle";
    const string REVIVE = "PhoenixRevive";
    
    
    // Start is called before the first frame update
    void Start()
    {
        damageMngScript = damageMNGObj.GetComponent<DamageMNGScript>();
        animator = GetComponent<Animator>();
        animator.Play(PHEONIX_IDLE);
        smallBirdsRate = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, (player.transform.position.x < transform.position.x) ? 180 : 0, 0);

        if (health <= 0)
        {
            Destroy(gameObject);
            spawnManager.GetComponent<GameManagerScript>().EnamyDead();
        }

        if (!reviveActive)
        {
            StartCoroutine(StartRevive(Random.Range(reviveMinDelay, reviveMaxDelay)));
        }

        if (shotReady)
        {
            StartCoroutine(ShootBird());
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {



        if (collision.gameObject.tag == "Wall")
        {
            //Debug.Log("hit WAll");

        }
        if ((collision.gameObject.tag == "FireBall"))
        {
            //Debug.Log("hit by fire ball");

            health -= damageMngScript.GetFireBallDMG();
        }

        if ((collision.gameObject.tag == "Haduken"))
        {
            //Debug.Log("hit by haduken");

            health -= damageMngScript.GetHadukenDMG();
        }

        if ((collision.gameObject.name == "FireBreath"))
        {
            Debug.Log("hit by fb");

            health -= damageMngScript.GetFireBreathDPS();
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

    private void Revive()
    {
        canShoot = false;
        animator.Play(REVIVE);
    }
    private void PlayIdle()
    {
        canShoot = true;
        animator.Play(PHEONIX_IDLE);
    }

    private void Shoot()
    {
        Instantiate(smallBird, transform.position, transform.rotation);
    }

    private IEnumerator StartRevive(float delayTime)
    {
        reviveActive = true;
        
        yield return new WaitForSeconds(delayTime);
       
        reviveActive = false;
        Revive();

    }

    private IEnumerator ShootBird()
    {
        //Debug.Log("coroutine start");
        shotReady = false;
        yield return new WaitForSeconds(smallBirdsRate);
        //Debug.Log("coroutine yield");
        if (canShoot)
        {
            GameObject sb = Instantiate(smallBird, transform.position, Quaternion.identity);
            sb.GetComponent<SmallBirdScript>().player = playerButtom;
        }
        shotReady = true;
    }

    public void setHealth(float h)
    {
        health = h;
    }
}
