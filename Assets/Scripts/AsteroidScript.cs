using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    public GameObject asteroidExplosion;
    public GameObject playerExplosion;

    public float health;
    public float speed;
    public float rotation;

    private float multiplier;
    // Start is called before the first frame update
    void Start()
    {
        multiplier = Random.Range(0.5f, 2.0f);

        Rigidbody asteroid = GetComponent<Rigidbody>();

        health *= multiplier;
        asteroid.angularVelocity = Random.insideUnitSphere * rotation / multiplier;
        
        asteroid.velocity = new Vector3(0, 0, - speed / multiplier);        
        transform.localScale *= multiplier;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "EnemyShip")
        {
            Instantiate(playerExplosion, other.transform.position, Quaternion.identity);

            GameObject newExplosion = Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
            newExplosion.transform.localScale *= multiplier;

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.tag == "EnemyShot")
        {
            if (health - ShootingScript.damage <= 0)
            {
                GameObject newExplosion = Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
                newExplosion.transform.localScale *= multiplier;

                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            else
            {
                health -= ShootingScript.damage;
                Destroy(other.gameObject);
                return;
            }
        }
        else if (other.tag == "PlayerShot")
        {
            if (health - ShootingScript.damage <= 0)
            {
                GameObject newExplosion = Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
                newExplosion.transform.localScale *= multiplier;

                Destroy(other.gameObject);
                Destroy(gameObject);

                ControllerScript.IncreaseScore(10);
            }
            else
            {
                health -= ShootingScript.damage;
                Destroy(other.gameObject);                
                return;
            }
        }
        else if (other.tag == "Shield")
        {
            GameObject newExplosion = Instantiate(asteroidExplosion, transform.position, Quaternion.identity);
            newExplosion.transform.localScale *= multiplier;

            Destroy(other.gameObject);
            Destroy(gameObject);
            ControllerScript.IncreaseScore(10);
        }
        
    }
}
