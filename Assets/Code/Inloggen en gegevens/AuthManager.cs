using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance { get; private set; }
    private string baseUrl = "https://avansict2237041.azurewebsites.net/account";
    //Voor locaal testen
    //private string baseUrl = "https://localhost:7086/account"
    private string authToken;
    private string refreshToken;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Blijft bestaan tussen scenes
        }
        else
        {
            Destroy(gameObject);
        }

        LoadTokens();
    }

    private void LoadTokens()
    {
        authToken = PlayerPrefs.GetString("auth_token", "");
        refreshToken = PlayerPrefs.GetString("refresh_token", "");
    }

    public string GetToken()
    {
        return authToken;
    }

    public void SaveTokens(string newAuthToken, string newRefreshToken)
    {
        authToken = newAuthToken;
        refreshToken = newRefreshToken;
        PlayerPrefs.SetString("auth_token", newAuthToken);
        PlayerPrefs.SetString("refresh_token", newRefreshToken);
        PlayerPrefs.Save();
    }

    public IEnumerator RefreshToken()
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            Debug.Log("Geen refresh token beschikbaar, opnieuw inloggen vereist.");
            yield break;
        }

        string jsonBody = $"{{\"refreshToken\":\"{refreshToken}\"}}";
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonBody);

        UnityWebRequest request = new UnityWebRequest($"{baseUrl}/refresh", "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseText = request.downloadHandler.text;
            string newAuthToken = ExtractToken(responseText);
            string newRefreshToken = ExtractRefreshToken(responseText);

            if (!string.IsNullOrEmpty(newAuthToken))
            {
                SaveTokens(newAuthToken, newRefreshToken);
                Debug.Log("Token succesvol vernieuwd!");
            }
        }
        else
        {
            Debug.LogError("Token refresh mislukt, opnieuw inloggen vereist.");
        }
    }

    private string ExtractToken(string responseText)
    {
        int tokenStartIndex = responseText.IndexOf("\"accessToken\":\"") + "\"accessToken\":\"".Length;
        int tokenEndIndex = responseText.IndexOf("\"", tokenStartIndex);
        return tokenStartIndex >= 0 && tokenEndIndex >= 0 ? responseText.Substring(tokenStartIndex, tokenEndIndex - tokenStartIndex) : null;
    }

    private string ExtractRefreshToken(string responseText)
    {
        int tokenStartIndex = responseText.IndexOf("\"refreshToken\":\"") + "\"refreshToken\":\"".Length;
        int tokenEndIndex = responseText.IndexOf("\"", tokenStartIndex);
        return tokenStartIndex >= 0 && tokenEndIndex >= 0 ? responseText.Substring(tokenStartIndex, tokenEndIndex - tokenStartIndex) : null;
    }

}
