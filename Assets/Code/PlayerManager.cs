using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public float speed = 5f;
    public static PlayerManager Singleton;

    private SpriteRenderer playerSprite;
    private bool facingRight = true; // Houdt bij of de speler naar rechts kijkt

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
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        // Beweging met WASD en pijltjestoetsen
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveDirection.x -= 1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveDirection.x += 1;

        // Verander de richting van de speler
        if (moveDirection.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveDirection.x < 0 && facingRight)
        {
            Flip();
        }

        if (moveDirection.magnitude > 1) moveDirection.Normalize();
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    void Flip()
    {
        facingRight = !facingRight; // Wissel de richting om
        Vector3 newScale = transform.localScale;
        newScale.x *= -1; // Spiegelen over de X-as
        transform.localScale = newScale;
    }
}
