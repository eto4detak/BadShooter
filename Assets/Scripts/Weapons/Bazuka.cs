using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazuka : Weapon
{
    private float currentRechargeTime = 0f;
    private float rechargeTime = 3f;
    private float rotSpeed = 5f;
    private bool firing;

    public float launchSpeed = 100f;

    public override void Fire()
    {
        firing = true;
        currentRechargeTime = 0f;
        Rigidbody shellInstance =
            Instantiate(shell, fireTransform.position, fireTransform.rotation) as Rigidbody;
        shellInstance.velocity = launchSpeed * fireTransform.forward;

        shellInstance.gameObject.layer = owner.gameObject.layer;
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

    public override bool IsOnSight(Collider target)
    {
        RaycastHit hit;
        Physics.Raycast(fireTransform.position, fireTransform.forward, out hit);
        if (hit.collider != null && hit.collider.Equals(target)) return true;
        return false;
    }
}
