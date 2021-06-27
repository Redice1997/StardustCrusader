using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenAsteroidScript : MonoBehaviour
{
    public GameObject asteroidExplosion;
    public GameObject playerExplosion;

    
    public float speed;
    public float rotation;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody asteroid = GetComponent<Rigidbody>();
        asteroid.angularVelocity = Random.insideUnitSphere * rotation;
        asteroid.velocity = new Vector3(0, 0, -speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -62)
        {
            ControllerScript.IncreaseScore(-50);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "EnemyShip")
        {
            Instantiate(playerExplosion, other.transform.position, Quaternion.identity);
            Instantiate(asteroidExplosion, transform.position, Quaternion.identity);            

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.tag == "PlayerShot" || other.tag == "Shield")
        {
            Instantiate(asteroidExplosion, transform.position, Quaternion.identity);

            Destroy(other.gameObject);
            Destroy(gameObject);

            ControllerScript.IncreaseScore(100);
        }
    }
}
