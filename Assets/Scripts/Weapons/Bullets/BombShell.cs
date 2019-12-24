using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombShell : MonoBehaviour
{
    public ParticleSystem ExplosionParticles;         // Reference to the particles that will play on explosion.
    public AudioSource ExplosionAudio;                // Reference to the audio that will play on explosion.
    private float maxDamage = 10;                    // The amount of damage done if the explosion is centred on a tank.
    public float ExplosionForce = 1000f;              // The amount of force added to a tank at the centre of the explosion.
    public float maxLifeTime = 3f;                    // The time in seconds before the shell is removed.
    public float explosionRadius = 5f;                // The maximum distance away from the explosion tanks can be and are still affected.


    private void Start()
    {
        SomeDestroy();
    }

    public void SomeDestroy()
    {
        Invoke("Explosion", maxLifeTime);
    }


    private void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            if (!targetRigidbody)
                continue;
            CharacterHealth targetHealth = targetRigidbody.GetComponent<CharacterHealth>();

            if (!targetHealth)
                continue;
            float damage = CalculateDamage(targetRigidbody.position);
            targetHealth.TakeDamage(damage);
        }
        Destroy(gameObject);
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;
        float explosionDistance = explosionToTarget.magnitude;
        float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;
        float damage = relativeDistance * maxDamage;
        damage = Mathf.Max(0f, damage);

        return damage;
    }
}
