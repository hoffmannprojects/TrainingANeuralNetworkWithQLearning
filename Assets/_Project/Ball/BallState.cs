using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState : MonoBehaviour
{
    public bool Dropped { get; set; } = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.tag == "Drop")
        {
            Dropped = true;
        }
    }
}
