using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public Teams faction;

    [HideInInspector] public static List<Unit> allUnits = new List<Unit>();
    [HideInInspector] public static GameObject prefab;
    [HideInInspector] public float speed = 2f;
    [HideInInspector] public bool isGrounded = true;
    [HideInInspector] public Collider body;

    [HideInInspector] public CharacterManager manager;

    protected List<float> allTypeAggression = new List<float>();
    protected UnitCommand command;
    protected List<Unit> commandTargets = new List<Unit>();
    protected float slowDown = 1f;


    public virtual void Awake()
    {
        body = GetComponentInChildren<Collider>();
        allUnits.Add(this);
    }

    private void OnDestroy()
    {
        allUnits.Remove(this);
    }

    protected virtual void Start()
    {
        command = new MoveCommand(this, transform.position);
    }

    protected virtual void Update()
    {
        
    }

    private void OnCollisionStay(Collision other)
    {
        Unit target = other.gameObject.GetComponent<Unit>();
    }

    public bool IsHostile(Unit target)
    {
        if (target.faction.Equals(faction))
        {
            return false;
        }
        return true;
    }

    public void ToGiweWeapon(Unit newOwner, Weapon weapon)
    {
        if (!newOwner || !weapon) return;
        manager.ToGiveWeapon(newOwner.manager, weapon);
    }

    public void SetAttackTarget(List<Unit> target)
    {
        commandTargets.Clear();
        commandTargets.AddRange(target);
    }

    public static void SetMoveCommand(List<Unit> executers, Vector3 newPosition)
    {
        for (int i = 0; i < executers.Count; i++)
        {
            executers[i].command = new MoveCommand(executers[i], newPosition);
        }
    }


    public void SetCommand(UnitCommand newCommand)
    {
        command = newCommand;
    }


    public void MoveToPoint(Vector3 point)
    {
        manager.MoveToCameraPoint(point);
    }


    public void MoveToPoint3D(Vector3 newPosition)
    {
        manager.Move(newPosition);
    }

    public virtual Sprite GetSprite()
    {
       return Resources.Load<Sprite>("Sprite/unit");
    }


    private void CheckErrorPositionY()
    {
        if (transform.position.y < GManager.minPositionY)
        {
            transform.position = new Vector3(transform.position.x, GManager.startPositionY, transform.position.z);
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}



