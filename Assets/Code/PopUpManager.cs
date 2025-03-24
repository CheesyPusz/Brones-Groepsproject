using UnityEngine;
using TMPro; // Vergeet niet TextMeshPro te importeren

public class TreePopup : MonoBehaviour
{
    public GameObject popupUI;        // De UI-container met afbeelding en tekst
    public TextMeshProUGUI popupText; // De tekst die wordt weergegeven
    public GameObject player;         // De speler
    public float triggerX;            // De x-waarde waarop de popup verschijnt
    public float hideX;               // De x-waarde waarop de popup verdwijnt
    public string Message;            // De tekst die wordt weergegeven
    private void Start()
    {
        popupUI.SetActive(false); // Verberg de popup bij de start
    }

    private void Update()
    {
        if (player != null)
        {
            // Controleer de x-positie van de speler
            if (player.transform.position.x >= triggerX && player.transform.position.x <= hideX)
            {
                popupUI.SetActive(true); // Toon de UI
                popupText.text = Message; // Stel de tekst in
            }
            else
            {
                popupUI.SetActive(false); // Verberg de UI
            }
        }
    }
}
