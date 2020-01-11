using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float launchSpeed = 100f;
    public float sharpShooting = 3f;

    public Rigidbody shell;
    public AudioSource ShootingAudio;
    public AudioClip ChargingClip;
    public AudioClip FireClip;
    public Sprite frontImage;

    [HideInInspector] public Transform fireTransform;
    [HideInInspector] public CharacterShooting owner;

    protected readonly Vector3 shootingOffset = new Vector3(0.4f, 1, 0);
    protected readonly float standartOffset = 10f;

    protected float aimingSpeed = 2f;
    protected float offsetMagnitude = 0;
    protected float currentRechargeTime = 0f;
    protected float rechargeTime = 0.1f;
    protected bool firing;

    protected void Awake()
    {
        offsetMagnitude = standartOffset / sharpShooting;
    }

    public virtual void Fire()
    {
        firing = true;
        currentRechargeTime = 0f;

        SightOffset();
        Rigidbody shellInstance = Instantiate(shell, fireTransform.position, fireTransform.rotation);
        shellInstance.velocity = launchSpeed * fireTransform.forward;
        shellInstance.gameObject.layer = owner.gameObject.layer;
    }


    public virtual void FireToTarget()
    {

    }


    public virtual void RotateAtTarget(Collider target)
    {
        fireTransform.rotation = Quaternion.Slerp(fireTransform.rotation,
            Quaternion.LookRotation(target.transform.position - fireTransform.position), aimingSpeed * Time.deltaTime);
    }


    public virtual void SelectWeapon()
    {
        owner.SelectWeapon(this);
    }


    public virtual bool CheckOnSight(Collider target)
    {
        return true;
    }


    public virtual void SightOffset()
    {
        fireTransform.Rotate(shootingOffset, Random.Range(-offsetMagnitude, offsetMagnitude));
    }
}
