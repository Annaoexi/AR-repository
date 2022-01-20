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
    
    

}

