using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAreaHandler : MonoBehaviour
{
    bool frontRectIsIn = false; 
    bool backRectIsIn = false;

    public bool IsParked()
    {
        if (frontRectIsIn && backRectIsIn)
        {
            Debug.Log("Parked");
            return true;
        }
        else
        {
            Debug.Log("Not parked");
            return false;
        }
    }
    private void FixedUpdate()
    {
        IsParked();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FrontRect"))
        {
            frontRectIsIn = true;
            //Debug.Log("FrontRect in");
        }
        if (other.CompareTag("BackRect"))
        {
            backRectIsIn = true;
            //Debug.Log("BackRect in");
        }
    }
    

    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("FrontRect"))
        {
            frontRectIsIn = false;
            //Debug.Log("FrontRect out");
        }
        if (other.CompareTag("BackRect"))
        {
            backRectIsIn = false;
            //Debug.Log("BackRect out");
        }
    }

    
}
