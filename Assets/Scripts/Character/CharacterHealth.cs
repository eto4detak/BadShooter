using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public CharacterInfoPanel panel;
    public Sprite fillImage;
    public Color FullHealthColor = Color.green;
    public Color ZeroHealthColor = Color.red;
    public GameObject ExplosionPrefab;

    [SerializeField] private float currentHealth = 100f;

    private Collider body;
    private AudioSource ExplosionAudio;
    private ParticleSystem ExplosionParticles;
    private bool Dead;

    public float CurrentHealth { get => currentHealth;private set => currentHealth = value; }
    public Collider Body { get => body; private set => body = value; }

    private void Awake()
    {
        Body = GetComponent<Collider>();
        ExplosionParticles = Instantiate(ExplosionPrefab).GetComponent<ParticleSystem>();
        ExplosionAudio = ExplosionParticles.GetComponent<AudioSource>();
        ExplosionParticles.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
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
        CurrentHealth -= amount;
        SetHealthUI();
        if (CurrentHealth <= 0f && !Dead)
        {
            OnDeath();
        }
    }


    private void SetHealthUI()
    {
        if (panel && panel.Health)
        {
            panel.Health.value = CurrentHealth/ maxHealth;
        }
    }

    private void OnDeath()
    {
        Dead = true;

        ExplosionParticles.transform.position = transform.position;
        ExplosionParticles.gameObject.SetActive(true);

        ExplosionParticles.Play();

        ExplosionAudio.Play();
        Destroy(gameObject);
    }
}