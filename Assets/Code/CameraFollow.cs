using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // De speler transform
    public float smoothing = 5f;  // Hoe snel de camera de speler volgt

    private Vector3 offset;  // Het offset van de camera ten opzichte van de speler

    void Start()
    {
        offset = transform.position - player.position;  // Berekent het oorspronkelijke verschil
    }

    void LateUpdate()
    {
        Vector3 targetPosition = player.position + offset;  // Doelpositie van de camera
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);  // Beweeg de camera naar de doelpositie
    }
}
