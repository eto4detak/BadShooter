using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIUnit : MonoBehaviour
{
    public Vector3 startPosition;
    public Unit target;
    public List<Unit> secondaryTargets = new List<Unit>();
    public Unit self;
    private float radius = 30;

    public float maxWaitingTime = 1f;
    public float curentWatingTime = 0;

    public bool targetIsHidding = false;
    public float currentTargetHideTime = 0f;
    public float maxTargetHideTime = 1f;
    AICommand command;
    Action newCommand;

    private void Awake()
    {
        startPosition = transform.position;
        self = GetComponent<Unit>();
        newCommand = TryFindTarget;
    }

    private void Update()
    {
        if ((self.transform.position - startPosition).magnitude > radius)
            newCommand = BackPosition;
        newCommand();
    }


    private void TryFindTarget()
    {
        if (target)
        {
            if (!self.manager.Shooting.IsOnSight)
                currentTargetHideTime += Time.deltaTime;
            else currentTargetHideTime = 0;
        }
        curentWatingTime += Time.deltaTime;
        if (curentWatingTime > maxWaitingTime) FindTarget();
    }


    private void FindTarget()
    {
        curentWatingTime = 0;
        Collider[] radiusUnits = Physics.OverlapSphere(transform.position, radius);
        secondaryTargets.Clear();
        for (int i = 0; i < radiusUnits.Length; i++)
        {
            Unit rUnit = radiusUnits[i].GetComponent<Unit>();
            if (rUnit && Unions.instance.CheckEnemies(self.faction, rUnit.faction))
            {
                secondaryTargets.Add(rUnit);
                Сhase(rUnit);
                return;
            }
        }
    }

    private void Сhase(Unit enemy)
    {
        target = enemy;
        if (!self.manager.Shooting.IsOnSight)
        {
            if (currentTargetHideTime > maxTargetHideTime)
            {
                self.SetCommand(new MoveCommand(self, target.transform.position));
                targetIsHidding = true;
                currentTargetHideTime = 0;
            }
        }
        else if(targetIsHidding)
        {
            self.SetCommand(new StopCommand(self));
            targetIsHidding = false;
        }

        self.SetCommand(new AttackCommand(self, target.manager.Health));
    }

    private void BackPosition()
    {
        if( (self.transform.position - startPosition).magnitude < 0.1f)
        {
            newCommand = TryFindTarget;
            return;
        }
        target = null;
        self.SetCommand(new MoveCommand(self, startPosition));
    }

}
