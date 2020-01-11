using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class CharacterManager
{
    public Color playerColor;
    [HideInInspector] public GameObject instance;

    private Unit unit;
    private CharacterMovement movement;
    private CharacterShooting shooting;
    private CharacterHealth health;

    public CharacterShooting Shooting { get => shooting; private set => shooting = value; }
    public CharacterMovement Movement { get => movement; private set => movement = value; }
    public CharacterHealth Health { get => health; private set => health = value; }

    public void Setup(GameObject resInstance = null)
    {
        if (resInstance == null) resInstance = instance;
        Movement = resInstance.GetComponent<CharacterMovement>();
        Shooting = resInstance.GetComponent<CharacterShooting>();
        Health = resInstance.GetComponent<CharacterHealth>();
        
        unit = resInstance.GetComponent<Unit>();
        unit.manager = this;

        MeshRenderer[] renderers = resInstance.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = playerColor;
        }
    }

    public List<Weapon> GetWeapons()
    {
        if (Shooting) return Shooting.weapons;
        return null;
    }

    public void ToGiveWeapon(CharacterManager newOwner, Weapon weapon)
    {
        Shooting.RemoveWeapon(weapon);
        newOwner.Shooting.AddWeapon(weapon);
        newOwner.unit.faction = Teams.Player;
    }

    public void SetupHealthPanel(CharacterInfoPanel healthPanel)
    {
        Health.SetData(healthPanel);
    }

    public void ChangeWeapon(Weapon weapon)
    {
        Shooting.SelectWeapon(weapon);
    }

    public void Fire(Collider newTarget)
    {
        Shooting.SetPermametFire(newTarget);
    }

    public void MoveToCameraPoint(Vector3 newPosition)
    {
        Movement.MoveToCameraPoint(newPosition);
    }
    public void OnlyMove(Vector3 newPosition)
    {
        if (unit == null) return;
        Fire(null);
        Movement.MoveToPoint(newPosition);
    }

    public void Move(Vector3 newPosition)
    {
        Movement.MoveToPoint(newPosition);
    }


}