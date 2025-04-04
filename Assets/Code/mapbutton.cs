using TMPro;
using UnityEngine;

public class mapbutton : MonoBehaviour
{
    public GameObject MapImage;
    public TMP_Text buttonText;
    private bool isMapVisible;
    void Start()
    {
        MapImage.SetActive(false);
        isMapVisible = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap();
        }
    }
    public void ToggleMap()
    {
        isMapVisible = !isMapVisible;
        MapImage.SetActive(isMapVisible);
        buttonText.text = isMapVisible ? "Verberg Kaart" : "Kaart";
    }
    //
}
