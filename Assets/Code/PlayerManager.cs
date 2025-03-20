using UnityEngine;
using UnityEngine.SceneManagement;
//test commit
public class PlayerManager : MonoBehaviour
{
    public float speed = 5f;
    public static PlayerManager Singleton;

    private SpriteRenderer playerSprite;

    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        // Beweging van de speler
        if (Input.GetKey(KeyCode.A)) moveDirection.x -= 1;
        if (Input.GetKey(KeyCode.D)) moveDirection.x += 1;

        if (moveDirection.magnitude > 1) moveDirection.Normalize();
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}
