using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShooting : MonoBehaviour
{
    public int PlayerNumber = 1;
    public Rigidbody Shell;
    public Transform FireTransform;
   // public Slider AimSlider;
    public AudioSource ShootingAudio;
    public AudioClip ChargingClip;
    public AudioClip FireClip;
    public float MinLaunchForce = 15f;
    public float MaxLaunchForce = 30f;
    public float MaxChargeTime = 0.75f;

    private GameObject target;
    private bool permametFire = false;
    private float currentRechargeTime = 0f;
    private float rechargeTime = 0.5f;
    private float rotSpeed = 5f;

    private string fireButton;                // The input axis that is used for launching shells.
    private float currentLaunchForce;         // The force that will be given to the shell when the fire button is released.
    private float ChargeSpeed;                // How fast the launch force increases, based on the max charge time.
    private bool fired;                       // Whether or not the shell has been launched with this button press.

    private void OnEnable()
    {
        currentLaunchForce = MinLaunchForce;
    }


    private void Start ()
    {
        fireButton = "Fire" + PlayerNumber;

        ChargeSpeed = (MaxLaunchForce - MinLaunchForce) / MaxChargeTime;
    }


    private void Update ()
    {
        //ControleFire();
        if (target != null)
        {
            ControleFireToTarget();
        }
    }



    public void SetPermametFire(GameObject newTarget)
    {
        target = newTarget;
    }


    private void ControleFire()
    {
        if (currentLaunchForce >= MaxLaunchForce && !fired)
        {
            currentLaunchForce = MaxLaunchForce;
            Fire();
        }
        else if (Input.GetButtonDown(fireButton))
        {
            fired = false;
            currentLaunchForce = MinLaunchForce;
            //ShootingAudio.clip = ChargingClip;
            // ShootingAudio.Play ();
        }
        else if (Input.GetButton(fireButton) && !fired)
        {
            currentLaunchForce += ChargeSpeed * Time.deltaTime;
        }
        else if (Input.GetButtonUp(fireButton) && !fired)
        {
            Fire();
        }
    }


    public void ControleFireToTarget()
    {

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotSpeed * Time.deltaTime);
        if (!fired || currentRechargeTime > rechargeTime)
        {
            Fire();
        }
        else
        {
            currentRechargeTime += Time.deltaTime;
        }
    }


    private void Fire ()
    {
        fired = true;
        currentRechargeTime = 0f;
        Rigidbody shellInstance =
            Instantiate (Shell, FireTransform.position, FireTransform.rotation) as Rigidbody;
        shellInstance.velocity = currentLaunchForce * FireTransform.forward;
        shellInstance.gameObject.layer = gameObject.layer;
        //  ShootingAudio.clip = FireClip;
        // ShootingAudio.Play ();

        currentLaunchForce = MinLaunchForce;
    }
    
}
