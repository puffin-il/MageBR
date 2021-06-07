using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMonsterScript : MonoBehaviour
{

    public GameObject player;
    //public GameObject shootingPointOne;
    private Rigidbody2D spiderRB;
    private float rollSpeed = 10f;
    private Animator animator;
    public float alertDistance = 5f;
    public float distanceFromPlayer;
    public bool lockOnPLayer = false;
    public Vector3 targetPosition;
    public float rollInertia = 3f;
    public float health=100f;
    private bool isStuned;
    private Vector3 rollDirection;
    public GameObject damageMNGObj;
    private DamageMNGScript damageMngScript;
    public GameObject spawnManager;


    const string SPIDER_IDLE = "SpiderIdle";
    const string SPIDER_STUN = "SpiderStun";
    const string SPIDER_TO_ROLL = "SpiderToRoll";
    const string SPIDER_ROLL = "SpiderRoll";

    // Start is called before the first frame update
    void Start()
    {
        spiderRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.Play(SPIDER_IDLE);
        isStuned = false;
        health = 100f;
        damageMngScript = damageMNGObj.GetComponent<DamageMNGScript>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);

        if ((distanceFromPlayer <= alertDistance)&&!lockOnPLayer&&!animator.GetCurrentAnimatorStateInfo(0).IsName(SPIDER_STUN))
        {
            
            lockOnPLayer = true;
            targetPosition = player.transform.position+ (player.transform.position - transform.position).normalized*rollInertia; 
            rollDirection = (targetPosition- transform.position).normalized;
            animator.Play(SPIDER_TO_ROLL);
            isStuned = false;
            
            
        }

        if ((lockOnPLayer == true) && (animator.GetCurrentAnimatorStateInfo(0).IsName(SPIDER_ROLL)))
        {
            transform.position += rollDirection * Time.deltaTime * rollSpeed;
        }

        if ((Vector3.Distance(targetPosition, transform.position) < 0.2f)&& lockOnPLayer)
        {
            lockOnPLayer = false;
            SpiderStuned();
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            spawnManager.GetComponent<GameManagerScript>().EnamyDead();
        }

        

       
    }

    public void StartRollAnimation()
    {
        animator.Play(SPIDER_ROLL);
        isStuned = false;
    }

    public void StartIdleAnimation()
    {

        
        lockOnPLayer = false;
        animator.Play(SPIDER_IDLE);
        isStuned = false;
        
    }

    void SpiderStuned()
    {
        spiderRB.velocity = new Vector2(0, 0);
        
        animator.Play(SPIDER_STUN);
        isStuned = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        
        
            if (collision.gameObject.tag == "Wall")
        {
            
            SpiderStuned();
        }
        if ((collision.gameObject.tag == "FireBall") && isStuned)
        {
            
            
            health -= damageMngScript.GetFireBallDMG();
        }

        if ((collision.gameObject.tag == "Haduken") && isStuned)
        {
            

            health -= damageMngScript.GetHadukenDMG();
        }
        if (collision.gameObject.tag == "Player")
        {

            Debug.Log("touch player player");
        }


    }

    private void OnParticleCollision(GameObject other)
    {
        if ((other.gameObject.name == "FireBreath"&&isStuned))
        {
            Debug.Log("hit by fb");

            health -= damageMngScript.GetFireBreathDPS();
        }
    }

    public void setHealth(float h)
    {
        health = h;
    }


}
