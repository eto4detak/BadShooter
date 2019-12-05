using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class CharacterManager
{
    public Color playerColor;
    public Transform SpawnPoint;
    [HideInInspector] public int PlayerNumber;
    [HideInInspector] public string ColoredPlayerText;
    [HideInInspector] public GameObject instance;
    [HideInInspector] public int Wins;


    private CharacterMovement Movement;
    private CharacterShooting shooting;
    private Unit unit;
    private GameObject CanvasGameObject;

        
    public void Setup()
    {
        Movement = instance.GetComponent<CharacterMovement>();
        shooting = instance.GetComponent<CharacterShooting>();
        unit = instance.GetComponent<Unit>();
        unit.manager = this;
        // CanvasGameObject = Instance.GetComponentInChildren<Canvas>().gameObject;

        // Movement.PlayerNumber = PlayerNumber;
        // Shooting.PlayerNumber = PlayerNumber;

        ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(playerColor) + ">PLAYER " + PlayerNumber + "</color>";

        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = playerColor;
        }
    }


    public void Fire(GameObject newTarget)
    {
        shooting.SetPermametFire(newTarget);
    }
    public void Move(Vector3 newPosition)
    {
        if (unit == null) return;
        Fire(null);
        unit.agent.destination = newPosition;
    }


    public void DisableControl()
    {
        Movement.enabled = false;
        shooting.enabled = false;

        CanvasGameObject.SetActive(false);
    }


    public void EnableControl()
    {
        Movement.enabled = true;
        shooting.enabled = true;

        CanvasGameObject.SetActive(true);
    }


    public void Reset()
    {
        instance.transform.position = SpawnPoint.position;
        instance.transform.rotation = SpawnPoint.rotation;

        instance.SetActive(false);
        instance.SetActive(true);
    }
}