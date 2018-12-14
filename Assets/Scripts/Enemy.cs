using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Renderer PlayerRenderer;
    public Renderer SpotterRenderer;
    public Collider HitBox;
    
    public float MaxAwareness = 3.0f;
    public float MinAwareness = -3.0f;
    public float Awareness = -1.0f;
    public float Hitpoints = 0.5f;

    private Spotter spotter;

	// Use this for initialization
	void Start () {
        this.Awareness = this.MinAwareness;
	}

    private void Awake()
    {
        spotter = GameObject.Find("Spotter").GetComponentInChildren<Spotter>();
    }

    // Update is called once per frame
    void Update () {
        if(spotter.CanSee(this.gameObject))
        {
            this.Awareness += Time.deltaTime * spotter.spotMultiplier;
            if(this.Awareness >= 0.0f)
            {
                Reveal();
            }
        }
        else
        {
            this.Awareness -= Time.deltaTime;
            if(this.Awareness < 0.0f)
            {
                Hide();
            }
        }

        // Keep awareness from getting too big or small
        this.Awareness = Mathf.Clamp(this.Awareness, MinAwareness, MaxAwareness);
	}

    internal void Damage(float power)
    {
        this.Hitpoints -= power;
        if(this.Hitpoints < 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject, 0.01f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Projectile hitProjectile = other.GetComponentInParent<Projectile>();
        if(hitProjectile != null)
        {
            hitProjectile.OnHit(this);
        }
    }

    public void Reveal()
    {
        this.PlayerRenderer.enabled = true;
        if(this.HitBox != null)
        {
            this.HitBox.enabled = true;
        }
    }

    public void Hide()
    {
        this.PlayerRenderer.enabled = false;
        if (this.HitBox != null)
        {
            this.HitBox.enabled = false;
        }
    }
}
