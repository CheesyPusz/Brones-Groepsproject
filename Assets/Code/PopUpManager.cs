using UnityEngine;

public class TreePopup : MonoBehaviour
{
    public GameObject popupUI;        // De UI afbeelding die je wilt tonen
    public GameObject player;         // Sleep hier de speler in de Inspector
    public float triggerX;// = 10f;      // De x-waarde waarop de popup moet verschijnen
    public float hideX;// = 12f;         // De x-waarde waarop de popup weer moet verdwijnen
    //public GameObject MapImage;
    private void Start()
    {
        popupUI.SetActive(false); // Zorg ervoor dat de UI afbeelding in het begin niet zichtbaar is
    }

    private void Update()
    {
        if (player != null)
        {
            // Controleer de x-positie van de speler
            if (player.transform.position.x >= triggerX && player.transform.position.x <= hideX)
            {
                popupUI.SetActive(true); // Toon de UI afbeelding als de speler tussen triggerX en hideX is
            }
            else
            {
                popupUI.SetActive(false); // Verberg de UI afbeelding als de speler buiten dat bereik is
            }
        }
    }
}

/*using UnityEngine;
using UnityEngine.UI; // Vergeet niet de UI namespace toe te voegen

public class TreePopup : MonoBehaviour
{
    public GameObject popupUI; // Sleep hier je UI-element in de inspector (bijvoorbeeld een UI Image)

    private void Start()
    {
        // Zorg ervoor dat de UI afbeelding in het begin niet zichtbaar is
        popupUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Controleer of de speler de triggerzone binnengaat
        if (other.CompareTag("Player"))
        {
            popupUI.SetActive(true); // Maak de UI afbeelding zichtbaar
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Verberg de afbeelding als de speler de triggerzone verlaat
        if (other.CompareTag("Player"))
        {
            popupUI.SetActive(false); // Verberg de UI afbeelding
        }
    }
}
*/