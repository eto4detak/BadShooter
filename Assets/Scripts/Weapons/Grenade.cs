using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Weapon
{
    public float minLaunchForce = 15f;
    public float maxLaunchForce = 30f;
    public float maxChargeTime = 2f;

    private float currentRechargeTime = 0f;
    private float maxTime = 2f;
    private float rotSpeed = 20f;

    private float currentLaunchForce;         // The force that will be given to the shell when the fire button is released.
    private float chargeSpeed;                // How fast the launch force increases, based on the max charge time.
    private bool fired;                       // Whether or not the shell has been launched with this button press.

    void Start()
    {
        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
       // Destroy(gameObject, maxLifeTime);
    }

    public override void Fire()
    {
        fired = true;
        currentRechargeTime = 0f;
        Rigidbody shellInstance =
            Instantiate(shell, fireTransform.position, fireTransform.rotation) as Rigidbody;
        shellInstance.gameObject.layer = owner.gameObject.layer;
        shellInstance.velocity = currentLaunchForce * (fireTransform.forward + new Vector3(0,1,0)).normalized;
        currentLaunchForce = minLaunchForce;
    }

    public void SetPermametFire(GameObject newTarget)
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
            if (!fired || (currentRechargeTime > maxTime) )
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
