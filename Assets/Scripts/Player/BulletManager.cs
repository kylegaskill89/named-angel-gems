using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour {

    BlasterManager blasterManager;
    AudioSource audioSource;
    [SerializeField] AudioClip damageSound;
    [SerializeField] AudioClip collisionSound;

    void Start ()
    {
        blasterManager = GameObject.Find("BulletSpawnLocation").GetComponent<BlasterManager>();
        audioSource = GetComponent<AudioSource>();
    }

	void Update ()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * blasterManager.speed;

        transform.Rotate(0, 0, -blasterManager.rotateSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.Damage(blasterManager.attack);
            blasterManager.collisionAudioSource.clip = damageSound;
            blasterManager.collisionAudioSource.Play();
        }
        else
        {
            blasterManager.collisionAudioSource.clip = collisionSound;
            blasterManager.collisionAudioSource.Play();
        }

        Destroy (gameObject);
    }

    private void OnDestroy()
    {
        blasterManager.shotCount--;
    }
}
