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

    //[SerializeField]
    //private TMP_Dropdown Hospital;

    //[SerializeField]
    //private TMP_Dropdown Roomtype;


    //Variables 
    string TextProjectName;
    string TextProjectManager;

    //const string PrefName = "OptionValue";


    //Methods

    void Awake()
    {

    }
    void Start()
    {   //Get Info from input fields 
        TextProjectName = PlayerPrefs.GetString("tutorialTextKeyName");
        ProjectName.text = TextProjectName;

        TextProjectManager = PlayerPrefs.GetString("tutorialTextKeyName");
        ProjectManager.text = TextProjectManager;

        //Get info from Dropdown menu
        //Hospital.value = PlayerPrefs.GetInt("HospitalDropdown");
       



    }

    


    public void Save()
        {
            TextProjectName = ProjectName.text;
            PlayerPrefs.SetString("tutorialTextKeyName", TextProjectName);

            TextProjectManager = ProjectManager.text;
            PlayerPrefs.SetString("tutorialTextKeyName", TextProjectManager);




        }


    }
