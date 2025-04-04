using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using static UnityEditor.UIElements.ToolbarMenu;
using static UnityEngine.GraphicsBuffer;

public class PlayerManager : MonoBehaviour
{
    public PatientInfoApiClient patientInfoApiClient;
    public float speed = 5f;
    public static PlayerManager Singleton;
    public bool routeA; //Hierdoor kan je met een knop het limiet aanpassen
    //private SpriteRenderer playerSprite;
    private bool facingRight = true; // Houdt bij of de speler naar rechts kijkt
    private int limit;
    public GameObject BigPlayerHumanVariant; // Reference to the existing GameObject

    private float PositionX;

    PatientInfo patientInfo = new PatientInfo();

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
    /*
    private void Start()
    {
        
        if (!routeA)
        {
            limit = 3366; //limiet voor route B
        }
        else
        {
            limit = 2230; //limiet voor route A
        }
    //playerSprite = GetComponent<SpriteRenderer>();
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
    }*/
    public void Start()
    {
        //deze methode moet die andere in de start tussen /**/ zijn onnodig lijkt me
        if (!routeA)
        {
            limit = 3366; //limiet voor route B
        }
        else
        {
            limit = 2230; //limiet voor route A
        }


        Vector3 newPosition = BigPlayerHumanVariant.transform.position;

        //newPosition.x = PlayerPrefs.GetFloat("PostionX");

        newPosition.x = 0;

        BigPlayerHumanVariant.transform.position = newPosition;

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

        patientInfo.positionX = Mathf.Round(BigPlayerHumanVariant.transform.position.x);
        PositionX = patientInfo.positionX;
        UpdatePatientInfo(PositionX);
    }

    public async void UpdatePatientInfo(float PositionX)
    {
        Debug.Log("UpdatePatientInfo called");
        Debug.Log(PositionX);



        // Update the patient info
        if (patientInfoApiClient != null)
        {
            IWebRequestReponse webRequestResponse = await patientInfoApiClient.PutPatientInfo(PositionX);

            switch (webRequestResponse)
            {
                case WebRequestData<string> dataResponse:
                    string responseData = dataResponse.Data;
                    Debug.Log("Player updated: " + responseData);

                    PlayerPrefs.SetFloat("PositionX", patientInfo.positionX);
                    //string responseData = dataResponse.Data;
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
    }

    void Flip() //methode die de speler laat omdraaien bij het bewegens
    {
        facingRight = !facingRight; // Wissel de richting om
        Vector3 newScale = transform.localScale;
        newScale.x *= -1; // Spiegelen over de X-as
        transform.localScale = newScale;
    }
}
