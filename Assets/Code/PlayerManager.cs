using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public float speed = 5f;
    public static PlayerManager Singleton;
    public bool routeA; //Hierdoor kan je met een knop het limiet aanpassen
    private SpriteRenderer playerSprite;
    private bool facingRight = true; // Houdt bij of de speler naar rechts kijkt
    private int limit;
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
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        // Beweging met WASD en pijltjestoetsen
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && transform.position.x > -133) moveDirection.x -= 1; //Beweegt de speler naar links.
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && transform.position.x < limit)  moveDirection.x += 1; //Beweegt de speler naar rechts maar niet verder dan het limiet van de route.

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

    void Flip() //methode die de speler laat omdraaien bij het bewegens
    {
        facingRight = !facingRight; // Wissel de richting om
        Vector3 newScale = transform.localScale;
        newScale.x *= -1; // Spiegelen over de X-as
        transform.localScale = newScale;
    }
}
