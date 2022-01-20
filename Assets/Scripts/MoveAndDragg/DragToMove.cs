using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToMove : MonoBehaviour
{
    private Touch touch;
    private float SpeedModifier; //controls the speed of the movement
    void Start()
    {
        SpeedModifier = 0.001f; 
    }

    void Update()
    {
        if(Input.touchCount>0)
        {
            touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                transform.position = new Vector3(transform.position.x + touch.deltaPosition.x * SpeedModifier, transform.position.y, transform.position.z +touch.deltaPosition.y*SpeedModifier);
            }
            //
        }
        
    }
}
