using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitCommand
{
    protected UnitGroup SelfGroup { get; set; }
    protected UnitGroup Target { get; set; }
    public virtual void DoCommand()
    {

    }
    public virtual void OnStay(Unit target)
    {
        SelfGroup.TryAttackUnit(target);
    }


}

public class AttackCommand : UnitCommand
{
    public AttackCommand(UnitGroup paramSelf, UnitGroup paramTarget)
    {
        SelfGroup = paramSelf;
        Target = paramTarget;
        DoCommand();
    }
    public override void DoCommand()
    {
        Debug.Log("AttackCommand " + SelfGroup.name);

        if(Target.units.Count > 0)
        {
            for (int i = 0; i < Target.units.Count; i++)
            {
                if (Target.units[i] != null)
                {
                    SelfGroup.MoveGroupToPoint3D(Target.units[i].transform.position);
                    return;
                }
            }
            
        }
    }
    public override void OnStay(Unit target)
    {
        SelfGroup.TryAttackUnit(target);
    }

}
public class MoveCommand : UnitCommand
{
    public Vector3 NewPosition { get; set; }
    public Vector3 GroupOffset { get; set; }
    public MoveCommand(UnitGroup paramGroup, Vector3 paramNewPosition, Vector3 paramGroupOffset)
    {
        SelfGroup = paramGroup;
        NewPosition = paramNewPosition;
        GroupOffset = paramGroupOffset;

        SelfGroup.MoveGroupToPoint2D(NewPosition, GroupOffset);
        DoCommand();
    }

    public override void DoCommand()
    {
        Debug.Log("MoveCommand " + SelfGroup.name);
        if (SelfGroup.CheckStopped())
        {
            SelfGroup.command = new StopCommand(SelfGroup);
        }
    }
    public override void OnStay(Unit target)
    {
        SelfGroup.TryAttackUnit(target);
    }
}
public class StopCommand : UnitCommand
{
    public Vector3 NewPosition { get; set; }
    public Vector3 GroupOffset { get; set; }
    public StopCommand(UnitGroup paramSelfGroup)
    {
        SelfGroup = paramSelfGroup;
        DoCommand();
    }

    public override void DoCommand()
    {
    }

    public override void OnStay(Unit unitOther)
    {
        SelfGroup.TryAttackUnit(unitOther);
    }

}

public class PursueCommand : UnitCommand
{
    public PursueCommand(UnitGroup paramGroup, UnitGroup paramTarget)
    {
        SelfGroup = paramGroup;
        Target = paramTarget;

        DoCommand();
    }

    public override void DoCommand()
    {
        Debug.Log("PursueCommand " + SelfGroup.name);

        if (Target.units.Count > 0)
        {
            for (int i = 0; i < Target.units.Count; i++)
            {
                if (Target.units[i] != null)
                {
                    SelfGroup.MoveGroupToPoint3D(Target.units[i].transform.position);
                    return;
                }
            }
        }
    }
    public override void OnStay(Unit unit)
    {
        SelfGroup.TryAttackUnit(unit);

    }
}
public class ProtectionCommand : UnitCommand
{
    public ProtectionCommand(UnitGroup paramGroup, UnitGroup paramTarget)
    {
        SelfGroup = paramGroup;
        Target = paramTarget;

        DoCommand();
    }

    public override void DoCommand()
    {
        Debug.Log("PursueCommand " + SelfGroup.name);

        if (Target.units.Count > 0)
        {
            for (int i = 0; i < Target.units.Count; i++)
            {
                if (Target.units[i] != null)
                {
                    SelfGroup.MoveGroupToPoint3D(Target.units[i].transform.position);
                    return;
                }
            }
        }
    }
    public override void OnStay(Unit unit)
    {
        SelfGroup.TryAttackUnit(unit);
    }
}

