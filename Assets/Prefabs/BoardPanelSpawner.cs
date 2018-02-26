using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPanelSpawner : MonoBehaviour {

    public GameObject BoardPanelPrefab;
    public float XSpawnOffset;
    public float YSpawnOffset;
    public float ZSpawnOffset; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SpawnSpanel(); 
        }
    }

    void SpawnSpanel()
    {
        Vector3 spawn_point = transform.position;
        spawn_point.x += XSpawnOffset;
        spawn_point.y += YSpawnOffset; 
        spawn_point.z += ZSpawnOffset;

        Instantiate(BoardPanelPrefab, spawn_point, transform.rotation); 
    }
}
