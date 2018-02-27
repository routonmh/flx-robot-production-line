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
    //public FourthAxis;
    public FLXControl Controller;
    public ZPositionController ZController;
    public JointMovement JointMovementController;
    public GameObject Head; 
    
}

public class FactoryLineController : MonoBehaviour {

    public GameObject Conveyor;    
    public List<FactoryRobot> Robots;    

    bool sequence_is_running = false;

    FactoryRobot MoverRobot;
    FactoryRobot TesterRobot;
    FactoryRobot RouterRobot;
    FactoryRobot PackagerRobot; 
    
    void Start()
    {
        MoverRobot = Robots[0];
        TesterRobot = Robots[1];
        RouterRobot = Robots[2];
        PackagerRobot = Robots[3]; 
    }

    void Update()
    {
        foreach (FactoryRobot robot in Robots)
        {
            //print("Move in progress: " + robot.Controller.MoveInProgress);
        }

        if (Input.GetKeyDown(KeyCode.Equals) && !sequence_is_running)
        {
            sequence_is_running = true; 

            EnableRobots();            
            StartCoroutine("MoveRobotsToStartPositions");
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

    public IEnumerator MoveRobotsToStartPositions()
    {
        MoverRobot.JointMovementController.ShoulderAngle = 90f;
        MoverRobot.JointMovementController.ElbowAngle = -85f;
        MoverRobot.ZController.ZPosition = -2.5f;

        PackagerRobot.JointMovementController.ShoulderAngle = -90f;
        PackagerRobot.JointMovementController.ElbowAngle = 85f;
        PackagerRobot.ZController.ZPosition = -2.5f;

        yield return new WaitForSeconds(3);

        TesterRobot.JointMovementController.ElbowMovementTargetVelocity = 40f; 
        RouterRobot.JointMovementController.ElbowMovementTargetVelocity = 40f;

        RouterRobot.JointMovementController.ElbowAngle = -70f;
        TesterRobot.JointMovementController.ElbowAngle = 70f;

        yield return new WaitForSeconds(0.015f); 

        RouterRobot.JointMovementController.ShoulderAngle = 90f;
        TesterRobot.JointMovementController.ShoulderAngle = -90f;

        yield return new WaitForSeconds(3f);

        RouterRobot.JointMovementController.ElbowAngle = 90f;
        TesterRobot.JointMovementController.ElbowAngle = -90f;

        yield return new WaitForSeconds(3f); 

        StartCoroutine("ReceivePanelFromConveyor");
    }

    public IEnumerator ReceivePanelFromConveyor()
    {
        Conveyor.GetComponent<BoardConveyor>().StartRollers();

        yield return new WaitForSeconds(2f); 
        Conveyor.GetComponent<BoardConveyor>().SpawnBoard(); 
        

        MoverRobot.JointMovementController.ShoulderAngle = 90f;
        MoverRobot.JointMovementController.ElbowAngle = -90; 

        yield return new WaitForSeconds(1f);

        MoverRobot.ZController.ZPosition = -4.4f;

        yield return new WaitForSeconds(2.5f);
        
                
    }

    public IEnumerator PickUpAndMovePanelFromConveryorToTest()
    {
        MoverRobot.Head.GetComponent<MagneticHead>().AttachObject(); 
        yield return new WaitForSeconds(1f);

        MoverRobot.ZController.ZPosition = 0f;

        yield return new WaitForSeconds(1f);
        
        MoverRobot.JointMovementController.ShoulderAngle = 0f;

        yield return new WaitForSeconds(0.5f);
        MoverRobot.ZController.ZPosition = -4.4f;

        yield return new WaitForSeconds(4f);
        MoverRobot.Head.GetComponent<MagneticHead>().DetachObject();

        yield return new WaitForSeconds(0.5f);
        MoverRobot.ZController.ZPosition = 0f;

        MoverRobot.JointMovementController.ShoulderAngle = 90f;
        MoverRobot.JointMovementController.ElbowAngle = 90;

        StartCoroutine("MoveToTestPanel"); 
    }

    public IEnumerator MoveToTestPanel()
    {
        yield return new WaitForSeconds(4f); 

        TesterRobot.JointMovementController.ShoulderAngle = -70f;
        TesterRobot.JointMovementController.ElbowAngle = 110f; 
        TesterRobot.ZController.ZPosition = 0f;

        yield return new WaitForSeconds(5f);

        TesterRobot.ZController.ZPosition = -4.4f;
        yield return new WaitForSeconds(2f);

        Light test_head_light = GameObject.Find("TestHeadLight").GetComponent<Light>();
        test_head_light.intensity = 30f; 

        yield return new WaitForSeconds(1f);
        test_head_light.intensity = 0f; 

        yield return new WaitForSeconds(.2f);
        test_head_light.intensity = 30f;

        yield return new WaitForSeconds(.2f);
        test_head_light.intensity = 0f;

        yield return new WaitForSeconds(2.5f);
        test_head_light.intensity = 30f;

        yield return new WaitForSeconds(1f);
        test_head_light.intensity = 0f;

        yield return new WaitForSeconds(.2f);
        test_head_light.intensity = 30f;

        yield return new WaitForSeconds(1f);
        TesterRobot.ZController.ZPosition = -2f;

        yield return new WaitForSeconds(1f);
        test_head_light.intensity = 0f;
        TesterRobot.ZController.ZPosition = -4.4f;

        yield return new WaitForSeconds(.2f);
        test_head_light.intensity = 30f;

        yield return new WaitForSeconds(.2f);
        test_head_light.intensity = 0f;

        yield return new WaitForSeconds(2.5f);
        test_head_light.intensity = 30f;

        yield return new WaitForSeconds(1f);
        test_head_light.intensity = 0f;

        yield return new WaitForSeconds(1f);
        TesterRobot.ZController.ZPosition = -4.4f;

        yield return new WaitForSeconds(2f); //////

        TesterRobot.JointMovementController.ShoulderAngle = -80f;

        test_head_light.intensity = 30f;

        yield return new WaitForSeconds(1f);
        test_head_light.intensity = 0f;

        yield return new WaitForSeconds(.2f);
        test_head_light.intensity = 30f;

        yield return new WaitForSeconds(.2f);
        test_head_light.intensity = 0f;

        yield return new WaitForSeconds(2.5f);
        test_head_light.intensity = 30f;

        yield return new WaitForSeconds(1f);
        test_head_light.intensity = 0f;

        yield return new WaitForSeconds(.2f);
        test_head_light.intensity = 30f;

        yield return new WaitForSeconds(1f);
        TesterRobot.ZController.ZPosition = -2f;

        yield return new WaitForSeconds(1f);
        test_head_light.intensity = 0f;
        TesterRobot.ZController.ZPosition = -4.4f;

        yield return new WaitForSeconds(.2f);
        test_head_light.intensity = 30f;

        yield return new WaitForSeconds(.2f);
        test_head_light.intensity = 0f;

        yield return new WaitForSeconds(2.5f);
        test_head_light.intensity = 30f;

        yield return new WaitForSeconds(1f);
        test_head_light.intensity = 0f;

        yield return new WaitForSeconds(1f);
        TesterRobot.ZController.ZPosition = -4.4f;

        TesterRobot.JointMovementController.ShoulderAngle = -90f;
        TesterRobot.JointMovementController.ElbowAngle = -90f;

        yield return new WaitForSeconds(5f); 
    }

    public IEnumerator PickupTestedBoard()
    {

        yield return new WaitForSeconds(1f); 
    }
   
}
