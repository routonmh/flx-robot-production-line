using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PIDController
{
    public float PGain;
    public float IGain;
    public float DGain;
    public float SumErrorMax;

    float prev_error;
    float prev_error_2;
    float sum_error;
    float sum_error_2; 

    public PIDController()
    {
        prev_error = 0f;
        prev_error_2 = 0f;
        sum_error = 0f;
        sum_error_2 = 0f;         
    }

    public float GetPIDOutput(float error)
    {
        return CalculatePIDOutput(error); 
    }   

    float CalculatePIDOutput(float error)
    {                                                          
        float output = 0f;

        output += PGain * prev_error;
        sum_error += Time.fixedDeltaTime * error;
        sum_error = Mathf.Clamp(sum_error, -SumErrorMax, SumErrorMax);

        output += IGain * sum_error;

        float d_error = (error - prev_error) / Time.fixedDeltaTime;

        prev_error_2 = prev_error;
        prev_error = error;

        output += DGain * d_error; 

        return output; 
    }
}
