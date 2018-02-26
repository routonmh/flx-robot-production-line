using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorController : MonoBehaviour
{

    public GameObject BeltLinkPrefab;
    public int NumBeltLinks;
    public float LinkSpacing;
    public float BeltSpeed;
    public float BeltCutoff; 

    List<GameObject> belt_links;

    bool belt_is_running = false;   
        
    void Start()
    {
        belt_links = new List<GameObject>(); 
        SpawnBeltLinks(); 
    }

    void Update()
    {
        if (belt_is_running)
        {
            TranslateBeltLinks(); 
        }

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    if (belt_is_running)
        //    {
        //        belt_is_running = false;
        //        print("Stopping the conveyor belt."); 
        //    }
        //    else
        //    {
        //        belt_is_running = true;
        //        print("Starting the conveyor belt."); 
        //    }
        //}
    }

    void SpawnBeltLinks()
    {
        Vector3 spawn_pos = transform.position; 

        for (int i = 0; i < NumBeltLinks; i++)
        {
            GameObject link = Instantiate(BeltLinkPrefab, spawn_pos, transform.rotation);
            spawn_pos.z -= LinkSpacing;

            belt_links.Add(link); 

            link.transform.SetParent(transform); 
        }
    }

    void TranslateBeltLinks()
    {
        print("Translating belts.");        

        for (int i = 0; i < belt_links.Count; i++)
        {
            GameObject link = belt_links[i];
            Vector3 pos = link.transform.position;
            pos.z -= Time.deltaTime * BeltSpeed;
            link.transform.position = pos; 

            if (pos.z < BeltCutoff)
            {
                belt_links.RemoveAt(i);
                Destroy(link);
                GameObject new_link = Instantiate(BeltLinkPrefab, transform.position, transform.rotation);
                belt_links.Add(new_link); 
            }
        }
    }
}