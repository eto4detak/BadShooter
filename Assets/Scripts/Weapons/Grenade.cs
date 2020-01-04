using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Weapon
{
    private float currentRechargeTime = 0f;
    private float rechargeTime = 2f;
    private float rotSpeed = 20f;
    private float launchSpeed = 5;
    private bool firing;

    void Start()
    {
       // Destroy(gameObject, maxLifeTime);
    }

    public override void Fire()
    {
        firing = true;
        currentRechargeTime = 0f;
        Rigidbody shellInstance =
            Instantiate(shell, fireTransform.position, fireTransform.rotation) as Rigidbody;
        shellInstance.gameObject.layer = owner.gameObject.layer;
        shellInstance.velocity = launchSpeed * (fireTransform.forward + new Vector3(0,1,0)).normalized;
    }

    public void SetPermametFire(Collider newTarget)
    {
        owner.target = newTarget;
    }

    public override void FireToTarget()
    {
        if (owner.target)
        {
            owner.transform.LookAt(owner.target.transform.position);
            owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation,
                Quaternion.LookRotation(owner.target.transform.position - owner.transform.position), rotSpeed * Time.deltaTime);
            if (!firing || (currentRechargeTime > rechargeTime) )
            {
                Fire();
            }
            else
            {
                currentRechargeTime += Time.deltaTime;
            }
        }

    }
}
