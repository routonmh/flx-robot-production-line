using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotsManager : MonoBehaviour {

    public GameObject ActiveRobot;

    GameObject[] robots; 

    void Start()
    {        
        robots = GameObject.FindGameObjectsWithTag("FLXARMBOT");        

    }

    void Update()
    {
        if (ActiveRobot == null)
        {
            //print("Active robot is not set. Set it with a num key 1-3.");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveRobot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveRobot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveRobot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetActiveRobot(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SetActiveRobot(4); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            EnableAllRobotsControl(); 
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            DisableAllRobotsControl();
        }
    }

    void SetActiveRobot(int index)
    {
        if (ActiveRobot != null)
        {
            ActiveRobot.GetComponent<FLXControl>().enabled = false; 
        }

        ActiveRobot = robots[index];
        ActiveRobot.GetComponent<FLXControl>().enabled = true; 

        print("Set the active robot to index: " + index); 
    }

    void EnableAllRobotsControl()
    {
        foreach(GameObject bot in robots)
        {
            bot.GetComponent<FLXControl>().enabled = true; 
        }
    }

    void DisableAllRobotsControl()
    {
        foreach (GameObject bot in robots)
        {
            bot.GetComponent<FLXControl>().enabled = false; 
        }
    }
}
