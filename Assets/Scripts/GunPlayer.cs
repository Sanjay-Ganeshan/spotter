using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GunPlayer : MonoBehaviour {

    public bool isLeft;
    public Color leftPlayerColor;
    public Color rightPlayerColor;

    private Gun gun;

    private Camera camera;


    private SteamVR_Behaviour_Pose controller;

    public const float MOVEMENT_THRESHOLD = 0.5f;
    public const float TRIGGER_THRESHOLD = 0.5f;

    public float TurnSpeed = 60.0f;

    // Use this for initialization
    void Start () {
		
	}

    private void Awake()
    {
        Initialize();

    }

    public void Initialize(bool isLeft)
    {
        this.isLeft = isLeft;
        Initialize();
    }

    public void Initialize()
    {
        this.gun = GetComponentInChildren<Gun>();
        this.camera = GetComponentInChildren<Camera>();
        this.controller = GetComponentInChildren<SteamVR_Behaviour_Pose>();
        if (isLeft)
        {
            // Take left half of IRL screen
            this.camera.rect = new Rect(new Vector2(0f, 0f), new Vector2(0.5f, 1f));
            this.camera.backgroundColor = leftPlayerColor;
            this.controller.inputSource = SteamVR_Input_Sources.LeftHand;
        }
        else
        {
            // Take right half of IRL screen
            this.camera.rect = new Rect(new Vector2(0.5f, 0f), new Vector2(0.5f, 1f));
            this.camera.backgroundColor = rightPlayerColor;
            this.controller.inputSource = SteamVR_Input_Sources.RightHand;
        }
    }

    // Update is called once per frame
    void Update () {
        float trigger = Input.GetAxis(axis("Trigger"));
        Vector2 trackpadDirection = new Vector2(Input.GetAxis(axis("TrackpadH")), Input.GetAxis(axis("TrackpadV")));
        bool trackpadClick = Input.GetButtonDown(axis("TrackpadPress"));

        UpdateFire(trigger);
        UpdateMovement(trackpadClick, trackpadDirection);

	}

    private void UpdateMovement(bool isTrackpadClicked, Vector2 trackpadDirection)
    {
        float horiz = trackpadDirection.x;
        if(isTrackpadClicked)
        {
            // Did they click while horizontal or vertical?
            if(Mathf.Abs(horiz) >= MOVEMENT_THRESHOLD)
            {
                Turn(Mathf.Sign(horiz) * -1.0f); // Invert because Right = turn right (negative turn)
            }
        }
    }

    /// <summary>
    /// Turn left by a magnitude, modified by TurnSpeed
    /// </summary>
    /// <param name="numTicks"></param>
    private void Turn(float numTicks)
    {
        this.transform.Rotate(Vector3.up, numTicks * this.TurnSpeed, Space.World);
    }

    private void UpdateFire(float trigger)
    {
        // Only use the trigger if it's pressed down a lot
        bool triggerDown = trigger >= TRIGGER_THRESHOLD;
        if(triggerDown)
        {
            gun.TriggerDown();
        }
        else
        {
            gun.TriggerUp();
        }
    }

    private string axis(string origname)
    {
        return (isLeft ? "Left" : "Right") + origname;
    }


    void SyncToGun()
    {
        // Always have XZ = gun XZ
        this.transform.position = new Vector3(this.gun.transform.position.x, 0, this.gun.transform.position.z);
    }
}
