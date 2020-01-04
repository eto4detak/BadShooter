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
    [HideInInspector] public string ColoredPlayerText;
    [HideInInspector] public GameObject instance;

    private Unit unit;
    private CharacterMovement movement;
    private CharacterShooting shooting;
    private CharacterHealth health;
    private GameObject CanvasGameObject;

    public CharacterManager()
    {

    }

    public void Setup(GameObject resInstance = null)
    {
        if (resInstance == null) resInstance = instance;
        movement = resInstance.GetComponent<CharacterMovement>();
        shooting = resInstance.GetComponent<CharacterShooting>();
        health = resInstance.GetComponent<CharacterHealth>();
        
        unit = resInstance.GetComponent<Unit>();
        unit.manager = this;

        //ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(playerColor) + ">PLAYER " + PlayerNumber + "</color>";

        MeshRenderer[] renderers = resInstance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = playerColor;
        }
    }

    public void SetupHealthPanel(CharacterInfoPanel healthPanel)
    {
        health.SetData(healthPanel);
    }


    public void ChangeWeapon(Weapon weapon)
    {
        shooting.SelectWeapon(weapon);
    }


    public void Fire(Collider newTarget)
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
        movement.enabled = false;
        shooting.enabled = false;

        CanvasGameObject.SetActive(false);
    }

    public void EnableControl()
    {
        movement.enabled = true;
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