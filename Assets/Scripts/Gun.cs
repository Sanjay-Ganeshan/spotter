using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour {

    public Transform ShootingPos;
    public float MaxRange = 1000f;
    public GameObject Projectile;

    private bool hasFired = false;
    private LineRenderer laser;

    private RaycastHit laserHit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateLaser();
	}


    private void Awake()
    {
        this.laser = GetComponentInChildren<LineRenderer>();
        if(ShootingPos == null)
        {
            ShootingPos = this.transform;
        }
    }

    

    void UpdateLaser()
    {
        bool isHit = Physics.Raycast(new Ray(laser.transform.position, laser.transform.forward), out laserHit, MaxRange, LayerMask.GetMask("Shootable"));
        float distanceToRender = MaxRange;

        if (isHit)
        {
            distanceToRender = laserHit.distance;
        }
        this.laser.SetPosition(1,Vector3.forward * distanceToRender);
    }

    public void Shoot()
    {
        hasFired = true;
        GameObject spawned = GameObject.Instantiate(Projectile, ShootingPos.position + ShootingPos.forward * 1.0f, ShootingPos.transform.rotation);
        Projectile firedProjectile = spawned.GetComponentInChildren<Projectile>();
        firedProjectile.Fire(ShootingPos.forward);
    }

    internal void TriggerDown()
    {
        if(!hasFired)
        {
            // Any shooting logic here
            Shoot();
        }
    }

    internal void TriggerUp()
    {
        if(hasFired)
        {
            // Any reset logic here
            hasFired = false;
        }
    }
}
