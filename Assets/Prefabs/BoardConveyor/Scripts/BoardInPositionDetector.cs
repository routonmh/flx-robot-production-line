using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInPositionDetector : MonoBehaviour {

    public BoardConveyor BoardConveyor;
    public FactoryLineController FactoryLineController; 

	void OnTriggerEnter(Collider col)
    {
        GameObject obj = col.gameObject; 
        if (obj.tag == "BoardUnit")
        {
            BoardConveyor.StopRollers();

            FactoryLineController.StartCoroutine("PickUpAndMovePanelFromConveryorToTest"); 
        }
    }
}
