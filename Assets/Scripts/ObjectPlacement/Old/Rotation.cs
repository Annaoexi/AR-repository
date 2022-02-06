using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Rotation : MonoBehaviour
{
    
    public PlacementOfObjects _PlacementOfObjects;

    private GameObject ObjectToRotate;

    [SerializeField]
    Slider slider;
   
    private float previousValue;

     void Awake ()
     {
         _PlacementOfObjects = ObjectToRotate.GetComponent<PlacementOfObjects>();
         
     }
    public void OnSliderChanged()
    {
               
        ObjectToRotate = PlacementOfObjects.gameObjectToInstantiate;
        float SilderValue = GetComponent<Slider>().value;
        ObjectToRotate.transform.rotation = Quaternion.Euler(SilderValue,0,0);
       
    }

    

}
