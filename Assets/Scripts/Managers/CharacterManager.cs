using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class CharacterManager
{
    public Color playerColor;
    public Transform SpawnPoint;
    [HideInInspector] public int PlayerNumber;
    [HideInInspector] public string ColoredPlayerText;
    [HideInInspector] public GameObject instance;
    [HideInInspector] public int Wins;
    [HideInInspector] public Unit unit;


    private CharacterMovement Movement;
    private CharacterShooting shooting;
    private CharacterHealth health;
    private GameObject CanvasGameObject;

        
    public void Setup(CharacterInfoPanel healthSlider)
    {
        Movement = instance.GetComponent<CharacterMovement>();
        shooting = instance.GetComponent<CharacterShooting>();
        health = instance.GetComponent<CharacterHealth>();
        health.SetData(healthSlider);
        unit = instance.GetComponent<Unit>();
        unit.manager = this;

        ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(playerColor) + ">PLAYER " + PlayerNumber + "</color>";

        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = playerColor;
        }
    }


    public void ChangeWeapon(Weapon weapon)
    {
        shooting.SelectWeapon(weapon);
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


    public List<Weapon> GetWeapons()
    {
        if(shooting)return shooting.weapons;
        return null;
    }


}