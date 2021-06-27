using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed;
    public float shotDelay;
    public float waitBeforeShoot;

    public GameObject lazerShot;
    public Transform weaponLeft;
    public Transform weaponRight;
    public GameObject EnemyExplosion;
    public GameObject Shield;

    float nextShotTime;
    
   
    
    // Start is called before the first frame update
    void Start()
    {
        waitBeforeShoot = Random.Range(0, waitBeforeShoot);
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        nextShotTime = Time.time + waitBeforeShoot;
    }

    // Update is called once per frame
    void Update()
    {

        Shoot();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerShot")
        {
            try
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                GameObject OldShield = GameObject.FindGameObjectWithTag("Shield");
                if (OldShield == null)
                    Instantiate(Shield, player.transform.position, Quaternion.identity);
                
            }
            catch
            {
                return;
            }             
            Instantiate(EnemyExplosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
            ControllerScript.IncreaseScore(50);
        }
        else if (other.tag == "Shield")
        {
            Instantiate(EnemyExplosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
            ControllerScript.IncreaseScore(50);
        }
        else if (other.tag == "Player")
        {
            Instantiate(EnemyExplosion, transform.position, Quaternion.identity);
            Instantiate(EnemyExplosion, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        
    }
    private void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            Quaternion quaternion = transform.rotation;
            Instantiate(lazerShot, weaponLeft.position, quaternion);
            Instantiate(lazerShot, weaponRight.position, quaternion);
            
            nextShotTime = Time.time + shotDelay;
        }
    }
    private void Escape(GameObject asteroid)
    {
        if (Vector3.Distance(this.transform.position, asteroid.transform.position) < 15f)
        {
            Shoot();
        }
    }

    IEnumerator Delay(float seconds)
    {        
        yield return new WaitForSeconds(seconds);     
    }
    
}
