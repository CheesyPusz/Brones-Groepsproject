using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoutButton : MonoBehaviour
{
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
        PlayerPrefs.DeleteKey("HasJustRegistered");
        PlayerPrefs.Save();
    }
}
