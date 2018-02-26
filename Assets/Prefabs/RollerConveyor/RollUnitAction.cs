using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollUnitAction : MonoBehaviour {

    public float RollRate; 

	void Update()
    {
        transform.Rotate(0, 0, RollRate * Time.deltaTime); 
    }
}
    