using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Settings : MonoBehaviour
{
    [Header("Boundary: Circle")]
    [SerializeField] bool circle;
    [SerializeField] float radius;
    [SerializeField] int steps;
    [SerializeField][Range(0, 1)] float circleThickness;
    [SerializeField] Color circleColor;

    [Header("Boundary: Box")]
    [SerializeField] bool box;
    [SerializeField][Range(0, 1)] float boxThickness;
    [SerializeField] public float boxHeight;
    [SerializeField] public float boxWidth;
    [SerializeField] Color boxColor;

    [Header("Create Shape: Circle")]
    [SerializeField] bool randomiseCircleSpawn;
    [SerializeField] [Min(0)] int circleAmount;
    [SerializeField] [Min(0)] public float circleRadius;
    [SerializeField] GameObject[] circleObjects;

    [Header("Particle Variables")]
    [SerializeField] public float velocity;
    [SerializeField] public bool setColor;
    [SerializeField] public Color particleColor;

    [Header("Other Scripts")]
    [SerializeField] BoundaryRenderer boundaryRendScript;
    [SerializeField] Shape shapeScript;

    void Start()
    {
        
    }

    void Update()
    {
        //circle settings
        boundaryRendScript.circle = circle;
        boundaryRendScript.steps = steps;
        boundaryRendScript.radius = radius;
        boundaryRendScript.colorC = circleColor;
        boundaryRendScript.thicknessC = circleThickness;

        //box settings
        boundaryRendScript.box = box;
        boundaryRendScript.thicknessB = boxThickness;
        boundaryRendScript.colorB = boxColor;
        boundaryRendScript.widthB = boxWidth;
        boundaryRendScript.heightB = boxHeight;

        //shape settings
        shapeScript.desiredCircleAmount = circleAmount;
        shapeScript.randomiseCircleSpawn = randomiseCircleSpawn;

}
}
