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
    private string selectedRoute = "not selected";
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


    public async void Login()
    {
        string username = emailField.text;
        string password = passwordField.text;
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Debug.Log("Username or password is empty");
            return;
        }
        Debug.Log($"Logging in with username: {username} and password: {password} with difficulty: {difficulty}");
        SceneManager.LoadScene("SampleScene");
        await Task.Delay(1000); // added this so i can have an asyn Login(), which is needed for awaiting the webrequest. afterwards delete this await Task.Delay(1000)
    }

    public async void Register()
    {
        string username = emailField.text;
        string password = passwordField.text;
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Debug.Log("Username or password is empty");
            return;
        }
        if (difficulty == "not selected" || selectedRoute == "not selected")
        {
            Debug.Log("Difficulty is not selected");
            return;
        }
        if (selectedRoute == "not selected")
        {
            Debug.Log("Route is not selected");
            return;
        }
        Debug.Log($"Selected route: {selectedRoute}");

        Debug.Log($"Registering with username: {username}, password: {password} with difficulty: {difficulty}");

        SceneManager.LoadScene("SampleScene");
        await Task.Delay(1000); // added this so i can have an asyn Register(), which is needed for awaiting the webrequest. afterwards delete this await Task.Delay(1000)
    }

}
