using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    public UserApiClient userApiClient;
    public TMP_InputField usernameInputField;
    public TMP_InputField passwordInputField;
    public TMP_Text messageLabel;

    //private string baseUrl = "https://avansict2237041.azurewebsites.net/account";

    public void Login()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            UpdateMessage("Vul alle velden in!", Color.red);
            return;
        }

        if (!IsValidPassword(password))
        {
            UpdateMessage("Wachtwoord moet minstens 10 tekens lang zijn, 1 kleine letter, 1 hoofdletter, 1 cijfer en 1 speciaal teken bevatten!", Color.red);
            return;
        }

        LoginUser(username, password);
    }

    public async void Register()
    {

        User user = new User
        {
            email = usernameInputField.text,
            password = passwordInputField.text
        };
        IWebRequestReponse webRequestResponse = await userApiClient.Register(user);

        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                Debug.Log("Register succes!");
                //ga naar de Gegevens scene om daar je patientInfo in  tevullen
                SceneManager.LoadScene("Gegevens");
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

    private bool IsValidPassword(string password)
    {
        return password.Length >= 10 && Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{10,}$");
    }

    private async void LoginUser(string username, string password)
    {
        // Create the user object with email and password from input fields
        User user = new User
        {
            email = username,
            password = password
        };

        // Call the Login method in UserApiClient
        IWebRequestReponse webRequestResponse = await userApiClient.Login(user);

        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                Debug.Log("Register succes!");
                // TODO: Handle succes scenario;
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

    private void UpdateMessage(string text, Color color)
    {
        messageLabel.text = text;
        messageLabel.color = color;
    }
}