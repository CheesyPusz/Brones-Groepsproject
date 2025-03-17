using UnityEngine;

public class SquigglyPath : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int points = 50;
    public float amplitude = 1f;
    public float frequency = 2f;
    public float length = 10f;

    void Start()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = points;
        GenerateSquigglyPath();
    }

    void GenerateSquigglyPath()
    {
        Vector3[] positions = new Vector3[points];

        for (int i = 0; i < points; i++)
        {
            float x = (i / (float)(points - 1)) * length;
            float y = Mathf.Sin(x * frequency) * amplitude;
            positions[i] = new Vector3(x, y, 0);
        }

        lineRenderer.SetPositions(positions);
    }
}
