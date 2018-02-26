using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointStats : MonoBehaviour {

    public GameObject Shoulder;
    public GameObject Elbow;

    HingeJoint shoulder_hinge_joint;
    HingeJoint elbow_hinge_joint; 

    public float LogInterval;
    float timekeeper = 0; 

    void Start()
    {
        shoulder_hinge_joint = Shoulder.GetComponent<HingeJoint>();
        elbow_hinge_joint = Elbow.GetComponent<HingeJoint>(); 
    }
    
    void FixedUpdate()
    {
        timekeeper += Time.fixedDeltaTime; 
        
        if (timekeeper >= LogInterval)
        {
            //Debug.ClearDeveloperConsole(); 

            print("Shoulder angle: " + shoulder_hinge_joint.angle);
            print("Elbow angle: " + elbow_hinge_joint.angle); 

            

            //Vector3 shoulder_angular_vel = ShoulderRigidbody.angularVelocity;
            //Vector3 elbow_angular_vel = ElbowRigidbody.angularVelocity;

            //print("Shoulder angular vel: " + shoulder_angular_vel);  
            //print("Elbow angular vel: " + elbow_angular_vel);

            timekeeper = 0; 
        }
    }
}
