using UnityEngine;
using TMPro; // Vergeet niet TextMeshPro te importeren
using UnityEngine.SceneManagement;
public class TreePopup : MonoBehaviour
{
    public GameObject popupUI;        // De UI-container met afbeelding en tekst
    public TextMeshProUGUI popupText; // Het tekst element
    public GameObject player;         // De speler
    public float triggerX;            // De x-waarde waarop de popup verschijnt
    public float hideX;               // De x-waarde waarop de popup verdwijnt
    public string Message;            // De tekst die wordt weergegeven
    private void Start()
    {
        popupUI.SetActive(false); // Verberg de popup bij de start
    }
    public void backToIntro()
    {
        SceneManager.LoadScene("IntroductieScherm");
    }
    private void Update()
    {
        if (player != null)
        {
            // Controleer de x-positie van de speler
            if (player.transform.position.x >= triggerX && player.transform.position.x <= hideX)
            {
                popupUI.SetActive(true); // Toont het frame voor de text
                popupText.text = Message; //  De ingstelde teskst word weergegeven
            }
            else
            {
                popupUI.SetActive(false); // Verberg de UI
            }
        }
    }

}
