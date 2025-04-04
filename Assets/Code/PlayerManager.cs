using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.UIElements.ToolbarMenu;

public class PlayerManager : MonoBehaviour
{
    PatientInfoApiClient patientInfoApiClient;
    public float speed = 5f;
    public static PlayerManager Singleton;
    public bool routeA; //Hierdoor kan je met een knop het limiet aanpassen
    private SpriteRenderer playerSprite;
    private bool facingRight = true; // Houdt bij of de speler naar rechts kijkt
    private int limit;
    public GameObject BigPlayerHumanVariant; // Reference to the existing GameObject

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        if (!routeA)
        {
            limit = 3366; //limiet voor route B
        }
        else
        {
            limit = 2230; //limiet voor route A
        }

        // Find the existing BigPlayerHumanVariant GameObject
        GameObject playerHuman = GameObject.Find(BigPlayerHumanVariant.name);
        if (playerHuman != null)
        {
            // Retrieve the PositionX value from PlayerPrefs
            float positionX = PlayerPrefs.GetFloat("PositionX", -133f); // Default to -133 if not found

            // Set the position of the existing GameObject
            playerHuman.transform.position = new Vector3(positionX, 220f, 0);
            Debug.Log("Set position of BigPlayerHumanVariant to positionX: " + positionX);
        }
        else
        {
            Debug.LogError("BigPlayerHumanVariant GameObject not found in the hierarchy.");
        }
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        // Beweging met WASD en pijltjestoetsen
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && transform.position.x > -133) moveDirection.x -= 1; //Beweegt de speler naar links.
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && transform.position.x < limit) moveDirection.x += 1; //Beweegt de speler naar rechts maar niet verder dan het limiet van de route.

        // Verander de richting van de speler
        if (moveDirection.x > 0 && !facingRight) //gaat na naar welke kant de speler kijkt en of  speler moet draaien
        {
            Flip();
        }
        else if (moveDirection.x < 0 && facingRight) //gaat na naar welke kant de speler kijkt en of  speler moet draaien
        {
            Flip();
        }

        if (moveDirection.magnitude > 1) moveDirection.Normalize();
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnDestroy()
    {
        UpdatePatientInfo();
    }

    public async void UpdatePatientInfo()
    {
        // Find the existing BigPlayerHumanVariant GameObject
        GameObject playerHuman = GameObject.Find(BigPlayerHumanVariant.name);
        if (playerHuman != null)
        {
            // Retrieve the current x position of the BigPlayerHumanVariant
            float positionX = playerHuman.transform.position.x;

            // Create a new PatientInfo object with only the positionX field set
            PatientInfo patientInfo = new PatientInfo()
            {
                positionX = positionX
            };

            // Update the patient info
            IWebRequestReponse webRequestResponse = await patientInfoApiClient.PutPatientInfo(patientInfo);

            switch (webRequestResponse)
            {
                case WebRequestData<string> dataResponse:
                    string responseData = dataResponse.Data;
                    // TODO: Handle success scenario.
                    Debug.Log("Patient info updated successfully.");
                    break;
                case WebRequestError errorResponse:
                    string errorMessage = errorResponse.ErrorMessage;
                    Debug.Log("Update patient info error: " + errorMessage);
                    // TODO: Handle error scenario. Show the error message to the user.
                    break;
                default:
                    throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
            }
        }
        else
        {
            Debug.LogError("BigPlayerHumanVariant GameObject not found in the hierarchy.");
        }
    }

    void Flip() //methode die de speler laat omdraaien bij het bewegens
    {
        facingRight = !facingRight; // Wissel de richting om
        Vector3 newScale = transform.localScale;
        newScale.x *= -1; // Spiegelen over de X-as
        transform.localScale = newScale;
    }
}
