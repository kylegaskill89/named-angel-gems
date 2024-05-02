using UnityEngine;
using System.Collections;

public class BlasterManager : MonoBehaviour {

    public GameObject bullet;
    GameObject bulletClone;
    public AudioSource shootAudioSource;
    public AudioSource collisionAudioSource;

    [Space(10)]
    public bool canFire = true;
    public bool isShooting = false;

    [Space(10)]
    public float speed;
    public float rotateSpeed;

    public float fireRate;

    [Header("Buster Stats")]
    public int attack;
    public float energy;
    public float range;
    public float rapid;

    [HideInInspector]
    public int shotCount;
    
    float timestamp;


    void Update () {

        //Handle shooting

        if (GameManager.Instance.state == GameManager.GameState.Normal)
        {
            if (shotCount == energy)
            {
                isShooting = false;
                canFire = false;
            }
            if (shotCount < energy)
            {
                canFire = true;
            }


            if (Input.GetButton("Shoot") && canFire && Time.time >= timestamp)
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        if (shotCount < energy)
        {            
            isShooting = true;
            shotCount++;
            shootAudioSource.Play();
            bulletClone = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            Destroy(bulletClone, range / 10);          
            timestamp = Time.time + rapid;
        }

    }
}
