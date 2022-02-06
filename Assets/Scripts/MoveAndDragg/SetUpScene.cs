using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpScene : MonoBehaviour
{
    
    void Start()
    {   //Getting Gameobjects 

        DontDestroyOnLoad (this.gameObject);
        
        //Set Up of panel
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
