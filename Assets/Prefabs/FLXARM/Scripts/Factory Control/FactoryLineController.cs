using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FactoryRobot : System.Object
{
    public GameObject FlxBot;
    public HingeJoint ShoulderJoint;
    public HingeJoint ElbowJoint;
    public FLXControl Controller;
    public ZPositionController ZController;
    public JointMovement JointMovementController;   
}

public class FactoryLineController : MonoBehaviour {

    public List<FactoryRobot> Robots; 
    
    void Start()
    {
        EnableRobots();
        //MoveRobotsToStartPositions(); 
        StartCoroutine("MoveRobotsToStartPositions"); 
    }

    void Update()
    {
        foreach (FactoryRobot robot in Robots)
        {
            print("Move in progress: " + robot.Controller.MoveInProgress); 
        }
    }

    void EnableRobots()
    {
        for (int i = 0; i < Robots.Count; i++)
        {
            Robots[i].Controller.enabled = true;
            Robots[i].Controller.ManualInputMode = false; 
        }
    }

    IEnumerator MoveRobotsToStartPositions()
    {
        Robots[0].JointMovementController.ShoulderAngle = 90f;
        Robots[0].JointMovementController.ElbowAngle = -85f;
        Robots[0].ZController.ZPosition = -2.5f;

        Robots[3].JointMovementController.ShoulderAngle = -90f;
        Robots[3].JointMovementController.ElbowAngle = 85f;
        Robots[3].ZController.ZPosition = -2.5f;

        yield return new WaitForSeconds(3); 

        Robots[1].JointMovementController.ShoulderAngle = -90f;
        Robots[1].JointMovementController.ElbowAngle = -90f;

        Robots[2].JointMovementController.ShoulderAngle = 90f;
        Robots[2].JointMovementController.ElbowAngle = 90f;        
    }
}
