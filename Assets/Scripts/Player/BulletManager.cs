using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour {

    BlasterManager blasterManager;

	void Start ()
    {
         blasterManager = GameObject.Find("BulletSpawnLocation").GetComponent<BlasterManager>();
    }

	void Update ()
    {

        GetComponent<Rigidbody>().velocity = transform.forward * blasterManager.speed;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.Damage(blasterManager.attack);
        }

        Destroy (gameObject);
    }

    private void OnDestroy()
    {
        blasterManager.shotCount--;
    }
}
