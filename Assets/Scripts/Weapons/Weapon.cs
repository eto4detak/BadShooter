using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Rigidbody shell;
    public AudioSource ShootingAudio;
    public AudioClip ChargingClip;
    public AudioClip FireClip;
    public Sprite frontImage;

    [HideInInspector] public Transform fireTransform;
    [HideInInspector] public CharacterShooting owner;

    protected float aimingSpeed = 1f;

    public virtual void Fire()
    {
        Debug.Log("virtual fire");
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
    public virtual bool IsOnSight(Collider target)
    {
        return true;
    }
}
