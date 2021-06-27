using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterScript : MonoBehaviour
{

    public GameObject asteroid1;
    public GameObject asteroid2;
    public GameObject asteroid3;
    public GameObject goldenAsteroid;
    public GameObject enemy1;
    

    public static float minDelay;
    
    float nextLaunchTime;
    

    // Update is called once per frame
    void Update()
    {
        if (!ControllerScript.isStarted)
        {
            return;
        }
        if (Time.time > nextLaunchTime)
        {
            float xSize = transform.localScale.x / 2;
            Vector3 asteroidPosition = new Vector3(
                Random.Range(-xSize, xSize),
                0,
                transform.position.z
            );
            float maxDelay = minDelay * 2;
            nextLaunchTime = Time.time + Random.Range(minDelay, maxDelay);

            int number = Random.Range(1, 9);
            switch (number)
            {
                case 1:
                    Instantiate(asteroid1, asteroidPosition, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(asteroid2, asteroidPosition, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(asteroid3, asteroidPosition, Quaternion.identity);
                    break;
                case 4:
                    Instantiate(enemy1, asteroidPosition, Quaternion.Euler(0, 180, 0));
                    break;
                case 5:
                    Instantiate(asteroid1, asteroidPosition, Quaternion.identity);
                    break;
                case 6:
                    Instantiate(asteroid2, asteroidPosition, Quaternion.identity);
                    break;
                case 7:
                    Instantiate(asteroid3, asteroidPosition, Quaternion.identity);
                    break;
                case 8:
                    Instantiate(goldenAsteroid, asteroidPosition, Quaternion.identity);
                    break;
            }

        }
    }
}
