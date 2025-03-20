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

    public void ToggleMap()
    {
        isMapVisible = !isMapVisible;
        MapImage.SetActive(isMapVisible);
        buttonText.text = isMapVisible ? "Hide Map" : "Show Map";
    }
    //
}
