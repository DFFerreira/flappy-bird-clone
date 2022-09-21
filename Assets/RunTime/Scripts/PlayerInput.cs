using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    

    public bool Tap()
    {
        if(!enabled)
        {
            return false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                return true;
            }
        }
        return false;
    }
}
