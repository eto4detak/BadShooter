using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazooka : Weapon
{
    private float rotSpeed = 5f;

    protected new void Awake()
    {
        sharpShooting = 3f;
        currentRechargeTime = 0f;
        rechargeTime = 3f;
        rotSpeed = 5f;
        launchSpeed = 50f;
        base.Awake();
    }


    public override void Fire()
    {
        base.Fire();
    }


    public void SetPermametFire(Collider newTarget)
    {
        owner.target = newTarget;
    }


    public override void FireToTarget()
    {
        owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, 
            Quaternion.LookRotation(owner.target.transform.position - owner.transform.position), rotSpeed * Time.deltaTime);
        if (!firing || currentRechargeTime > rechargeTime)
        {
            Fire();
        }
        else
        {
            currentRechargeTime += Time.deltaTime;
        }
    }

    public override bool CheckOnSight(Collider target)
    {
        RaycastHit hit;
        Physics.Raycast(fireTransform.position, fireTransform.forward, out hit);
        if (hit.collider != null && hit.collider.Equals(target)) return true;
        return false;
    }
}
