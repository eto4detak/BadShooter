using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    public float startingHealth = 100f;               // The amount of health each tank starts with.
    public float maxHealth = 100f;               // The amount of health each tank starts with.
    public CharacterInfoPanel panel;                             // The slider to represent how much health the tank currently has.
    public Sprite fillImage;                           // The image component of the slider.
    public Color FullHealthColor = Color.green;       // The color the health bar will be when on full health.
    public Color ZeroHealthColor = Color.red;         // The color the health bar will be when on no health.
    public GameObject ExplosionPrefab;                // A prefab that will be instantiated in Awake, then used whenever the tank dies.


    private AudioSource ExplosionAudio;               // The audio source to play when the tank explodes.
    private ParticleSystem ExplosionParticles;        // The particle system the will play when the tank is destroyed.
    private float currentHealth;                      // How much health the tank currently has.
    private bool Dead;                                // Has the tank been reduced beyond zero health yet?


    private void Awake()
    {
        ExplosionParticles = Instantiate(ExplosionPrefab).GetComponent<ParticleSystem>();
        ExplosionAudio = ExplosionParticles.GetComponent<AudioSource>();
        ExplosionParticles.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        currentHealth = startingHealth;
        Dead = false;

        SetHealthUI();
    }


    public void SetData(CharacterInfoPanel newPanel)
    {
        panel = newPanel;
        panel.Setup(this);
        OnEnable();
    }


    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        SetHealthUI();

        if (currentHealth <= 0f && !Dead)
        {
            OnDeath();
        }
    }


    private void SetHealthUI()
    {
        if (panel && panel.Health)
        {
            panel.Health.value = currentHealth/ maxHealth;
        }
        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        //  FillImage.color = Color.Lerp(ZeroHealthColor, FullHealthColor, CurrentHealth / StartingHealth);
    }


    private void OnDeath()
    {

        Dead = true;

        ExplosionParticles.transform.position = transform.position;
        ExplosionParticles.gameObject.SetActive(true);

        ExplosionParticles.Play();

        ExplosionAudio.Play();

        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}