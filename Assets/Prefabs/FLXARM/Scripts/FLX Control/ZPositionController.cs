using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZPositionController : MonoBehaviour {

    public float ZStep;

    public float ZMinPosition;
    public float ZMaxPosition; 

    [Range(-4.4f, 4.5f)]
    public float ZPosition;
    public float PositionTolerance; 

    public GameObject ZCarriageBlocks;

    bool moving_z_position;
    bool moving_z_up;

    FLXControl controller; 

    void Start()
    {
        controller = GetComponent<FLXControl>(); 
    }

    void Update()
    {

        if (moving_z_position)
        {
            Vector3 pos = ZCarriageBlocks.transform.localPosition; 

            if (moving_z_up)
            {                
                pos.y += ZStep;                
            }
            else
            {                
                pos.y -= ZStep;                
            }

            pos = EnforceZLimits(pos); 

            ZCarriageBlocks.transform.localPosition = pos;
        }

        if (!controller.ManualInputMode)
        {
            float z_diff = ZPosition - ZCarriageBlocks.transform.localPosition.y; 

            if (Mathf.Abs(z_diff) < PositionTolerance)
            {
                //print("Z position within tolerance."); 
                StopMovingZPosition(); 
            }
            else
            {
                //print("Moving Z. Current: " + ZCarriageBlocks.transform.localPosition.y + ", Target: " + ZPosition); 

                if (z_diff > 0)
                {
                    StartMovingZPosition(true);
                }
                else if (z_diff < 0)
                {
                    StartMovingZPosition(false);
                }
            }

        }
    } 

    public void StartMovingZPosition(bool positive_direction)
    {
        moving_z_position = true;

        if (positive_direction)
        {
            moving_z_up = true; 
        }
        else
        {
            moving_z_up = false; 
        }
    }

    public void StopMovingZPosition()
    {
        moving_z_position = false; 
    }

    Vector3 EnforceZLimits(Vector3 pos)
    {
        if (pos.y > ZMaxPosition)
        {
            pos.y = ZMaxPosition; 
            StopMovingZPosition();
        }
        if (pos.y < ZMinPosition)
        {
            pos.y = ZMinPosition; 
            StopMovingZPosition();
        }

        return pos; 
    }
}
