using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacementOfObjects : MonoBehaviour
{
    
    [SerializeField]
    public static GameObject gameObjectToInstantiate;

    private GameObject spawnedObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private List<GameObject> placedPrefabList = new List<GameObject>();
    [SerializeField]
    private int maxPreabSpawnCount = 0;
    private int placedPrefabCount;

    //Method to set new object with a button 
    public void SetPrefabType(GameObject prefabType)
    {
        gameObjectToInstantiate = prefabType;
    }

    //Method to instantiate gameobjects 
    private void SpwanPrefab(Pose hitPose)
    {   
        spawnedObject = Instantiate(gameObjectToInstantiate, hitPose.position+gameObjectToInstantiate.transform.position, hitPose.rotation);
        placedPrefabList.Add(spawnedObject);
        placedPrefabCount++;
    }

    //Method to check if the position is touched 
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {  
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;

    }

    private void Start()
    {
  

        //Variables for plane creation 
        //Get Variables from Playerprefs

        float x_LBC_saved = PlayerPrefs.GetFloat("x_LBC");
        float y_LBC_saved = PlayerPrefs.GetFloat("y_LBC");
        float z_LBC_saved = PlayerPrefs.GetFloat("z_LBC");

        float x_RBC_saved = PlayerPrefs.GetFloat("x_RBC");
        float y_RBC_saved = PlayerPrefs.GetFloat("y_RBC");
        float z_RBC_saved = PlayerPrefs.GetFloat("z_RBC");

        float x_LTC_saved = PlayerPrefs.GetFloat("x_LTC");
        float y_LTC_saved = PlayerPrefs.GetFloat("y_LTC");
        float z_LTC_saved = PlayerPrefs.GetFloat("z_LTC");

        float x_RTC_saved = PlayerPrefs.GetFloat("x_RTC");
        float y_RTC_saved = PlayerPrefs.GetFloat("y_RTC");
        float z_RTC_saved = PlayerPrefs.GetFloat("z_RTC");



        Vector3 _LeftBottomCorner = new Vector3(x_LBC_saved, y_LBC_saved, z_LBC_saved);
        Vector3 _RightBottomCorner = new Vector3(x_RBC_saved, y_RBC_saved, z_RBC_saved);
        Vector3 _LeftTopCorner = new Vector3(x_LTC_saved, y_LTC_saved, z_LTC_saved);
        Vector3 _RightTopCorner = new Vector3(x_RTC_saved, y_RTC_saved, z_RTC_saved);


        //Creating of plane 

        GameObject Plane = new GameObject();
        MeshRenderer meshRenderer = Plane.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = Plane.AddComponent<MeshFilter>();

        meshRenderer.sharedMaterial= new Material(Shader.Find("Standard"));    
        meshRenderer.sharedMaterial.color = Color.blue;    
         
         
        
        Mesh m = new Mesh();

        //Vertex Array 
        m.vertices = new Vector3[4]
        {
                 _LeftBottomCorner,  //Corresponse to 0
                 _RightBottomCorner, //Corresponse to 1
                 _LeftTopCorner,     //Corresponse to 2
                 _RightTopCorner     //Corresponse to 3
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

    }
    private void Awake()
    {
        //reference AR Raycastmanager 
        _arRaycastManager = GetComponent<ARRaycastManager>();


    }

    private void Update()
    {

        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;


        if (_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            bool isOverUI = touchPosition.IsPointOverUIObject();



                if (!isOverUI && placedPrefabCount < maxPreabSpawnCount)
                {
                    SpwanPrefab(hitPose);
                }
            





        }

    }
}


