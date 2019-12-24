using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazuka : Weapon
{
    private float currentRechargeTime = 0f;
    private float rechargeTime = 0.5f;
    private float rotSpeed = 5f;

    public float launchSpeed = 100f;

    private bool fired;

    public override void Fire()
    {
        fired = true;
        currentRechargeTime = 0f;
        Rigidbody shellInstance =
            Instantiate(shell, fireTransform.position, fireTransform.rotation) as Rigidbody;
        shellInstance.velocity = launchSpeed * fireTransform.forward;

        shellInstance.gameObject.layer = owner.gameObject.layer;
    }

    public void SetPermametFire(GameObject newTarget)
    {
        owner.target = newTarget;
    }

    public override void FireToTarget()
    {
        owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, 
            Quaternion.LookRotation(owner.target.transform.position - owner.transform.position), rotSpeed * Time.deltaTime);
        if (!fired || currentRechargeTime > rechargeTime)
        {
            Fire();
        }
        else
        {
            currentRechargeTime += Time.deltaTime;
        }
    }
}
