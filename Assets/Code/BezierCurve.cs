using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform point0, point1, point2, point3;
    public int resolution = 50;

    void Start()
    {
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = resolution;
        DrawCurve();
    }

    void DrawCurve()
    {
        for (int i = 0; i < resolution; i++)
        {
            float t = i / (float)(resolution - 1);
            Vector3 point = Mathf.Pow(1 - t, 3) * point0.position +
                            3 * Mathf.Pow(1 - t, 2) * t * point1.position +
                            3 * (1 - t) * Mathf.Pow(t, 2) * point2.position +
                            Mathf.Pow(t, 3) * point3.position;
            lineRenderer.SetPosition(i, point);
        }
    }
}
