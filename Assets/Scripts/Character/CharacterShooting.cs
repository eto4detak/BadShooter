using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShooting : MonoBehaviour
{
    public Transform fireTransform;
    public List<Weapon> weapons;

    [HideInInspector] public Weapon selectWeapon;
    [HideInInspector] public Collider target;

    private bool isOnSight = false;
    private float rotSpeed = 5f;

    public bool IsOnSight { get => isOnSight; private set => isOnSight = value; }

    private void Awake()
    {
        LoadStartupWeapons();
    }

    private void Update()
    {
        TryShoot();
    }


    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
        SetupWeapons(weapon);
        if (weapons.Count == 1) SelectWeapon(weapons[0]);
        else weapon.gameObject.SetActive(false);
    }


    public void RemoveWeapon(Weapon weapon)
    {
        weapons.Remove(weapon);
        if(selectWeapon == weapon) SelectWeapon(weapons[0]);
    }


    public void SelectWeapon(Weapon weapon)
    {
        HideViewWeapons();
        selectWeapon = weapon;
        selectWeapon.fireTransform = fireTransform;
        selectWeapon.gameObject.SetActive(true);
    }


    public void SetPermametFire(Collider newTarget)
    {
        target = newTarget;
    }

    private void SetupWeapons(Weapon weapon)
    {
        weapon.transform.position = fireTransform.position;
        weapon.transform.rotation = fireTransform.rotation;
        weapon.transform.parent = transform;
        weapon.owner = this;
    }


    private void LoadStartupWeapons()
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


    private void HideViewWeapons()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].gameObject.SetActive(false);
        }
    }

    private void TryShoot()
    {
        if (target != null)
        {
            LookAtTarget();
            if (IsOnSight = selectWeapon.CheckOnSight(target))
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
