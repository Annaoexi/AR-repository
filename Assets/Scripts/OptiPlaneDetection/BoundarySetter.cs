using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.XR;
using UnityEngine.UI;



public class BoundarySetter : MonoBehaviour
{   //Verweise auf Object Placer skript 
    //[SerializeField]
    //private ObjectPlacer _objectPlacer;


    //Variables for raycasting and plane detection 
    [SerializeField]
    private ARRaycastManager _aRRaycastManager;
    
    [SerializeField]
    private ARPlaneManager _arPlaneManager;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>(); //wieso static? 


    //Variables for creating the boundaries  

    [SerializeField]
    private TextMeshProUGUI showCorner; //Display of text 

    [SerializeField]
    private GameObject indicateCorner; //Game object to place into the corners of the plane 


    private GameObject[] spawnedObjects = new GameObject[4];
    private int i = 0; //Variable for placed object


    //Variables for corner points 
    public List<Vector3> planeOutlinePoints = new List<Vector3>();

    private enum gameCorner
    {
        UpperLeft,
        UpperRight,
        LowerRight,
        LowerLeft,
        AllDone,

    }

    gameCorner setCorner = gameCorner.UpperLeft; //Here the taping of the corners start 


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
                showCorner.SetText("Plane will be initialized");
                foreach (var _object in spawnedObjects) //Jedes Objekt in den platzierten Objekten wird dekativiert 
                {
                    _object.gameObject.SetActive(false);
                }

                showCorner.gameObject.SetActive(false); //Deactivateing of text 

                //1. Deaktivierung der AR Plane 
                _arPlaneManager.planePrefab.SetActive(false);
                _arPlaneManager.SetTrackablesActive(false);

                //2. Erstellen der Plane zwischen den Punkten 
                //Get items for the planeOutlinePointsList 
                Vector3 LeftTopCorner = planeOutlinePoints[0];
                Vector3 RightTopCorner = planeOutlinePoints[1];
                Vector3 LeftBottomCorner = planeOutlinePoints[2];
                Vector3 RightBottomCorner = planeOutlinePoints[3]; //Man braucht eigentlich nur 3 Punkte um das Plane zu erstellen 

                //Calculating and creating new plane 
                var plane = new Plane(LeftTopCorner, RightTopCorner, LeftBottomCorner);
                var center = (LeftTopCorner + RightTopCorner + LeftBottomCorner) / 3f;
                float LenghtOfPlane = RightTopCorner.x - RightBottomCorner.x;
                float WidthOfPlane = LeftTopCorner.y - LeftBottomCorner.y;

                Vector3 SizeOfPlane = new Vector3(LenghtOfPlane, WidthOfPlane, 0);


                //Draw plane?               
                    

                Gizmos.color = Color.cyan;
                Gizmos.DrawCube(center, SizeOfPlane);
                //Gizmos geht anscheinend nur wieder mit kack Gameobjekten... transform.position? 


                Gizmos.DrawLine(LeftTopCorner, RightTopCorner); 
                Gizmos.DrawLine(RightTopCorner, RightBottomCorner); 
                Gizmos.DrawLine(RightBottomCorner, LeftBottomCorner);
                Gizmos.DrawLine(LeftBottomCorner, LeftTopCorner); 


                //Visualisierung der Plane 
                //_objectPlacer.setBoundaries();
                //was braucht der an Infos damit er die Objecte plazieren kann 
                //alternative mathematisch lösen 
                //collischen, collider -> gameObject kann collider haben (physikalisches Componeten)
                // ob object collidiert 
                //plane und objekt haben beide collider und dann checken ob die sich berühren 
                //vier eckpunkte 


                break;



        }


        //Placing of corner objects 
        if (!TryGetTouchPosition(out Vector3 touchPosition))
            return;
        if (_aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon)) //oder hier schon PlaneWithinBounds??? 
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



                    break;




            }

        }
    }
}
