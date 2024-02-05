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
    Vector2 direction;
    bool gravity;
    Color color;
    Color defaultColor;
    float radius;

    //settings script
    Settings settingsScript;

    private void UpdateParticleVariables()
    {
        //adjust uniform scale of particle from value in inspector
        //radius = diameter (uniform scale) / 2
        circleSize = settingsScript.circleRadius;
        radius = circleSize / 2;
        transform.localScale = new Vector2(circleSize, circleSize);

        //update velocity and direction values from settings
        velocity = settingsScript.velocity;

        if (settingsScript.setColor)
        {
            color = settingsScript.particleColor;
            GetComponent<SpriteRenderer>().color = color;
        }
        else 
        {
            GetComponent<SpriteRenderer>().color = defaultColor;
        }
    }

    private void UpdateParticlePosition() 
    {
        transform.position += new Vector3(direction.x, direction.y, 0f).normalized * Time.deltaTime * velocity;
    }

    private void CheckBoundaryCollisions()
    {
        if (transform.position.x + radius >= (settingsScript.boxWidth / 2) && transform.position.y + radius >= (settingsScript.boxHeight / 2)) 
        {
            transform.position = new Vector3((settingsScript.boxWidth / 2) - radius, (settingsScript.boxHeight / 2) - radius, 0f);
            direction = direction * -1f;
        }
        if (transform.position.x - radius <= (-settingsScript.boxWidth / 2) && transform.position.y + radius >= (settingsScript.boxHeight / 2))
        {
            transform.position = new Vector3((-settingsScript.boxWidth / 2) + radius, (settingsScript.boxHeight / 2) - radius, 0f);
            direction = direction * -1f;
        }
        if (transform.position.x - radius <= (-settingsScript.boxWidth / 2) && transform.position.y - radius <= (-settingsScript.boxHeight / 2))
        {
            transform.position = new Vector3((-settingsScript.boxWidth / 2) + radius, (-settingsScript.boxHeight / 2) + radius, 0f);
            direction = direction * -1f;
        }
        if (transform.position.x + radius >= (settingsScript.boxWidth / 2) && transform.position.y - radius <= (-settingsScript.boxHeight / 2))
        {
            transform.position = new Vector3((settingsScript.boxWidth / 2) - radius, (-settingsScript.boxHeight / 2) + radius, 0f);
            direction = direction * -1f;
        }

        if (transform.position.x + radius >= (settingsScript.boxWidth / 2))
        {
            transform.position = new Vector3((settingsScript.boxWidth / 2) - radius, transform.position.y, 0f);
            direction = direction * -1f;
        }
        if (transform.position.x - radius <= (-settingsScript.boxWidth / 2))
        {
            transform.position = new Vector3((-settingsScript.boxWidth / 2) + radius, transform.position.y, 0f);
            direction = direction * -1f;
        }
        if (transform.position.y + radius >= (settingsScript.boxHeight / 2))
        {
            transform.position = new Vector3(transform.position.x, (settingsScript.boxHeight / 2) - radius, 0f);
            direction = direction * -1f;
        }
        if (transform.position.y - radius <= (-settingsScript.boxHeight / 2))
        {
            transform.position = new Vector3(transform.position.x, (-settingsScript.boxHeight / 2) + radius, 0f);
            direction = direction * -1f;
        }
    }

    void Start()
    {
        settingsScript = GameObject.Find("Settings").GetComponent<Settings>();
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        defaultColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

    }

    void Update()
    {
        UpdateParticleVariables();
        UpdateParticlePosition();
        CheckBoundaryCollisions();
    }
}
