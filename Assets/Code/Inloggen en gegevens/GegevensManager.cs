using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Deze klasse uiteindelijk uitbreiden voor de info als dat lukt

public class GegevensManager : MonoBehaviour
{
    
    public Toggle toggleA;
    public Toggle toggleB;
    public TMP_InputField NaamKind;
    public TMP_InputField NaamArts;
    void Start()
    {
        // Zorgt ervoor dat beide toggles uit staan bij start
        toggleA.isOn = false;
        toggleB.isOn = false;
    }
    public void Continue()
    {
        SceneManager.LoadScene("IntroductieScherm");
    }

}
