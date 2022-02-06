using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SaveButton : MonoBehaviour
{
    //Connections to UI Components 
    [SerializeField]
    private TMP_InputField ProjectName;

    [SerializeField]
    private TMP_InputField ProjectManager;

    [SerializeField]
    private TMP_Dropdown Hospital;

    [SerializeField]
    private TMP_Dropdown Roomtype;


    //Variables 
    string TextProjectName;
    string TextProjectManager;

    int ValueHospitalRoom; 

    int ValueRoomType; 


 
    void Start()
    {   //Get Info from input fields 
        TextProjectName = PlayerPrefs.GetString("ProjectName");
        ProjectName.text = TextProjectName;

        TextProjectManager = PlayerPrefs.GetString("ProjectManager");
        ProjectManager.text = TextProjectManager;

        //Get info from Dropdown menu
        ValueHospitalRoom = PlayerPrefs.GetInt("HospitalDropdown");
        Hospital.value = ValueHospitalRoom;
        

        ValueRoomType = PlayerPrefs.GetInt("Room Type");
        Roomtype.value = ValueRoomType;
       
    }



    public void Save()
        {
            TextProjectName = ProjectName.text;
            PlayerPrefs.SetString("ProjectName", TextProjectName);

            TextProjectManager = ProjectManager.text;
            PlayerPrefs.SetString("ProjectManager", TextProjectManager);

            ValueHospitalRoom = Hospital.value;
            PlayerPrefs.SetInt("HospitalDropdown", ValueHospitalRoom);

            ValueRoomType = Roomtype.value;
            PlayerPrefs.SetInt("Room Type", ValueRoomType);

            

        }


    }
