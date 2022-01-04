using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.EventSystems; 

[RequireComponent(typeof(ARRaycastManager))]
public class ButtonHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject gameObjectToInstantiate;
    [SerializeField]
    public Button btnObject1;



    private GameObject spawnedObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();


    //Method to check if the position is touched 
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }
    void Awake()
    {
        //reference AR Raycastmanager 
        _arRaycastManager = GetComponent<ARRaycastManager>();
        btnObject1 = GetComponent<Button>();


    }
    void Update()
    {
        btnObject1.enabled = false; //Does not work :(
        if (btnObject1.enabled)
        {
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

            if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;

                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(gameObjectToInstantiate, hitPose.position, hitPose.rotation);
                }
                else
                {
                    spawnedObject.transform.position = hitPose.position;
                }
            }
        }
    }

}