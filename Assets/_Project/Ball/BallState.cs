using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallState : MonoBehaviour
{
    public bool Dropped { get; set; } = false;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Drop")
        {
            Dropped = true;
        }
    }
}
