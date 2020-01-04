using UnityEngine;

public class ShellBullet : MonoBehaviour
{
    public float lifeTime = 10f;
    private float maxDamage = 1;

    private void Start ()
    {
        SomeDestroy();
    }

    public virtual void SomeDestroy()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        CharacterHealth targetHealth = other.collider.GetComponent<CharacterHealth> ();
        if (targetHealth) targetHealth.TakeDamage(maxDamage);
        Destroy (gameObject);
    }

    private float CalculateDamage ()
    {
        return maxDamage;
    }

}
