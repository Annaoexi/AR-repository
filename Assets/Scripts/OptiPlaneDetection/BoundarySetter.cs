using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.XR;
using UnityEngine.UI;



public class BoundarySetter : MonoBehaviour
{


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
    public List<Vector3> cornerPoints = new List<Vector3>();

    private enum PlaneSurface 
    {
        UpperLeft,
        UpperRight,
        LowerRight,
        LowerLeft,
        AllDone,

    }

    PlaneSurface  setCorner = PlaneSurface.UpperLeft; //Here the taping of the corners start 



    //Vector Variables 
    public static Vector3 LeftTopCorner;
    public static Vector3 RightTopCorner;
    public static Vector3 LeftBottomCorner;
    public static Vector3 RightBottomCorner;

    //Variables to save vector 

  
    


    //Method for touching to set corners of frame , if touched 
    bool TryGetTouchPosition(out Vector3 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            Touch theTouch = Input.GetTouch(0);

            if (theTouch.phase == TouchPhase.Ended) 
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
            case PlaneSurface.UpperLeft:
                showCorner.SetText("Click on upper left corner");
                break;

            case PlaneSurface.UpperRight:
                showCorner.SetText("Click on upper right Corner");

                break;

            case PlaneSurface.LowerRight:
                showCorner.SetText("Click on lower right Corner");

                break;

            case PlaneSurface.LowerLeft:
                showCorner.SetText("Click on lower left Corner");

                break;

            case PlaneSurface.AllDone:
                showCorner.SetText("Surface will be initialized");
                foreach (var _object in spawnedObjects) 
                {
                    _object.gameObject.SetActive(false);
                }

                //1. Deaktivierung der AR Plane 
                _arPlaneManager.planePrefab.SetActive(false);
                _arPlaneManager.SetTrackablesActive(false);


                //2. Erstellen der Plane zwischen den Punkten 
                //Get items for the cornerPointsList 
                LeftTopCorner = cornerPoints[0];
                RightTopCorner = cornerPoints[1];
                RightBottomCorner = cornerPoints[2];
                LeftBottomCorner = cornerPoints[3];

                //Creating new plane 
                
                float Width = RightTopCorner.x - RightBottomCorner.x;
                float Height = LeftTopCorner.y - LeftBottomCorner.y;


                //Setup of Mesh 
                GameObject Plane = new GameObject();
                MeshRenderer meshRenderer = Plane.AddComponent<MeshRenderer>();
                MeshFilter meshFilter = Plane.AddComponent<MeshFilter>();

                meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
                meshRenderer.sharedMaterial.color = Color.blue;
               

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
        if (_aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon)) 
        {
            var HitPose = hits[0].pose;


            GameObject spawnedObject = Instantiate(indicateCorner, HitPose.position + indicateCorner.transform.position, HitPose.rotation);
            spawnedObjects[i] = spawnedObject;


            switch (setCorner)
            {
                case PlaneSurface.UpperLeft:
                    cornerPoints.Add(HitPose.position);
                    setCorner = PlaneSurface.UpperRight;
                    i++;
                    break;

                case PlaneSurface.UpperRight:
                    cornerPoints.Add(HitPose.position);
                    setCorner = PlaneSurface.LowerRight;
                    i++;
                    break;

                case PlaneSurface.LowerRight:
                    cornerPoints.Add(HitPose.position);
                    setCorner = PlaneSurface.LowerLeft;
                    i++;
                    break;

                case PlaneSurface.LowerLeft:
                    cornerPoints.Add(HitPose.position);
                    setCorner = PlaneSurface.AllDone;
                    break;

                case PlaneSurface.AllDone:



                    break;




            }

        }
    }
}
