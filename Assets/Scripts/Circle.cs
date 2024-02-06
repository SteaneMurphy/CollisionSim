using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class Circle : MonoBehaviour
{
    //variables
    float circleSize;
    float velocity;
    float acceleration;
    public Vector2 direction;
    bool gravity;
    float gravityAmount;
    Color color;
    Color defaultColor;
    float radius;
    bool boundaryCircle;
    bool boundaryBox;
    public int positionInList;

    Vector2 stuckCheck;

    //settings script
    Settings settingsScript;
    Shape shapeScript;

    private void UpdateParticleVariables()
    {
        //adjust uniform scale of particle from value in inspector
        //radius = diameter (uniform scale) / 2
        circleSize = settingsScript.circleRadius;
        radius = circleSize / 2;
        transform.localScale = new Vector2(circleSize, circleSize);

        //update velocity and direction values from settings
        velocity = settingsScript.velocity;

        //check boundary type
        boundaryCircle = settingsScript.circle;
        boundaryBox = settingsScript.box;

        if (settingsScript.setColor)
        {
            color = settingsScript.particleColor;
            GetComponent<SpriteRenderer>().color = color;
        }
        else 
        {
            GetComponent<SpriteRenderer>().color = defaultColor;
        }

        if (settingsScript.gravity)
        {
            gravity = true;
            gravityAmount = settingsScript.gravityAmount;
        }
        else 
        {
            gravity = false;
        }
    }

    private void UpdateParticlePosition() 
    {
        if (gravity)
        {
            transform.position += new Vector3(direction.x, -gravityAmount, 0f).normalized * Time.deltaTime * velocity;
        }
        else 
        {
            transform.position += new Vector3(direction.x, direction.y, 0f).normalized * Time.deltaTime * velocity;
        }
    }

    private void CheckBoundaryCollisions()
    {
        if (boundaryBox) 
        {
            //corner collisions
            //top right
            if (transform.position.x + radius >= (settingsScript.boxWidth / 2) && transform.position.y + radius >= (settingsScript.boxHeight / 2))
            {
                transform.position = new Vector3((settingsScript.boxWidth / 2) - radius, (settingsScript.boxHeight / 2) - radius, 0f);
                //direction = new Vector2(-1f, -1f);
                direction = direction * -1f;
            }
            //top left
            if (transform.position.x - radius <= (-settingsScript.boxWidth / 2) && transform.position.y + radius >= (settingsScript.boxHeight / 2))
            {
                transform.position = new Vector3((-settingsScript.boxWidth / 2) + radius, (settingsScript.boxHeight / 2) - radius, 0f);
                //direction = new Vector2(1f, -1f);
                direction = direction * -1f;
            }
            //bottom left
            if (transform.position.x - radius <= (-settingsScript.boxWidth / 2) && transform.position.y - radius <= (-settingsScript.boxHeight / 2))
            {
                transform.position = new Vector3((-settingsScript.boxWidth / 2) + radius, (-settingsScript.boxHeight / 2) + radius, 0f);
                //direction = new Vector2(1f, 1f);
                direction = direction * -1f;
            }
            //bottom right
            if (transform.position.x + radius >= (settingsScript.boxWidth / 2) && transform.position.y - radius <= (-settingsScript.boxHeight / 2))
            {
                transform.position = new Vector3((settingsScript.boxWidth / 2) - radius, (-settingsScript.boxHeight / 2) + radius, 0f);
                //direction = new Vector2(-1f, 1f);
                direction = direction * -1f;
            }

            //sides collisions
            if (transform.position.x + radius >= (settingsScript.boxWidth / 2))
            {
                transform.position = new Vector3((settingsScript.boxWidth / 2) - radius, transform.position.y, 0f);
                direction = new Vector2(direction.x * -1f, direction.y);
            }
            if (transform.position.x - radius <= (-settingsScript.boxWidth / 2))
            {
                transform.position = new Vector3((-settingsScript.boxWidth / 2) + radius, transform.position.y, 0f);
                direction = new Vector2(direction.x * -1f, direction.y);
            }
            if (transform.position.y + radius >= (settingsScript.boxHeight / 2))
            {
                transform.position = new Vector3(transform.position.x, (settingsScript.boxHeight / 2) - radius, 0f);
                direction = new Vector2(direction.x, direction.y * -1f);
            }
            if (transform.position.y - radius <= (-settingsScript.boxHeight / 2))
            {
                transform.position = new Vector3(transform.position.x, (-settingsScript.boxHeight / 2) + radius, 0f);
                direction = new Vector2(direction.x, direction.y * -1f);
            }
        }

        if (boundaryCircle) 
        {
            if (transform.position.magnitude + radius >= settingsScript.diameter) 
            {
                direction = direction * -1f;
            }
            if (transform.position.magnitude - radius <= -settingsScript.diameter)
            {
                direction = direction * -1f;
            }
        }
    }

    private void CheckOtherCollisions() 
    {
        for (int i = 0; i < shapeScript.circleObjects.Count; i++) 
        {
            Vector2 newVec = (shapeScript.circleObjects[i].transform.position) - transform.position;
            float mag = newVec.magnitude;
            
            if (mag <= radius && i != positionInList) 
            {
                direction = direction * -1f;
                shapeScript.circleObjects[i].GetComponent<Circle>().direction = direction * -1f;
            }
        }
    }

    private void CheckStuck() 
    {
        if (transform.position.x == stuckCheck.x && transform.position.y == stuckCheck.y) 
        {
            shapeScript.circleObjects.Remove(shapeScript.circleObjects[positionInList]);
            shapeScript.currentCircleAmount--;
            Destroy(gameObject);
            print("Destroyed Circle: Stuck");
        }
        stuckCheck = transform.position; 
    }

    void Start()
    {
        settingsScript = GameObject.Find("Settings").GetComponent<Settings>();
        shapeScript = GameObject.Find("Shapes").GetComponent<Shape>();
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        defaultColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        stuckCheck = transform.position;
    }

    void Update()
    {
        UpdateParticleVariables();
        UpdateParticlePosition();
        CheckBoundaryCollisions();
        CheckOtherCollisions();
        CheckStuck();
    }
}
