using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.Linq;

public class IntroManager : MonoBehaviour
{
    public PatientInfoApiClient patientInfoApiClient;
    private int Count;
    public TMP_Text m_Text;
    public string Name;
    private string route;

    private void Start() // Zorgt dat als de scene wordt geladen de juiste gegevens worden opgehaald
    {
        if (SceneManager.GetActiveScene().name == "IntroductieScherm")
        {
            GetPatientInfo();
            Change();

        }
    }

    public void Logout()
    {
        ClearTokens(); // Verwijdert tokens
        SceneManager.LoadScene("LoginScene"); // Laad het inlogscherm
    }

    public void ClearTokens() // Verwijdert de tokens die opgeslagen zijn bij het inloggen
    {
        Debug.Log("Succesvol uitgelogd");
        PlayerPrefs.DeleteKey("accessToken");
        PlayerPrefs.DeleteKey("refreshToken");
        PlayerPrefs.Save();
    }

    public void TestGegevens() // Deze gegevens worden gebruikt om te testen voordat de API werkt
    {
        Count = 0;
        Name = "Test123";
        route = "A";
        // route = "B";
    }

    public void IncreaseCount()
    {
        Count++;
        Change();
    }

    public void CountBack()
    {
        Count--;
        Change();
    }

    private void Change() // Deze methode zorgt dat voor elke keer als er geklikt wordt de juiste tekst wordt geladen
    {
        if (Count == 0) { m_Text.text = Text1(); }
        else if (Count == 1) { m_Text.text = Text2(); }
        else if (Count == 2) { m_Text.text = Text3(); }
        else if (Count == 3) { m_Text.text = Text4(); }
        else if (Count == 4) { m_Text.text = Text5(); }
        else if (Count == 5) { m_Text.text = Text6(); }
        else if (Count == 6) { m_Text.text = Text7(); }
        else if (Count == 7 && route == "B") { SceneManager.LoadScene("Route B"); } // De route wordt na de tekst geladen
        else if (Count == 7 && route == "A") { SceneManager.LoadScene("Route A"); }
    }

    public async void GetPatientInfo()
    {
        // Call the Login method in UserApiClient
        IWebRequestReponse webRequestResponse = await patientInfoApiClient.ReadPatientsInformation();

        switch (webRequestResponse)
        {
            case WebRequestData<List<PatientInfo>> listResponse:
                // Handle the list of PatientInfo objects
                if (listResponse.Data == null || listResponse.Data.Count == 0)
                {
                    Debug.LogError("No patient info found in the response");
                    return;
                }

                PatientInfo firstPatientInfo = listResponse.Data.FirstOrDefault();
                if (firstPatientInfo == null)
                {
                    Debug.LogError("No patient info found in the list");
                    return;
                }

                // Save the name and behandelPlan values in local variables
                Name = firstPatientInfo.name;
                route = firstPatientInfo.behandelPlan;
                PlayerPrefs.SetFloat("PositionX", firstPatientInfo.positionX);
                PlayerPrefs.SetString("id", firstPatientInfo.userId);
                Debug.Log($"Patient: {firstPatientInfo.name}, Doctor: {firstPatientInfo.naamArts}, Route: {route}");
                Change();
                break;

            case WebRequestData<PatientInfo> singleResponse:
                // Handle the single PatientInfo object
                PatientInfo patientInfo = singleResponse.Data;
                if (patientInfo == null)
                {
                    Debug.LogError("No patient info found in the response!!!!");
                    return;
                }

                // Save the name and behandelPlan values in local variables
                Name = patientInfo.name;
                route = patientInfo.behandelPlan;

                Debug.Log($"Patient: {patientInfo.name}, Doctor: {patientInfo.naamArts}, Route: {route}");
                Change();
                break;

            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.LogError("Register error: " + errorMessage);
                // TODO: Handle error scenario. Show the error message to the user.
                break;

            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    // De methodes waar de teksten in staan:
    #region Texten 
    private string Text1()
    {
        return $"Hallo {Name}!!\nIk ben Gekke Henkie en ik heb al 10 botjes gebroken, omdat ik zo bekend ben met het ziekenhuis ga ik met je mee en leg ik je uit wat er gaat gebeuren";
    }

    private string Text2()
    {
        if (route == "B")
        {
            return $"Net als mij heb jij ook een botje gebroken, om die te repareren moeten de dokters jou opereren, Dit kan een beetje eng zijn.";
        }
        else
        {
            return $"Net als mij heb jij ook een botje gebroken, die moet weer gerepareerd worden.";
        }
    }

    private string Text3()
    {
        return $"Maar maak je maar niet druk, je bent in goede handen bij de dokters en zei gaan voor jou het botje weer repareren, je hoeft dus niet bang te zijn.";
    }

    private string Text4()
    {
        return $"Gelukkig heb jij precies hetzelfde botje gebroken als mij en het is even erg, daarom lopen wij samen door de stappen en leg ik je alles uit.";
    }

    private string Text5()
    {
        return $"Het enige dat ik wil is dat jij je ouders erbij roept, zodat zij ook de stappen begrijpen en weten wat er gaat gebeuren";
    }

    private string Text6()
    {
        return $"Je kunt bewegen met de pijltjes toetsen op het toetsenbord of met A om naar links te gaan en D om naar rechts te gaan.";
    }

    private string Text7()
    {
        return $"Onderweg Komen we stukjes informatie tegen. Door weer door te bewegen verdwijnen deze automatisch!\n {Name}, heel veel succes!";
    }
    #endregion
}