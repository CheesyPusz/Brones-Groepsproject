using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

using UnityEngine.SceneManagement;
using NUnit.Framework;
using System.Collections.Generic;

public class Instruction : MonoBehaviour
{
    public PatientInfoApiClient patientInfoApiClient;
    public IntroManager introManager;
    public TMP_Text nameLabel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ReadInfoPatients();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private async void ReadInfoPatients()
    {
        // Call the Login method in UserApiClient
        IWebRequestReponse webRequestResponse = await patientInfoApiClient.ReadPatientsInformation();

        switch (webRequestResponse)
        {
            case WebRequestData<List<PatientInfo>> listResponse:
                // Handle the list of PatientInfo objects
                Debug.Log("List Response");
                foreach (var patient in listResponse.Data)
                {
                    Debug.Log($"Patient: {patient.name}, Doctor: {patient.naamArts}");
                    introManager.Name = patient.name;
                }
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Register error: " + errorMessage);
                // TODO: Handle error scenario. Show the error message to the user.
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }
}
