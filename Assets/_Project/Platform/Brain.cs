using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Replay
{
    public List<double> States { get; set; }
    public double Reward { get; set; }

    public Replay (double zRotation, double ballPositionX, double ballVelocityX, double reward)
    {
        States = new List<double>();
        States.Add(zRotation);
        States.Add(ballPositionX);
        States.Add(ballVelocityX);
        Reward = reward;
    }
}

public class Brain : MonoBehaviour 
{
    

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
