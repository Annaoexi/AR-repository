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
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();


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



    //Vector Variables 
    public static Vector3 LeftTopCorner;
    public static Vector3 RightTopCorner;
    public static Vector3 LeftBottomCorner;
    public static Vector3 RightBottomCorner;

    //Variables to save vector 

    public string x_LBC; 
   
    public string y_LBC; 
    public string z_LBC; 

    public string x_RBC; 
    public string y_RBC; 
    public string z_RBC; 

    public string x_LTC; 
    public string y_LTC; 
    public string z_LTC; 

    public string x_RTC; 
    public string y_RTC; 
    public string z_RTC; 

    

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

                //1. Deaktivierung der AR Plane 
                _arPlaneManager.planePrefab.SetActive(false);
                _arPlaneManager.SetTrackablesActive(false);


                //2. Erstellen der Plane zwischen den Punkten 
                //Get items for the planeOutlinePointsList 
                LeftTopCorner = planeOutlinePoints[0];
                RightTopCorner = planeOutlinePoints[1];
                RightBottomCorner = planeOutlinePoints[2];
                LeftBottomCorner = planeOutlinePoints[3];

                //Creating new plane 
                //_planeCreator.GetComponent<PlaneCreator>().CreatingPlane();
                float Width = RightTopCorner.x - RightBottomCorner.x;
                float Height = LeftTopCorner.y - LeftBottomCorner.y;


                //Setup of Mesh 
                GameObject Plane = new GameObject();
                MeshRenderer meshRenderer = Plane.AddComponent<MeshRenderer>();
                MeshFilter meshFilter = Plane.AddComponent<MeshFilter>();
            
                //meshRenderer.sharedMaterial = new Material(Shader.Find("Standard")); 

                Mesh m = new Mesh();

                //Vertex Array 
                m.vertices = new Vector3[4]
                {
                 LeftBottomCorner,  //Corresponse to 0
                 RightBottomCorner, //Corresponse to 1
                 LeftTopCorner,     //Corresponse to 2
                 RightTopCorner     //Corresponse to 3
                };

                //Setting up triangels 
                m.triangles = new int[6]
                {
                    //lower left triangle 
                    0,2,1,
                    //upper right triangle
                    2,3,1
                };


                //Setting up Texture
                m.uv = new Vector2[4]
                {
                 new Vector2(0, 0),
                 new Vector2(0, 1),
                 new Vector2(1, 1),
                 new Vector2(1, 0)
                };


                meshFilter.mesh = m;
                m.RecalculateNormals();
                m.RecalculateBounds();

                //3. Save vectors in playerprefs 
                float _LBCx = LeftBottomCorner[0]; 
                float _LBCy = LeftBottomCorner[1]; 
                float _LBCz = LeftBottomCorner[2]; 

                float _RBCx = RightBottomCorner[0];
                float _RBCy = RightBottomCorner[1];
                float _RBCz = RightBottomCorner[2];

                float _LTCx = LeftTopCorner[0];
                float _LTCy = LeftTopCorner[1];
                float _LTCz = LeftTopCorner[2];
                
                float _RTCx = RightTopCorner[0];
                float _RTCy = RightTopCorner[1];
                float _RTCz = RightTopCorner[2];

                PlayerPrefs.SetFloat("x_LBC", _LBCx); 
                PlayerPrefs.SetFloat("y_LBC", _LBCy); 
                PlayerPrefs.SetFloat("z_LBC", _LBCz); 

                PlayerPrefs.SetFloat("x_RBC", _RBCx);
                PlayerPrefs.SetFloat("y_RBC", _RBCy); 
                PlayerPrefs.SetFloat("z_RBC", _RBCz); 

                PlayerPrefs.SetFloat("x_LTC", _LTCx); 
                PlayerPrefs.SetFloat("y_LTC", _LTCy); 
                PlayerPrefs.SetFloat("z_LTC", _LTCz); 

                PlayerPrefs.SetFloat("x_RTC", _RTCx); 
                PlayerPrefs.SetFloat("y_RTC", _RTCy); 
                PlayerPrefs.SetFloat("z_RTC", _RTCz); 
             


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
