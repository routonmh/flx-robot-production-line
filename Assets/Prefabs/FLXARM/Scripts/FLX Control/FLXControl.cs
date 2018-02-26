using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLXControl : MonoBehaviour {

    public PIDController PID; 
    public GameObject Elbow;
    public GameObject Shoulder;            

    
    public float ElbowForce;   
    public float ShoulderForce;
    // add target velocities once controlled;     

    public bool ManualInputMode = true; 

    public float ShoulderTargetVelocity;
    public float ElbowTargetVelocity;

    public bool MoveInProgress = false;

    Rigidbody shoulder_rigid_body;
    Rigidbody elbow_rigid_body;

    HingeJoint shoulder_hinge_joint;
    HingeJoint elbow_hinge_joint;

    ZPositionController z_controller;

    bool shoulder_move_in_progress;
    bool elbow_move_in_progress; 

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

        z_controller = GetComponent<ZPositionController>(); 

   
    }

    void Update()
    {            
        handleInputs(); 
        
        if (shoulder_move_in_progress || elbow_move_in_progress)
        {
            MoveInProgress = true; 
        }
        else
        {
            MoveInProgress = false; 
        }
    }

    void FixedUpdate()
    {        

    }
   

    void handleInputs()
    {

        if (ManualInputMode)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                StartMovingElbow(ElbowTargetVelocity, false);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                StartMovingElbow(ElbowTargetVelocity, true);
            }
            else
            {
                StopMovingElbow();
            }

            if (Input.GetKey(KeyCode.A))
            {
                StartMovingShoulder(ShoulderTargetVelocity, false);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                StartMovingShoulder(ShoulderTargetVelocity, true);
            }
            else
            {
                StopMovingShoulder();
            }



            if (Input.GetKey(KeyCode.W))
            {
                z_controller.StartMovingZPosition(true);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                z_controller.StartMovingZPosition(false); 
            }
            else
            {
                z_controller.StopMovingZPosition();     
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
        


        if (Input.GetKeyDown(KeyCode.P))
        {
            float[] args = new float[2];
            args[0] = 30f;
            args[1] = 1f;

            // can't pass more than one param to startcoroutine??

            StartCoroutine("MoveShoulderAngle", args);

            //startMovingShoulder(ShoulderTargetVelocity, true);

        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (ManualInputMode)
            {
                ManualInputMode = false; 
            }
            else
            {
                ManualInputMode = true; 
            }

            print("Manual input mode is toggled to: " + ManualInputMode); 
        }
    }

    public void StartMovingElbow(float target_velocity, bool positive_direction)
    {
        JointMotor motor = elbow_hinge_joint.motor;
        elbow_move_in_progress = true; 

        if (positive_direction)
        {
            motor.targetVelocity = target_velocity;
        }
        else
        {
            motor.targetVelocity = -target_velocity;
        }

        elbow_hinge_joint.motor = motor;
    }

    public void StartMovingShoulder(float target_velocity, bool positive_direction)
    {
        JointMotor motor = shoulder_hinge_joint.motor;
        shoulder_move_in_progress = true;

        if (positive_direction)
        {
            motor.targetVelocity = target_velocity; 
        }
        else
        {
            motor.targetVelocity = -target_velocity; 
        }

        shoulder_hinge_joint.motor = motor; 
    }

    public void StopMovingElbow()
    {
        JointMotor motor = elbow_hinge_joint.motor;
        motor.targetVelocity = 0;

        elbow_move_in_progress = false; 

        elbow_hinge_joint.motor = motor; 
    }

    public void StopMovingShoulder()
    {
        JointMotor motor = shoulder_hinge_joint.motor;
        motor.targetVelocity = 0;
        //motor.force = BIG_NUMBER // this requires the force to be set back on start movement
        shoulder_move_in_progress = false; 
        
        shoulder_hinge_joint.motor = motor;
    }

    public IEnumerator MoveShoulderAngle(float[] args)        
    {
        float angle = args[0];
        float tolerance = args[1]; 

        float shoulder_angle = Shoulder.transform.rotation.z;
        float diff = angle - shoulder_angle;

        print("starting movement");

        StartMovingShoulder(20, true); 

        while (true)
        {
            shoulder_angle = Shoulder.transform.rotation.z;

            diff = angle - shoulder_angle;

            print("Shoulder angle: " + shoulder_angle + " Target angle: " + angle); 
            print("Diff: " + diff); 

            if (diff < tolerance)
            {
                StopMovingShoulder(); 
                StopCoroutine("MoveShoulderAngle"); 
            }
            yield return new WaitForSeconds(0.01f); 
        }

        
    }

    public void MoveElbowAngle(float angle)
    {
        
    }
    
   
}
