using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField emailField;
    [SerializeField] private TMP_InputField passwordField;
    
    private string difficulty = "not selected";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectYoungDifficulty(bool toggleValue)
    {
        if (toggleValue == true)
        {
            Debug.Log("selected Young child mode");
            difficulty = "young";
        }
        else
        {
            difficulty = "not selected";
        }
    }

    public void SelectOldDifficulty(bool toggleValue) 
    {
        if (toggleValue == true)
        {
            Debug.Log("selected old child mode");
            difficulty = "old";
        }
        else 
        {
            difficulty = "not selected";
        }
    }

    public async void Login()
    {
        string username = emailField.text;
        string password = passwordField.text;
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Debug.LogError("Username or password is empty");
            return;
        }
        if (difficulty == "not selected") 
        {
            Debug.LogError("Difficulty is not selected");
            return;
        }
        Debug.Log($"Logging in with username: {username} and password: {password} with difficulty: {difficulty}");
        // Call the login API
        // await LoginAPI.Login(username, password);
        // Load the next scene
        SceneManager.LoadScene("SampleScene");
        await Task.Delay(1000); // added this so i can have an asyn Login(), which is needed for awaiting the webrequest. afterwards delete this await Task.Delay(1000)
    }


}
