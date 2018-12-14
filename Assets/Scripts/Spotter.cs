using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spotter : MonoBehaviour {

    public float spottingAngle = 20.0f;
    public float spotMultiplier = 3.0f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    	
	}

    public bool CanSee(GameObject obj)
    {
        // Would be better with raycasts
        Vector3 toObj = (obj.transform.position - this.transform.position);
        float angle = Vector3.Angle(this.transform.forward, toObj);
        if (angle <= spottingAngle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
