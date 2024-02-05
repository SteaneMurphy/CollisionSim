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
    [SerializeField][Range(0, 1)] float thicknessC;
    [SerializeField] Color colorC;

    [Header("Boundary: Box")]
    [SerializeField] bool box;
    [SerializeField][Range(0, 1)] float thicknessB;
    [SerializeField] float heightB;
    [SerializeField] float widthB;
    [SerializeField] Color colorB;

    [Header("Other Scripts")]
    [SerializeField] BoundaryRenderer boundaryRendScript;

    void Start()
    {
        
    }

    void Update()
    {
        //circle settings
        boundaryRendScript.circle = circle;
        boundaryRendScript.steps = steps;
        boundaryRendScript.radius = radius;
        boundaryRendScript.colorC = colorC;
        boundaryRendScript.thicknessC = thicknessC;

        //box settings
        boundaryRendScript.box = box;
        boundaryRendScript.thicknessB = thicknessB;
        boundaryRendScript.colorB = colorB;
        boundaryRendScript.widthB = widthB;
        boundaryRendScript.heightB = heightB;

}
}
