using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    
    private GameObject objectToPlace; //Objekt das auserwählt wird 

    public void setObject (GameObject selectedObject){
        objectToPlace = selectedObject; 

    }
    //Übergebe die Corner points oder die plane je nachdem was ich noch im ARTaptoPlace visaulisieren lassen 
    


    public void setBoundaries(){

    }
    
   public void PlaceObject (Vector3 position, Quaternion rotation){
       Instantiate(objectToPlace, position, rotation); // check if in boundaries ansonsten fehlermeldung/Hinweis 
   }     
    
}

