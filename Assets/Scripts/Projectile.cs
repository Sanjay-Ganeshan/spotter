using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

    public float Lifetime = 2.0f;
    public float Speed = 10.0f;
    public float Power = 1.0f;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    internal void Fire(Vector3 forward)
    {
        GameObject.Destroy(this.gameObject, Lifetime);
        rb.velocity = Speed * forward;
    }

    public void OnHit(Enemy enemy)
    {
        enemy.Damage(this.Power);
    }
}
