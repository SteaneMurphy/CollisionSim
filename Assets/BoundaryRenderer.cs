using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryRenderer : MonoBehaviour
{
    //shape selection
    public bool circle;
    public bool box;

    //circle variables
    public int steps;
    public float radius;
    public float thicknessC;
    public Color colorC;

    //box variables
    public float thicknessB;
    public Color colorB;
    public float widthB;
    public float heightB;

    [SerializeField] LineRenderer lineR;

    private void DrawBoundary(int steps, float radius, bool circle, bool box) 
    {
        if (circle)
        {
            lineR.positionCount = steps;
            lineR.startWidth = thicknessC;
            lineR.endWidth = thicknessC;
            lineR.startColor = colorC;
            lineR.endColor = colorC;

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

        if (box) 
        {
            lineR.positionCount = 5;
            lineR.startWidth = thicknessB;
            lineR.endWidth = thicknessB;
            lineR.startColor = colorB;
            lineR.endColor = colorB;

            //leftside
            lineR.SetPosition(0, new Vector2((-widthB / 2), (-heightB / 2)));
            lineR.SetPosition(1, new Vector2((-widthB / 2), (heightB / 2)));
            //top
            lineR.SetPosition(1, new Vector2((-widthB / 2), (heightB / 2)));
            lineR.SetPosition(2, new Vector2((widthB / 2), (heightB / 2)));
            //rightside
            lineR.SetPosition(2, new Vector2((widthB / 2), (heightB / 2)));
            lineR.SetPosition(3, new Vector2((widthB / 2), (-heightB / 2)));
            //bottom
            lineR.SetPosition(3, new Vector2((widthB / 2), (-heightB / 2)));
            lineR.SetPosition(0, new Vector2((-widthB / 2), (-heightB / 2)));
            //return to origin
            lineR.SetPosition(4, new Vector2((-widthB / 2), (-heightB / 2)));
        }
    }
    private void DrawCircle()
    {
    }

    void Start()
    {

    }

    void Update()
    {
        DrawBoundary(steps, radius, circle, box);
    }
}
