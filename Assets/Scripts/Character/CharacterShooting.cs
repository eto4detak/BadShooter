using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShooting : MonoBehaviour
{
    public Transform fireTransform;
    public List<Weapon> weapons;

    [HideInInspector] public Weapon selectWeapon;
    [HideInInspector] public Collider target;

    private float rotSpeed = 5f;

    private void Awake()
    {
        SetupWeapons();
    }

    private void Start ()
    {

    }

    private void Update()
    {
        TryShoot();
    }

    public void SelectWeapon(Weapon weapon)
    {
        selectWeapon = weapon;
        selectWeapon.fireTransform = fireTransform;
    }

    public void SetPermametFire(Collider newTarget)
    {
        target = newTarget;
    }

    private void SetupWeapons()
    {
        List<Weapon> newWeapons = new List<Weapon>();
        for (int i = 0; i < weapons.Count; i++)
        {
            newWeapons.Add(Instantiate(weapons[i], fireTransform.position, fireTransform.rotation, transform));
            newWeapons[i].owner = this;
        }
        weapons = newWeapons;
        if (weapons.Count > 0) SelectWeapon(weapons[0]);
    }

    private void TryShoot()
    {
        if (target != null)
        {
            LookAtTarget();
            if (selectWeapon.IsOnSight(target))
            {
                selectWeapon.FireToTarget();
            }
            else
            {
                selectWeapon.RotateAtTarget(target);
            }
        }
    }

    private void LookAtTarget()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(target.transform.position - transform.position), rotSpeed * Time.deltaTime);
    }

    private void Fire ()
    {
        selectWeapon.Fire();
    }
    
}
