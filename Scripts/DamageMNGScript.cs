using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMNGScript : MonoBehaviour
{


    [SerializeField]
    private float fireBallDMG = 25f;
    [SerializeField]
    private float fireBreathDPS = 5f;
    [SerializeField]
    private float hadukenDMG = 50f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetFireBallDMG()
    {
        return fireBallDMG;
    }

    public void SetFireBallDMG(float newdmg)
    {
        fireBallDMG = newdmg;
    }

    public float GetFireBreathDPS()
    {
        Debug.Log("get fb dps called");
        return fireBreathDPS;
    }

    public void SerFireBreathDPS(float newdmg)
    {
        fireBreathDPS = newdmg;
    }

    public float GetHadukenDMG()
    {
        return hadukenDMG;
    }

    public void SetHadukenDMG(float newdmg)
    {
        hadukenDMG = newdmg;
    }






}
