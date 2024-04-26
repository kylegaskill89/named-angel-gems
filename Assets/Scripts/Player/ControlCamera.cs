using UnityEngine;
using System.Collections;

public class ControlCamera : MonoBehaviour {

    Rigidbody rb;
    public GameObject playerCam;

    private float camBegin;
    private float camEnd;
    private float journeyLength;
    private float startTime;

    public float camMoveSpeed = 1.0f;

    Transform pos;
    public Transform startMarker;
    public Transform endMarker;

	// Use this for initialization
	void Start ()
    {
     /*   startTime = Time.time;
        rb = GetComponent<Rigidbody>();
        camBegin = 6.35f;
        camEnd = 10f;
     */
	}
	
	// Update is called once per frame
	void Update ()
    {
      
	}

}
