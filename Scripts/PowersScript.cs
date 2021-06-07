using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.Mathematics;
using UnityEngine;

public class PowersScript : MonoBehaviour
{

    public GameObject player;
    
    public GameObject manaBar;
    private Vector2 mousePos;
   
    public Camera cam;
    public float angle;
    private Rigidbody2D rb;
    private Vector2 rotationVector;
    private LineRenderer lineRenderer;
    public Material[] laserMaterials;
    private int laserMaterialIndex = 0;
    public int powerState;
    private Quaternion relativeRotation;
    private float leftButtonCountdown = 0;
    private float rightButtonCountdown = 0;
    private bool leftButtonpress = false;
    private bool rightButtonpress = false;

    //Mana Setup
    private float maxMana = 100f;
    private float currentMana;
    [SerializeField]
    private float manaIncrese = 5f;


    //FireBall Setup
    public GameObject fireBallPrefab;
    public float fireBallForce = 20f;
    public float fireBallManaCost = 10f;
    


    //FireBreath Setup
    public ParticleSystem firebreathPC;
    private bool firebreathActive = false;
    public float firebreathManaCost = 100f;
    

    //Ice Shield Setup
    public GameObject iceShield;
    public float iceShieldManaCost = 50f;
    public float iceShieldHealthIncrease = 40f;

    //Haduken Setup
    public GameObject hadukenPrefab;
    [SerializeField]
    private float hadukenForce=10;
    [SerializeField]
    private float hadukenChanelTime = 2f;
    public ParticleSystem hadukenChargePS;
    public float hadukenManaCost = 25f;


    // Start is called before the first frame update

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        firebreathPC.GetComponent<ParticleSystem>();
        firebreathPC.Stop();
        hadukenChargePS.GetComponent<ParticleSystem>();
        hadukenChargePS.Stop();
        manaBar.GetComponent<ManaBarScript>().SetMaxMana(maxMana);
        currentMana = maxMana;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true;
        powerState = 1;
        
       
    }

        // Update is called once per frame
        void Update()
    {
        transform.position = player.transform.position;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
        ManaFill();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            powerState = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            powerState = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)){
            powerState = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            powerState = 4;
        }
        
       

        if (Input.GetButtonDown("Fire1"))
        {
            ShootOne();
            leftButtonpress = true;
        }
        if (Input.GetButtonDown("Fire2"))
        {
            ShootTwo();
            rightButtonpress = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            leftButtonCountdown = 0;
            leftButtonpress = false;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            DeactiveShootTwo();
            rightButtonCountdown = 0;
            rightButtonpress = false;
        }

        if (hadukenChargePS.isEmitting && (leftButtonpress==false))
        {
            Debug.Log(hadukenChargePS.isEmitting);
            Debug.Log("stop stop stop");
            hadukenChargePS.Stop();
        }


        PressCounter();
        
    }

    private void FixedUpdate()
    {

        Vector2 lookDir = mousePos - rb.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
        rotationVector = new Vector2(Mathf.Cos((angle + 90f) * Mathf.Deg2Rad), Mathf.Sin((angle + 90f) * Mathf.Deg2Rad));
        relativeRotation.eulerAngles = new Vector3(0, 0, angle + 90f);
    }

    private void ShootOne()
    {
        
        if (powerState == 1)
        {
            ShootFireBall();
        }

        if (powerState == 2)
        {
            StartCoroutine(StartHaduken());
        }
    }

    private void ShootTwo()
    {
        if (powerState == 1)
        {
            ActivateFireBreath();
            firebreathActive = true;
        }
        if (powerState == 2)
        {
            ActiveIceShield();
        }
    }


    void ManaFill()
    {
        if (currentMana < maxMana)
        {
            currentMana += manaIncrese * Time.deltaTime;
        }
        if ((firebreathActive == true)&&(currentMana>0))
        {
            currentMana -= firebreathManaCost * Time.deltaTime;
        }


        manaBar.GetComponent<ManaBarScript>().SetMana(currentMana);
        if (currentMana <= 0)
        {
            DeactiveShootTwo();
        }
    }

    void ShootLaser()
    {
        int playerLayer = 9;
        Quaternion relativeRotation = new Quaternion();
        relativeRotation.eulerAngles = new Vector3(0, 0, angle + 90f);
        RaycastHit2D hit = Physics2D.Raycast(rb.position,rotationVector,Mathf.Infinity, playerLayer);
        Debug.DrawLine(rb.position, hit.point);
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, rb.position);
        lineRenderer.SetPosition(1, hit.point);
        
        
        
        
    }
    void LaserMateralIndexChanger()
    {
        if (laserMaterialIndex == (laserMaterials.Length - 1))
        {
            laserMaterialIndex = 0;
        }
        else laserMaterialIndex++;
        lineRenderer.material = laserMaterials[laserMaterialIndex];
    }

    void ShootFireBall()
    {
        if (currentMana > fireBallManaCost)
        {
            GameObject fireBall = Instantiate(fireBallPrefab, rb.position, relativeRotation);
            Rigidbody2D fireBallRb = fireBall.GetComponent<Rigidbody2D>();
            fireBallRb.AddForce(rotationVector * fireBallForce, ForceMode2D.Impulse);
            currentMana -= fireBallManaCost;
        }
    }

    void ShootHaduken()
    {

        if (currentMana > hadukenManaCost)
        {
            GameObject haduken = Instantiate(hadukenPrefab, rb.position, relativeRotation);
            Rigidbody2D hadukenRb = haduken.GetComponent<Rigidbody2D>();
            hadukenRb.AddForce(rotationVector * hadukenForce, ForceMode2D.Impulse);
            hadukenChargePS.Stop();
            currentMana -= hadukenManaCost;
        }

    }

    void ActivateFireBreath()
    {
        firebreathPC.Play();
    }

    void DeactiveShootTwo()
    {
        if ((powerState == 1)||(currentMana<0))
        {
            firebreathPC.Stop();
            firebreathActive = false;
        }
    }

    void ActiveIceShield()
    {
        if (currentMana >= iceShieldManaCost)
        {
            Instantiate(iceShield, player.transform.position, player.transform.rotation);
            currentMana =currentMana -iceShieldManaCost;
            player.GetComponent<PlayerHealthManager>().AddHealth(iceShieldHealthIncrease);
        }
    }

    public float GetCurrentMana()
    {
        return currentMana;
    }

    public void SetCurrentMana(float manaChange)
    {
        currentMana += manaChange;
    }



    private void PressCounter()
    {
        if (rightButtonpress == true)
        {
            rightButtonCountdown += Time.deltaTime;
        }
        if (leftButtonpress == true)
        {
            leftButtonCountdown += Time.deltaTime;
        }
    }

    private IEnumerator StartHaduken()
    {
        hadukenChargePS.Play();
        Debug.Log("should play");
        yield return new WaitForSeconds(hadukenChanelTime);
        if (leftButtonCountdown >= hadukenChanelTime)
        {
            ShootHaduken();
           
        }
    }

 
}
