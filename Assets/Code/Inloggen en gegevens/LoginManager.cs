using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System;

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

        // Process the response
        if (webRequestResponse is WebRequestData<string> dataResponse && dataResponse.Data == "inside ProcessLoginResponse() - Login successful")
        {
            Debug.Log("Login successful! Access token is set.");

            // put code here for setting the token using webClient (stored in PlayerPrefs)

        }
        else
        {
            Debug.LogError("Login failed or invalid response: " + webRequestResponse?.ToString());
            // Handle login failure (e.g., show an error message)
        }
    }





    //private IEnumerator LoginUser(string username, string password)
    //{
    //    string jsonBody = $"{{\"email\":\"{username}\",\"password\":\"{password}\"}}";
    //    byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonBody);

    //    UnityWebRequest request = new UnityWebRequest($"{baseUrl}/Login", "POST");
    //    request.uploadHandler = new UploadHandlerRaw(jsonToSend);
    //    request.downloadHandler = new DownloadHandlerBuffer();
    //    request.SetRequestHeader("Content-Type", "application/json");

    //    yield return request.SendWebRequest();

    //    if (request.result == UnityWebRequest.Result.Success)
    //    {
    //        string responseText = request.downloadHandler.text;
    //        string token = ExtractToken(responseText);
    //        string refreshToken = ExtractRefreshToken(responseText);

    //        if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(refreshToken))
    //        {
    //            AuthManager.Instance.SaveTokens(token, refreshToken);
    //            UpdateMessage("Login succesvol!", Color.green);
    //            UnityEngine.SceneManagement.SceneManager.LoadScene("EnvironmentMain");
    //            //hier moet de volgend
    //        }
    //        else
    //        {
    //            UpdateMessage("Fout: Geen geldige token ontvangen.", Color.red);
    //        }
    //    }
    //    else
    //    {
    //        UpdateMessage("Inloggen mislukt! Controleer je gegevens.", Color.red);
    //    }
    //}

    //private IEnumerator RegisterUser(string email, string password)
    //{
    //    string jsonBody = $"{{\"email\":\"{email}\",\"password\":\"{password}\"}}";
    //    byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonBody);

    //    UnityWebRequest request = new UnityWebRequest($"{baseUrl}/register", "POST");
    //    request.uploadHandler = new UploadHandlerRaw(jsonToSend);
    //    request.downloadHandler = new DownloadHandlerBuffer();
    //    request.SetRequestHeader("Content-Type", "application/json");

    //    yield return request.SendWebRequest();

    //    if (request.result == UnityWebRequest.Result.Success)
    //    {
    //        UpdateMessage("Registratie succesvol! Je kunt nu inloggen.", Color.green);
    //    }
    //    else
    //    {
    //        UpdateMessage("Registratie mislukt! Probeer het opnieuw.", Color.red);
    //    }
    //}

    //private string ExtractToken(string responseText)
    //{
    //    int tokenStartIndex = responseText.IndexOf("\"accessToken\":\"") + "\"accessToken\":\"".Length;
    //    int tokenEndIndex = responseText.IndexOf("\"", tokenStartIndex);

    //    if (tokenStartIndex >= 0 && tokenEndIndex >= 0)
    //    {
    //        return responseText.Substring(tokenStartIndex, tokenEndIndex - tokenStartIndex);
    //    }

    //    return null;
    //}

    //private string ExtractRefreshToken(string responseText)
    //{
    //    int tokenStartIndex = responseText.IndexOf("\"refreshToken\":\"") + "\"refreshToken\":\"".Length;
    //    int tokenEndIndex = responseText.IndexOf("\"", tokenStartIndex);

    //    if (tokenStartIndex >= 0 && tokenEndIndex >= 0)
    //    {
    //        return responseText.Substring(tokenStartIndex, tokenEndIndex - tokenStartIndex);
    //    }

    //    return null;
    //}

    //public void ResetFields()
    //{
    //    usernameInputField.text = "";
    //    passwordInputField.text = "";
    //    UpdateMessage("Velden gereset!", Color.yellow);
    //}

    private void UpdateMessage(string text, Color color)
    {
        messageLabel.text = text;
        messageLabel.color = color;
    }


}
//using System.Threading.Tasks;
//using TMPro;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class LoginManager : MonoBehaviour
//{
//    [SerializeField] private TMP_InputField emailField;
//    [SerializeField] private TMP_InputField passwordField;
    
//    private string difficulty = "not selected";
//    private string selectedRoute = "not selected";
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    public void SelectYoungDifficulty(bool toggleValue)
//    {
//        if (toggleValue == true)
//        {
//            Debug.Log("selected Young child mode");
//            difficulty = "young";
//        }
//        else
//        {
//            difficulty = "not selected";
//        }
//    }

//    public void SelectOldDifficulty(bool toggleValue) 
//    {
//        if (toggleValue == true)
//        {
//            Debug.Log("selected old child mode");
//            difficulty = "old";
//        }
//        else 
//        {
//            difficulty = "not selected";
//        }
//    }

//    public void SelectRouteA(bool toggleValue)
//    {
//        if (toggleValue == true)
//        {
//            Debug.Log("selected route A: gips");
//            selectedRoute = "A";
//            Debug.Log(selectedRoute);
//        }
//        else
//        {
//            selectedRoute = "not selected";
//            Debug.Log("desected route A");
//        }
//    }

//    public void SelectRouteB(bool toggleValue)
//    {
//        if (toggleValue == true)
//        {
//            Debug.Log("selected route B: operatie");
//            selectedRoute = "B";
//            Debug.Log(selectedRoute);
//        }
//        else
//        {
//            selectedRoute = "not selected";
//            Debug.Log("deselected route B");
//        }
//    }
//    public async void Login()
//    {
//        string username = emailField.text;
//        string password = passwordField.text;
//        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
//        {
//            Debug.Log("Username or password is empty");
//            return;
//        }
//        Debug.Log($"Logging in with username: {username} and password: {password} with difficulty: {difficulty}");
//        SceneManager.LoadScene("SampleScene");
//        await Task.Delay(1000); // added this so i can have an asyn Login(), which is needed for awaiting the webrequest. afterwards delete this await Task.Delay(1000)
//    }

//    public async void Register()
//    {
//        string username = emailField.text;
//        string password = passwordField.text;
//        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
//        {
//            Debug.Log("Username or password is empty");
//            return;
//        }
//        if (difficulty == "not selected" || selectedRoute == "not selected")
//        {
//            Debug.Log("Difficulty is not selected");
//            return;
//        }
//        if (selectedRoute == "not selected")
//        {
//            Debug.Log("Route is not selected");
//            return;
//        }
//        Debug.Log($"Selected route: {selectedRoute}");

//        Debug.Log($"Registering with username: {username}, password: {password} with difficulty: {difficulty}");

//        SceneManager.LoadScene("IntroductieScherm");
//        await Task.Delay(1000); // added this so i can have an asyn Register(), which is needed for awaiting the webrequest. afterwards delete this await Task.Delay(1000)
//    }

//}
