using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public float StartingHealth = 1f;               // The amount of health each tank starts with.
    //public Slider Slider;                             // The slider to represent how much health the tank currently has.
   // public Image FillImage;                           // The image component of the slider.
    public Color FullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
    public GameObject ExplosionPrefab;                // A prefab that will be instantiated in Awake, then used whenever the tank dies.


    private AudioSource ExplosionAudio;               // The audio source to play when the tank explodes.
    private ParticleSystem ExplosionParticles;        // The particle system the will play when the tank is destroyed.
    private float CurrentHealth;                      // How much health the tank currently has.
    private bool Dead;                                // Has the tank been reduced beyond zero health yet?


    private void Awake()
    {
        ExplosionParticles = Instantiate(ExplosionPrefab).GetComponent<ParticleSystem>();
        ExplosionAudio = ExplosionParticles.GetComponent<AudioSource>();
        ExplosionParticles.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        CurrentHealth = StartingHealth;
        Dead = false;

        SetHealthUI();
    }


    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        SetHealthUI();

        Debug.Log("TakeDamage " + CurrentHealth);

        if (CurrentHealth <= 0f && !Dead)
        {
            OnDeath();
        }
    }


    private void SetHealthUI()
    {
        // Set the slider's value appropriately.
     //   Slider.value = CurrentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
      //  FillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, CurrentHealth / StartingHealth);
    }


    private void OnDeath()
    {

        Debug.Log("test123");

        Dead = true;

        ExplosionParticles.transform.position = transform.position;
        ExplosionParticles.gameObject.SetActive(true);

        ExplosionParticles.Play();

        ExplosionAudio.Play();

        gameObject.SetActive(false);

    }
}