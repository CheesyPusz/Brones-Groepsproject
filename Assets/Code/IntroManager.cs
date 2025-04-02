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
        return $"Hallo {Name}!!\nIk ben Jeff en heb mijn spaakbeen gebroken bij de judo ik loop met je mee door de stappen van jouw proces ";
    }

    private string Text2()
    {
        if (route == "B")
        {
            return $"Je hebt iets ernstig gebroken en moet geopereerd worden door een dokter daarom ben je vast een beetje bang";
        }
        else
        {
            return $"Je hebt iets gebroken maar gelukkig is het niet heel erg  ";
        }
    }

    private string Text3()
    {
        return $"Maar maak je niet druk \nJe bent in goede handen en hoeft je nergens druk over te maken";
    }

    private string Text4()
    {
        return $"In deze app gaan wij jou stap voor stap uitleggen hoe jouw bot gaat genezen door samen met mij route {route} te belopen.";
    }

    private string Text5()
    {
        return $"Als het goed is heeft een ouder je net geholpen met in te loggen. Vraag of die ook mee wil kijken naar het spel";
    }

    private string Text6()
    {
        return $"Bewegen doe je met de pijltjes toetsen voor links en naar rechts of met de knoppen A en D op het toetsenbord!!";
    }

    private string Text7()
    {
        return $"Onderweg Komen we stukjes informatie tegen. Door weer door te bewegen verdwijnen deze automatisch!\n {Name} heel veel succes";
    }
    #endregion
}