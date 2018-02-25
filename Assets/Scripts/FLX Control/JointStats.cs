using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointStats : MonoBehaviour {

    public Rigidbody ShoulderRigidbody; 
    public Rigidbody ElbowRigidbody;

    public float LogInterval;
    float timekeeper = 0; 
    
    void FixedUpdate()
    {
        timekeeper += Time.fixedDeltaTime; 
        
        if (timekeeper >= LogInterval)
        {
            Debug.ClearDeveloperConsole(); 

            Vector3 shoulder_angular_vel = ShoulderRigidbody.angularVelocity;
            Vector3 elbow_angular_vel = ElbowRigidbody.angularVelocity;

            print("Shoulder angular vel: " + shoulder_angular_vel);  
            print("Elbow angular vel: " + elbow_angular_vel);

            timekeeper = 0; 
        }
    }
}
