using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shape : MonoBehaviour
{
    //circle variables
    private int currentCircleAmount;
    public int desiredCircleAmount;
    public bool randomiseCircleSpawn;
    [SerializeField] GameObject circlePrefab;
    public List<GameObject> circleObjects;
    private void CreateCircle() 
    {
        int changeAmount = desiredCircleAmount - currentCircleAmount;

        if(changeAmount > 0) 
        {

            for (int i = 0; i < changeAmount; i++)
            {
                if (randomiseCircleSpawn)
                {
                    GameObject newObj = Instantiate(circlePrefab, new Vector3(Random.Range(-50f, 50f), Random.Range(-50f, 50f), 0f), Quaternion.identity);
                    newObj.transform.SetParent(transform, false);
                    circleObjects.Add(newObj);
                    currentCircleAmount++;
                }
                else
                {
                    GameObject newObj = Instantiate(circlePrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                    newObj.transform.SetParent(transform, false);
                    circleObjects.Add(newObj);
                    currentCircleAmount++;
                }
            }
        }

        if(changeAmount < 0) 
        {
            for (int i = 0; i > changeAmount; i--) 
            {
                Destroy(circleObjects.Last());
                circleObjects.Remove(circleObjects.Last());
                currentCircleAmount--;
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        CreateCircle();
    }
}
