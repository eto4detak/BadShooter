using UnityEngine;

public class ShellBullet : MonoBehaviour
{
    public ParticleSystem ExplosionParticles;
    public AudioSource ExplosionAudio;
    private float maxDamage = 1;
    public float ExplosionForce = 1000f;
    public float MaxLifeTime = 30f;

    private void Start ()
    {
        SomeDestroy();
    }

    public virtual void SomeDestroy()
    {
        Destroy(gameObject, MaxLifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        CharacterHealth targetHealth = other.collider.GetComponent<CharacterHealth> ();
        if (!targetHealth) return;

        targetHealth.TakeDamage (maxDamage);
        Destroy (gameObject);
    }

    private float CalculateDamage ()
    {
        return maxDamage;
    }

}
