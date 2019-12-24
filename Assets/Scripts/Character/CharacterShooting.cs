using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShooting : MonoBehaviour
{
    public Transform fireTransform;
    public List<Weapon> weapons;

    [HideInInspector] public Weapon selectWeapon;
    [HideInInspector] public GameObject target;

    private void Start ()
    {
        if (weapons.Count > 0) SelectWeapon(weapons[0]);
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].owner = this;
        }
    }

    private void Update ()
    {
        if (target != null)
        {
            selectWeapon.FireToTarget();
        }
    }

    public void SelectWeapon(Weapon weapon)
    {
        //  if (weapons.Find(x => x.Equals(weapon)) )
        //  {
        selectWeapon = weapon;
            selectWeapon.fireTransform = fireTransform;
       // }
    }

    public void SetPermametFire(GameObject newTarget)
    {
        target = newTarget;
    }

    private void Fire ()
    {
        selectWeapon.Fire();
    }
    
}
