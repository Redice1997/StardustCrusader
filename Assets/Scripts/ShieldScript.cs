using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public Vector3 rotation;
    public float velosity;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime * velosity);
        transform.position = player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyShot")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);            
        }
        
    }

}
