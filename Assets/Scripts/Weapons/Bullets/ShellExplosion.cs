using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask tankMask;                        // Used to filter what the explosion affects, this should be set to "Players".
    public ParticleSystem ExplosionParticles;         // Reference to the particles that will play on explosion.
    public AudioSource ExplosionAudio;                // Reference to the audio that will play on explosion.
    private float maxDamage = 10;                    // The amount of damage done if the explosion is centred on a tank.
    public float ExplosionForce = 1000f;              // The amount of force added to a tank at the centre of the explosion.
    public float MaxLifeTime = 10f;                    // The time in seconds before the shell is removed.
    public float ExplosionRadius = 5f;                // The maximum distance away from the explosion tanks can be and are still affected.


    private void Start ()
    {
        SomeDestroy();
    }

    public virtual void SomeDestroy()
    {
        Destroy(gameObject, MaxLifeTime);
    }


    private void OnTriggerEnter (Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere (transform.position, ExplosionRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody> ();

            if (!targetRigidbody)
                continue;
           // targetRigidbody.AddExplosionForce (ExplosionForce, transform.position, ExplosionRadius);
            CharacterHealth targetHealth = targetRigidbody.GetComponent<CharacterHealth> ();

            if (!targetHealth)
                continue;
            float damage = CalculateDamage (targetRigidbody.position);
            targetHealth.TakeDamage (damage);
        }

       // ExplosionParticles.transform.parent = null;
       // ExplosionParticles.Play();
       // ExplosionAudio.Play();
       // ParticleSystem.MainModule mainModule = ExplosionParticles.main;
       // Destroy (ExplosionParticles.gameObject, mainModule.duration);
        Destroy (gameObject);
    }


    private float CalculateDamage (Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;
        float explosionDistance = explosionToTarget.magnitude;
        float relativeDistance = (ExplosionRadius - explosionDistance) / ExplosionRadius;
        float damage = relativeDistance * maxDamage;
        damage = Mathf.Max (0f, damage);

        return damage;
    }
}
