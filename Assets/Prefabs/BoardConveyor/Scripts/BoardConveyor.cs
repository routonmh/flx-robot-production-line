using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardConveyor : MonoBehaviour {

    public GameObject BoardPrefab;
    public Vector3 BoardSpawnPosition;
    public float RollerTranslateRate; 
    public bool RollersRolling = false; 
    
    GameObject[] rollers; 

    void Start()
    {
        rollers = GameObject.FindGameObjectsWithTag("RollerUnit"); 
    }

    void Update()   
    {
        if (Input.GetKeyDown(KeyCode.G))
        {            
            SpawnBoard();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            
            if (RollersRolling)
            {
                StopRollers();
            }
            else
            {
                StartRollers(); 
            }
        }
    }

    public GameObject SpawnBoard()
    {
        GameObject board = Instantiate(BoardPrefab, transform.TransformPoint(BoardSpawnPosition), new Quaternion());

        return board; 
    }

    public void StartRollers()
    {
        print("Starting Rollers."); 

        RollersRolling = true;
        foreach (GameObject roller in rollers)
        {
            roller.GetComponent<RotateRoller>().enabled = true; 
        }
    }

    public void StopRollers()  
    {
        print("Stopping rollers."); 

        RollersRolling = false; 
        foreach (GameObject roller in rollers)
        {
            roller.GetComponent<RotateRoller>().enabled = false;
        }
    }

    //void OnCollisionStay(Collision collis)
    //{
    //    print("Colliding conveyor.");

    //    GameObject ob = collis.gameObject; 
    //    if (ob.tag == "BoardUnit")
    //    {
    //        print("this is a board!");

    //        if (RollersRolling)
    //        {
    //            print("Moving board."); 
    //            Rigidbody rb = ob.GetComponent<Rigidbody>();
    //            Vector3 pos = ob.transform.position;
    //            pos.z += RollerTranslateRate;
    //            rb.MovePosition(pos); 
    //        }
    //    }
    //}

    void OnTriggerStay(Collider col)
    {

        if (col.tag == "BoardUnit")
        {
            if (RollersRolling)
            {
                Rigidbody rb = col.attachedRigidbody;

                Vector3 pos = col.transform.position;
                pos.z += RollerTranslateRate;
                rb.MovePosition(pos); 
            }            
        }
    }
}
