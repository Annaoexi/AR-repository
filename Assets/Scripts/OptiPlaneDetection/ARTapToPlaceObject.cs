using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.XR; 
using UnityEngine.Experimental.XR.Interaction; 




[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(ARPlaneManager))]
public class ARTapToPlaceObject : MonoBehaviour
{   //Variables for creating the frame  

    [SerializeField]
    private TextMeshProUGUI showCorner; //Display of text 

    [SerializeField]
    private GameObject indicateCorner; //Game object to place into the corners of the plane 

    //[SerializeField]
    //private GameObject gameLoopObject; //Object to place try out 
    private GameObject[] spawnedObjects = new GameObject[4];
    private int i = 0; //Variable for placed object


    private ARRaycastManager _aRRaycastManager;
    private ARPlaneManager _arPlaneManager;

    private ARRaycastManager newARRaycastManager;


    public List<Vector3> planeOutlinePoints = new List<Vector3>();
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private enum gameCorner
    {
        UpperLeft,
        UpperRight,
        LowerRight,
        LowerLeft,
        AllDone
    }

    gameCorner setCorner = gameCorner.UpperLeft; //Here the taping of the corners start 


    //Methode for plane corners was macht die? bzw was sind die plane outline points

    public List<Vector3> GetOutlinePoints()
    {
        return planeOutlinePoints;
    }



    //Variables for placing the objects into the frame 

    //private Vector2 touchPositionObject;//tap on screen where to shoot raycast 

    public GameObject gameObjectToInstantiate;  //Variable for any object we want to instantiate 
    private GameObject placedObject; //reference varibale to gameObjectToInstantiate






    private void Awake()
    {
        _aRRaycastManager = GetComponent<ARRaycastManager>(); //Raycasting wird aktiviert 
        _arPlaneManager = GetComponent<ARPlaneManager>(); //Plane detection wird aktiviert 
    }

    //Method for touching to set corners of frame , if touched 
    bool TryGetTouchPosition(out Vector3 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            Touch theTouch = Input.GetTouch(0);

            if (theTouch.phase == TouchPhase.Ended) // seems to work
            {
                touchPosition = theTouch.position;
                return true;
            }

        }
        touchPosition = default;
        return false;
    }



    // Update is called once per frame
    void Update()
    {   //Set up the defined plane where to place objects 
        switch (setCorner)
        {
            case gameCorner.UpperLeft:
                showCorner.SetText("Set the upper left Corner");
                break;

            case gameCorner.UpperRight:
                showCorner.SetText("Set the upper right Corner");

                break;

            case gameCorner.LowerRight:
                showCorner.SetText("Set the lower right Corner");

                break;

            case gameCorner.LowerLeft:
                showCorner.SetText("Set the lower left Corner");

                break;

            case gameCorner.AllDone:

                _arPlaneManager.planePrefab.SetActive(true); //Deaktivierung der "alten" Plane Detection?
                _arPlaneManager.SetTrackablesActive(true); //Deaktivierung der "alten" Trackables?
                showCorner.gameObject.SetActive(false); //Deaktivierung der Corner Objekte 

                foreach (var _object in spawnedObjects) //Jedes Objekt in den platzierten Objekten wird dekativiert 
                {
                    _object.gameObject.SetActive(false);
                }

                //Update of plane???
                


                
                //Placing objects? 
                

                
                
                

                

                break;






        }




        //Shoot array in update method 
        if (!TryGetTouchPosition(out Vector3 touchPosition))
            return;
        if (_aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinBounds)) //oder hier schon PlaneWithinBounds??? 
        {
            var HitPose = hits[0].pose;


            GameObject spawnedObject = Instantiate(indicateCorner, HitPose.position, HitPose.rotation);
            spawnedObjects[i] = spawnedObject;


            switch (setCorner)
            {
                case gameCorner.UpperLeft:
                    planeOutlinePoints.Add(HitPose.position);
                    setCorner = gameCorner.UpperRight;
                    i++;
                    break;

                case gameCorner.UpperRight:
                    planeOutlinePoints.Add(HitPose.position);
                    setCorner = gameCorner.LowerRight;
                    i++;
                    break;

                case gameCorner.LowerRight:
                    planeOutlinePoints.Add(HitPose.position);
                    setCorner = gameCorner.LowerLeft;
                    i++;
                    break;

                case gameCorner.LowerLeft:
                    planeOutlinePoints.Add(HitPose.position);
                    setCorner = gameCorner.AllDone;
                    break;

                case gameCorner.AllDone:

                    // nothing i guess oder gehts hier weiter? muss ich hier get corner zu plane hinzufügen? 

                    break;



            }
            //Oder geht es hier dann weiter? 

            //if(spawnedObject == null ) //if there is no object 
            //{
            //  spawnedObject = Instantiate(gameObjectToInstantiate, HitPose.position, HitPose.rotation);


            //}
            //else //if there is already an object then move it
            //{
            //    spawnedObject.transform.position = HitPose.position; //moving object
            //}
        }
    }
}
