using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    

    public GameObject lazerShot;

    public Transform middleGun;
    public Transform leftGun;
    public Transform rightGun;

    public float shotDelay;

    public static float damage = 80;

    private float nextShotTimeMiddle, nextShotTimeSide;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!ControllerScript.isStarted)
        {
            return;
        }
        StandartFire();
    }

    private void SemiAuto()
    {
        if (Time.time > nextShotTimeMiddle && Input.GetButton("Fire1"))
        {
            StartCoroutine(ExampleShot());
            nextShotTimeMiddle = Time.time + shotDelay * 2;
        }
    }

    private void StandartFire()
    {
        if (Time.time > nextShotTimeMiddle && Input.GetButton("Fire1"))
        {

            Instantiate(lazerShot, middleGun.position, Quaternion.identity);

            nextShotTimeMiddle = Time.time + shotDelay;
        }
    }

    private void UltimateFire()
    {

        if (Input.GetButton("Fire1"))
        {
            if (Input.GetButton("Fire2"))
            {
                if (Time.time > nextShotTimeMiddle)
                {
                    Instantiate(lazerShot, middleGun.position, Quaternion.identity);

                    nextShotTimeMiddle = Time.time + shotDelay;
                }
                if (Time.time > nextShotTimeSide)
                {

                    GameObject newLazerShot = Instantiate(lazerShot, leftGun.position, Quaternion.Euler(0, -45, 0));
                    newLazerShot.transform.localScale /= 4;

                    Instantiate(newLazerShot, rightGun.position, Quaternion.Euler(0, 45, 0));

                    nextShotTimeSide = Time.time + shotDelay / 2;
                }
            }
            else
            {
                if (Time.time > nextShotTimeMiddle)
                {
                    Instantiate(lazerShot, middleGun.position, Quaternion.identity);

                    nextShotTimeMiddle = Time.time + shotDelay;
                }
                if (Time.time > nextShotTimeSide)
                {

                    GameObject newLazerShot = Instantiate(lazerShot, leftGun.position, Quaternion.identity);
                    newLazerShot.transform.localScale /= 4;

                    Instantiate(newLazerShot, rightGun.position, Quaternion.identity);

                    nextShotTimeSide = Time.time + shotDelay / 2;
                }
            }
        }
    }

    IEnumerator ExampleShot()
    {
        Instantiate(lazerShot, middleGun.position, Quaternion.identity);
        yield return new WaitForSeconds(shotDelay / 3);
        Instantiate(lazerShot, middleGun.position, Quaternion.identity);
        yield return new WaitForSeconds(shotDelay / 3);
        Instantiate(lazerShot, middleGun.position, Quaternion.identity);
    }
}
