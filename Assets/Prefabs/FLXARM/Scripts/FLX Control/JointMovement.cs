using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointMovement : MonoBehaviour {

    [Range(-90f, 90f)]
    public float ShoulderAngle;

    [Range(-150f, 150f)]
    public float ElbowAngle;

    public float AngleTolerance; 

    public GameObject Shoulder;
    public GameObject Elbow;

    public float ShoulderMovementTargetVelocity;
    public float ElbowMovementTargetVelocity;    

    HingeJoint shoulder_joint;
    HingeJoint elbow_joint;

    FLXControl controller; 

    void Start()
    {
        shoulder_joint = Shoulder.GetComponent<HingeJoint>();
        elbow_joint = Elbow.GetComponent<HingeJoint>();
        controller = GetComponent<FLXControl>(); 
    }

    void Update()
    {
        if (!controller.ManualInputMode)
        {
            setShoulderAngle();
            setElbowAngle(); 
        }


    }

    void setShoulderAngle()
    {
        float shoulder_angle_diff = ShoulderAngle - shoulder_joint.angle;

        if (Mathf.Abs(shoulder_angle_diff) < AngleTolerance)
        {
            //print("Shoulder angle is within range.");            

            controller.StopMovingShoulder();
        }
        else
        {
            

            //print("Moving shoulder angle. Current: " + shoulder_joint.angle + ", Target: " + ShoulderAngle);

            if (shoulder_angle_diff < 0)
            {
                controller.StartMovingShoulder(ShoulderMovementTargetVelocity, false);

            }
            else if (shoulder_angle_diff > 0)
            {
                controller.StartMovingShoulder(ShoulderMovementTargetVelocity, true);
            }
        }
    }

    void setElbowAngle()
    {
        float elbow_angle_diff = ElbowAngle - elbow_joint.angle; 

        if (Mathf.Abs(elbow_angle_diff) < AngleTolerance)
        {
            controller.StopMovingElbow(); 
        }
        else
        {            

            //print("Moving elbow angle. Current: " + elbow_joint.angle + ", Target: " + ElbowAngle); 

            if (elbow_angle_diff < 0)
            {
                controller.StartMovingElbow(ElbowMovementTargetVelocity, false); 
            }

            if (elbow_angle_diff > 0)
            {
                controller.StartMovingElbow(ElbowMovementTargetVelocity, true); 
            }
        }
    }
}
