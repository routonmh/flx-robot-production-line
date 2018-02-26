using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerConveyorController : MonoBehaviour {

    public GameObject RollerPrefab;
    public int NumRollers;
    public float RollerSpacing;
    public float RollerRate;

    //List<GameObject> rollers = new List<GameObject>(); 

    bool apply_roller_rot = false;

    GameObject[] rollers; 

    void Start()
    {
        rollers = GameObject.FindGameObjectsWithTag("RollerUnit"); 

        SpawnRollers(); 
    }

    void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (apply_roller_rot)
            {
                apply_roller_rot = false;
                print("Starting the converyor.");

                EnableRollers(); 

            }
            else
            {
                apply_roller_rot = true;
                print("Stopping the conveyor");

                DisableRollers(); 
            }
        }
    }

    void SpawnRollers()
    {        
        Vector3 roller_rot_eulers = new Vector3(0, 90, 0);
        Quaternion rot = transform.rotation;
        rot.eulerAngles = roller_rot_eulers;

        Vector3 spawn_position = transform.localPosition; 
        for (int i = 0; i < NumRollers; i++)
        {
            GameObject roller = Instantiate(RollerPrefab, spawn_position, rot);
            
            spawn_position = roller.transform.localPosition;             
            spawn_position.z -= RollerSpacing;

            roller.transform.SetParent(transform);
            //rollers.Add(roller);
        }
    }

    void EnableRollers()
    {
        for (int i = 0; i < rollers.Length; i++)
        {
            rollers[i].GetComponent<RollUnitAction>().enabled = true;
        }
    }

    void DisableRollers()
    {
        for (int i = 0; i < rollers.Length; i++)
        {
            rollers[i].GetComponent<RollUnitAction>().enabled = false;
        }
    }
}
