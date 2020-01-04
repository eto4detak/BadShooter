using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47 : Weapon
{
    public float launchSpeed = 100f;
    private float currentRechargeTime = 0f;
    private float rechargeTime = 0.1f;
    private bool firing;

    public override void Fire()
    {
        firing = true;
        currentRechargeTime = 0f;
        Rigidbody shellInstance = Instantiate(shell, fireTransform.position, fireTransform.rotation);
        shellInstance.velocity = launchSpeed * fireTransform.forward;
        shellInstance.gameObject.layer = owner.gameObject.layer;
    }

    private void Update()
    {
        if(fireTransform)
        transform.rotation = fireTransform.rotation;
    }

    public void SetPermametFire(Collider newTarget)
    {
        owner.target = newTarget;
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

    public override bool IsOnSight(Collider target)
    {
        RaycastHit hit;
        Physics.Raycast(fireTransform.position, fireTransform.forward, out hit);
        if (hit.collider != null && hit.collider.Equals(target) ) return true;
        return false;
    }


}