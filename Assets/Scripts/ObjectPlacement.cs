using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;



public class ObjectPlacement : MonoBehaviour
{
    //Variables for the script

    [SerializeField]
    ARRaycastManager m_Raycastmanager; 
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>(); 
    [SerializeField]
    GameObject placedPrefab; 

    Camera arCam; 
    GameObject placedObject; 
   
    void Start()
    {
     placedObject = null; 
     arCam = GameObject.Find("AR Camera").GetComponent<Camera>(); 

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        
            return; 
            RaycastHit hit; 
            Ray ray =  arCam.ScreenPointToRay(Input.GetTouch(0).position); 

        

        if(m_Raycastmanager.Raycast(Input.GetTouch(0).position, m_Hits))
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began&&placedObject == null)
            {
                if(Physics.Raycast(ray, out hit))
                {
                    if(hit.collider.gameObject.tag == "Spawnable")
                    {
                        placedObject= hit.collider.gameObject;
                    }
                    else
                    {
                        SpawnPrefab(m_Hits[0].pose.position); 
                    }
                }

            }
            else if(Input.GetTouch(0).phase == TouchPhase.Moved && placedObject!= null)
            {
                placedObject.transform.position = m_Hits[0].pose.position; 
            }
            if(Input.GetTouch(0).phase == TouchPhase.Ended){

                placedObject = null; 
            }
        }
    }

    private void SpawnPrefab(Vector3 placePosition)
    {
        placedObject = Instantiate(placedPrefab, placePosition, Quaternion.identity);

    }
}
