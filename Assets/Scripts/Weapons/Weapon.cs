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

    public virtual void Fire()
    {
        Debug.Log("virtual fire");
    }

    public virtual void FireToTarget()
    {

    }

    public virtual void SelectWeapon()
    {
        owner.SelectWeapon(this);
    }

}
