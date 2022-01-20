using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyButton : MonoBehaviour
{
    public void DeleteAll(){
        foreach (GameObject o in Object.FindObjectsOfType<GameObject>()){
            Destroy(o);
        }
    }
}
