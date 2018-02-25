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

    float shoulder_motor_target_velocity = 0f;
    float elbow_motor_target_velocity = 0f;     

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
            
            moveElbow(elbow_motor_target_velocity);
        }       
        else if (Input.GetKey(KeyCode.E))
        {
            
            moveElbow(elbow_motor_target_velocity);
        }
        else
        {
            lockElbow();
        }
      
        if (Input.GetKey(KeyCode.A))
        {
            
            moveShoulder(shoulder_motor_target_velocity);
        }       
        else if (Input.GetKey(KeyCode.D))
        {
            
            moveShoulder(shoulder_motor_target_velocity);
        }
        else
        {
            lockShoulder(); 
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

    void lockElbow()
    {
        print("Locking elbow."); 
        moveElbow(0);        
    }

    void lockShoulder()
    {
        print("Locking shoulder.");         
        moveShoulder(0); 
        
    }
}
