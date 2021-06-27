using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualControllScript : MonoBehaviour
{
    public GameObject playerExplosion;
    
    public float velosity;
    
    public float tilt;
    public float xMax, xMin, zMax, zMin;

    
    
    Rigidbody ship;
    
    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        

        float xClamped = Mathf.Clamp(ship.position.x, xMin, xMax);
        float zClamped = Mathf.Clamp(ship.position.z, zMin, zMax);
        ship.position = new Vector3(xClamped, 0, zClamped);

        Vector3 speed = new Vector3(moveHorizontal, 0, moveVertical * 0.7F) * velosity;


        if (Input.GetButton("Fire2"))
        {
            ship.velocity = speed * 0;
        }        
        else if (Input.GetKey("left shift"))
        {
            ship.velocity = speed * 2;
        }
        else
        {
            ship.velocity = speed;
        }
        //ship.rotation = Quaternion.Euler(moveVertical * 0.7F * tilt * 0.3F * velosity, 0, -moveHorizontal * tilt * velosity);
        //ship.rotation = Quaternion.Euler(ship.velocity.z * tilt * 0.3F, 0, -ship.velocity.x * tilt);
        ship.rotation = Quaternion.Euler(speed.z * tilt * 0.3F, 0, -speed.x * tilt);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyShot")
        {
            Instantiate(playerExplosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        
        
    }


}
