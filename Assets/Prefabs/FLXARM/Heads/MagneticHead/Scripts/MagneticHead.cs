using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticHead : MonoBehaviour {

    public float MagneticForce;
    public float MagneticRadius;

    public Vector3 AttachedObjectPosition; 

    GameObject attached_object = null; 



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            print("Attempting to attach object.");
            AttachObject();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            print("Detaching Object.");
            DetachObject(); 
        }
    }

    public void AttachObject()
    {
        foreach (Collider col in Physics.OverlapSphere(transform.position, MagneticRadius))
        {
            if (col.gameObject.tag == "BoardUnit")
            {
                col.gameObject.transform.SetParent(transform);

                col.gameObject.transform.position = transform.TransformPoint(AttachedObjectPosition); 

                attached_object = col.gameObject; 
            }
        }
    }

    public void DetachObject()
    {
        if (attached_object == null)
        {
            return; 
        }
        
        attached_object.transform.SetParent(null);

        attached_object = null; 
    }

    void OnDrawGizmos()
    {
        Vector3 pos = transform.position;

        GameObject[] boards = GameObject.FindGameObjectsWithTag("BoardUnit");
        if (boards.Length > 0)
        {
            foreach (GameObject board in boards)
            {
                Gizmos.DrawLine(pos, board.transform.position);
            }
        }
        
    }

    //public void FixedUpdate()
    //{
    //    foreach (Collider collider in Physics.OverlapSphere(transform.position, MagneticRadius)) {            


    //        if (collider.gameObject.tag == "BoardUnit")
    //        {
    //            print("Forcing Board unit");

    //            //Vector3 forceDirection = transform.position - collider.transform.position;

    //            //collider.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * MagneticForce * Time.fixedDeltaTime);

    //            collider.GetComponent<Rigidbody>().AddForce(transform.localPosition * MagneticForce); 
    //        }
    //    }
    //}  

    //void OnTriggerEnter(Collider col)
    //{
    //    GameObject ob = col.gameObject;

    //    if (ob.tag == "BoardUnit") ; 
    //}


}
