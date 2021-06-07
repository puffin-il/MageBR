using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private PlayerAnimationScript animationScript;
    public Vector2 movement;
    public float dashSpeed=20f;
    public float dashTime=1f;
    public Camera cam;
    public bool isDash = false;
    public GameObject shootingPointOne;
    [SerializeField]
    private float dashManaCost = 10f;

    public ParticleSystem dust;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animationScript = GetComponent<PlayerAnimationScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            animationScript.MovmentAnimation(movement);
        
        if ((Input.GetKeyDown(KeyCode.Space))&&(shootingPointOne.GetComponent<PowersScript>().GetCurrentMana()>=dashManaCost))
        {
            //Debug.Log("corutine called");
            shootingPointOne.GetComponent<PowersScript>().SetCurrentMana(-dashManaCost);
            StartCoroutine(DashCoroutine());
        }

    }

    private void FixedUpdate()
    {
        if (isDash == false)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    IEnumerator DashCoroutine()
    {
        isDash = true;
        dust.Play();
        //trailRenderer.emitting = true;
        //Debug.Log("dash");
        rb.velocity = movement * dashSpeed;
        yield return new WaitForSeconds(dashTime);
        isDash = false;
        
        //trailRenderer.emitting = false;
        rb.velocity = new Vector2(0, 0);
    }

}
