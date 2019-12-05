using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public int PlayerNumber = 1;
    public float Speed = 12f;
    public float TurnSpeed = 180f;
    public AudioSource MovementAudio;
    public AudioClip EngineIdling;
    public AudioClip EngineDriving;
    public float PitchRange = 0.2f;

    /*
    private string MovementAxisName;     
    private string TurnAxisName;         
    private Rigidbody Rigidbody;         
    private float MovementInputValue;    
    private float TurnInputValue;        
    private float OriginalPitch;         


    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable ()
    {
        Rigidbody.isKinematic = false;
        MovementInputValue = 0f;
        TurnInputValue = 0f;
    }


    private void OnDisable ()
    {
        Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        MovementAxisName = "Vertical" + PlayerNumber;
        TurnAxisName = "Horizontal" + PlayerNumber;

        OriginalPitch = MovementAudio.pitch;
    }
    */

    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
    }


    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
    }


    private void FixedUpdate()
    {
        // Move and turn the tank.
    }


    private void Move()
    {
        // Adjust the position of the tank based on the player's input.
    }


    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
    }
}
