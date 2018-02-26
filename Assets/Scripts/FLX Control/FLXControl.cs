using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLXControl : MonoBehaviour {

    public PIDController PID; 
    public GameObject Elbow;
    public GameObject Shoulder;        

    public GameObject ZCarriageBlocks;
    public float ZStep;
    public float ZMaxPosition;
    public float ZMinPosition; 
    
    public float ElbowForce;   
    public float ShoulderForce;
    // add target velocities once controlled;     

    public float ShoulderTargetVelocity;
    public float ElbowTargetVelocity; 

    Rigidbody shoulder_rigid_body;
    Rigidbody elbow_rigid_body;

    HingeJoint shoulder_hinge_joint;
    HingeJoint elbow_hinge_joint;    

    void Start()
    {
        shoulder_rigid_body = Shoulder.GetComponent<Rigidbody>();
        elbow_rigid_body = Elbow.GetComponent<Rigidbody>();

        shoulder_hinge_joint = Shoulder.GetComponent<HingeJoint>();
        elbow_hinge_joint = Elbow.GetComponent<HingeJoint>();

        JointMotor shoulder_motor = shoulder_hinge_joint.motor;
        shoulder_motor.force = ShoulderForce;
        shoulder_hinge_joint.motor = shoulder_motor; 

        JointMotor elbow_motor = elbow_hinge_joint.motor;
        elbow_motor.force = ElbowForce;
        elbow_hinge_joint.motor = elbow_motor; 
   
    }

    void Update()
    {        
        handleInputs(); 
        
    }

    void FixedUpdate()
    {        

    }
   

    void handleInputs()
    {


        if (Input.GetKey(KeyCode.Q))
        {
            
            moveElbow(-ElbowTargetVelocity);
        }       
        else if (Input.GetKey(KeyCode.E))
        {
            
            moveElbow(ElbowTargetVelocity);
        }
        else
        {
            moveElbow(0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            
            moveShoulder(-ShoulderTargetVelocity);
        }       
        else if (Input.GetKey(KeyCode.D))
        {
            
            moveShoulder(ShoulderTargetVelocity);
        }
        else
        {
            moveShoulder(0); 
        }



        if (Input.GetKey(KeyCode.W))
        {
            Vector3 pos = ZCarriageBlocks.transform.position;
            pos.y += ZStep;
            ZCarriageBlocks.transform.position = pos;

            print("Blocks position: " + ZCarriageBlocks.transform.position);
            print("Blocks local position: " + ZCarriageBlocks.transform.localPosition);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 pos = ZCarriageBlocks.transform.position;
            pos.y -= ZStep;
            ZCarriageBlocks.transform.position = pos;

            print("Blocks position: " + ZCarriageBlocks.transform.position);
            print("Blocks local position: " + ZCarriageBlocks.transform.localPosition);
        }


        if (Input.GetKey(KeyCode.Z))
        {
            print("4th axis movement");
        }
        if (Input.GetKey(KeyCode.X))
        {
            print("4th axis movement");
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine("MoveElbowAngle", 30f); 

        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine("MoveElbowAngle", -50f); 
        }
    }

    void moveElbow(float target_velocity)
    {
        JointMotor motor = elbow_hinge_joint.motor;
        motor.targetVelocity = target_velocity;
        elbow_hinge_joint.motor = motor;         
    }

    void moveShoulder(float target_velocity)
    {
        JointMotor motor = shoulder_hinge_joint.motor;         
        motor.targetVelocity = target_velocity;
        shoulder_hinge_joint.motor = motor;
    }

    public void MoveShoulderAngle(float angle)
    {
        
    }

    public IEnumerator MoveElbowAngle(float angle)
    {
        float elbow_angle = Elbow.transform.position.z; 
        float tolerance = 1f; // within 1 degree; 

        float diff = angle - elbow_angle; 
        while (diff > tolerance)
        {
            elbow_angle = Elbow.transform.position.z;
            diff = elbow_angle - angle; 
            print("diff: " + diff); 

            if (diff < 0)
            {
                moveElbow(ElbowTargetVelocity);
            }
            if (diff > 0)
            {
                moveElbow(-ElbowTargetVelocity);
            }

            
            yield return new WaitForSeconds(0.1f);
            moveElbow(0);
        }
        StopCoroutine("MoveElbowAngle"); 
    }
   
}
