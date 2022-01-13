using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;


public class ButtonHandler : MonoBehaviour
{
    //Varialble um auf andere skripte zu verweisen 
    //[SerializeField]
    //private BoundarySetter _boundarySetter; //Brauch ma erstmal ned weil der boundary setter nicht geht 


    //Variablen für die Objektplatzierung 

   
    public static GameObject placedObject; //Objekt das auserwählt wird 
    



    //Method um objecte auszuwählen 
    public void setObject(GameObject selectedObject)
    {
        placedObject = selectedObject;

    }
    //Übergebe die Corner points oder die plane je nachdem was ich noch im ARTaptoPlace visaulisieren lassen 
    //Vielleicht kann ich hier auf die Plane zugreifen auch wenn sie nicht visualisiert ist?   //

   // public void setBoundaries()
    //{
        //Erstellen wir hier die plane? 
        //Vector3 LeftTopCorner = planeOutlinePoints[0]; 

        //  Vector3 RightTopCorner = planeOutlinePoints[1];
        //  Vector3 LeftBottomCorner = planeOutlinePoints[2];
        //  Vector3 RightBottomCorner = planeOutlinePoints[3]; //Man braucht eigentlich nur 3 Punkte um das Plane zu erstellen 


        //var plane = new Plane(LeftTopCorner, RightTopCorner, LeftBottomCorner);
        //  var center = (LeftTopCorner + RightTopCorner + LeftBottomCorner) / 3f;
        // float LenghtOfPlane = RightTopCorner.x - RightBottomCorner.x;
        // float WidthOfPlane = LeftTopCorner.y - LeftBottomCorner.y;

        // Vector3 SizeOfPlane = new Vector3(LenghtOfPlane, WidthOfPlane, 0);


        //Draw plane 
        //Gizmos.color = Color.cyan;
        //  Gizmos.DrawCube(center, SizeOfPlane);
   // }

    

    //public void PlaceObject(Vector3 position, Quaternion rotation)
    //{
        //Instantiate(gameObjectToInstantiate, position, rotation);
        




}

