using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRenderer : MonoBehaviour
{
    [SerializeField] int steps;
    [SerializeField] float radius;
    [SerializeField] [Range(0, 1)] float thickness; 
    [SerializeField] Color color;

    public LineRenderer lineR;

    private void DrawCircle(int steps, float radius) 
    {
        lineR.positionCount = steps;
        lineR.startWidth = thickness; 
        lineR.endWidth = thickness;
        lineR.startColor = color;
        lineR.endColor = color;

        for (int i = 0; i < steps; i++) 
        {
            float circumferencePos = (float)i / (steps - 1);

            float radianPos = circumferencePos * 2 * Mathf.PI;

            float xScale = Mathf.Sin(radianPos);
            float yScale = Mathf.Cos(radianPos);

            float x = xScale * radius;
            float y = yScale * radius;

            Vector2 currentPos = new Vector2(x, y);

            lineR.SetPosition(i, currentPos);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        DrawCircle(steps, radius);
    }
}
