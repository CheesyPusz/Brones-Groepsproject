using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Deze klasse uiteindelijk uitbreiden voor de info als dat lukt

public class GegevensManager : MonoBehaviour
{
 
    public PatientInfoApiClient patientInfoApiClient;
    public Toggle toggleA;
    public Toggle toggleB;
    public TMP_InputField NaamKind;
    public TMP_InputField NaamArts;

    public TMP_InputField birthDateYear;
    public TMP_InputField birthDateMonth;
    public TMP_InputField birthDateDay;

    public TMP_InputField afspraakYear;
    public TMP_InputField afspraakMonth;
    public TMP_InputField afspraakDay;

    private string selectedRoute = "not selected";
    void Start()
    {
        // Zorgt ervoor dat beide toggles uit staan bij start
        toggleA.isOn = false;
        toggleB.isOn = false;
    }
    //public void Continue()
    //{
    //    SceneManager.LoadScene("IntroductieScherm");
    //}

    public async void Register()
    {
        string textKind = NaamKind.text;
        string textArts = NaamArts.text;
        DateTime birthDate = new DateTime(int.Parse(birthDateYear.text), int.Parse(birthDateMonth.text), int.Parse(birthDateDay.text));
        DateTime afspraakDate = new DateTime(int.Parse(afspraakYear.text), int.Parse(afspraakMonth.text), int.Parse(afspraakDay.text));

        if (string.IsNullOrEmpty(textKind) || string.IsNullOrEmpty(textArts))
        {
            Debug.Log("Username or password is empty");
            return;
        }

        if (selectedRoute == "not selected")
        {
            Debug.Log("Route is not selected");
            return;
        }

        if (birthDate == null || afspraakDate == null)
        {
            Debug.Log("Birthdate or afspraakdate is null");
            return;
        }

        Debug.Log($"Registering with name: {textKind}, docotr: {textArts} with route: {selectedRoute}, with birthDay {birthDate} with afspraak {afspraakDate}");

        //de datums gecommend aangezien de api DateNow gebruikt,
        //in unity is DateNow niet beschikbaar en aangezien alles hier null mag zijn staat het voor nu uitgekommend
        PatientInfo patientInfo = new PatientInfo
        {
            name = textKind,
            dateOfBirth = birthDate,
            behandelPlan = selectedRoute,
            naamArts = textArts,
            eersteAfspraak = afspraakDate
        };
        IWebRequestReponse webRequestResponse = await patientInfoApiClient.CreatePatientInfo(patientInfo);

        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                Debug.Log("Register succes!");
                Debug.Log("Patient info: " + dataResponse.Data);
                SceneManager.LoadScene("IntroductieScherm");
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Register error: " + errorMessage);
                // TODO: Handle error scenario. Show the errormessage to the user.

                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
        
    }

    public void SelectRouteA(bool toggleValue)
    {
        if (toggleValue == true)
        {
            Debug.Log("selected route A: gips");
            selectedRoute = "A";
            Debug.Log(selectedRoute);
        }
        else
        {
            selectedRoute = "not selected";
            Debug.Log("desected route A");
        }
    }

    public void SelectRouteB(bool toggleValue)
    {
        if (toggleValue == true)
        {
            Debug.Log("selected route B: operatie");
            selectedRoute = "B";
            Debug.Log(selectedRoute);
        }
        else
        {
            selectedRoute = "not selected";
            Debug.Log("deselected route B");
        }
    }
}
