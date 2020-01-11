using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47 : Weapon
{
    protected new void Awake()
    {
        sharpShooting = 6f;
        offsetMagnitude = standartOffset / sharpShooting;
    }

    private void Update()
    {
        if(fireTransform)
        transform.rotation = fireTransform.rotation;
    }


    public override void Fire()
    {
        base.Fire();
    }

    public override void FireToTarget()
    {
        if (!firing && currentRechargeTime > rechargeTime)
        {
            Fire();
        }
        else
        {
            currentRechargeTime += Time.deltaTime;
            firing = false;
        }
    }


    public void SetPermametFire(Collider newTarget)
    {
        owner.target = newTarget;
    }
    

    public override bool CheckOnSight(Collider target)
    {
        RaycastHit hit;
        Physics.Raycast(fireTransform.position, fireTransform.forward, out hit);
        if (hit.collider != null && hit.collider.Equals(target) ) return true;
        return false;
    }


}