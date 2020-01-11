using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitCommand
{
    protected Unit Executer { get; set; }
    protected Unit Target { get; set; }
    public virtual void DoCommand()
    {

    }
    public virtual void OnStay(Unit target)
    {
        //SelfGroup.TryAttackUnit(target);
    }

}

public class AttackCommand : UnitCommand
{
    private CharacterHealth healtTarget;
    public AttackCommand(Unit self, CharacterHealth _target)
    {
        Executer = self;
        healtTarget = _target;
        DoCommand();
    }
    public override void DoCommand()
    {
        if(healtTarget)
        {
            Executer.manager.Fire(healtTarget.Body);
        }
    }
}

public class MoveCommand : UnitCommand
{
    public Vector3 NewPosition { get; set; }
    public Vector3 GroupOffset { get; set; }
    public MoveCommand( Unit self, Vector3 finishPoint)
    {
        Executer = self;
        NewPosition = finishPoint;
        Executer.manager.OnlyMove(NewPosition);
        DoCommand();
    }

    public override void DoCommand()
    {
        //if (Executer.CheckStopped())
        //{
        //    Executer.command = new StopCommand(Executer);
        //}
    }
    public override void OnStay(Unit target)
    {
        //Executer.TryAttackUnit(target);
    }
}
public class StopCommand : UnitCommand
{
    public Vector3 NewPosition { get; set; }
    public Vector3 GroupOffset { get; set; }
    public StopCommand(Unit self)
    {
        Executer = self;
        self.manager.Movement.Stop();
        DoCommand();
    }

    public override void DoCommand()
    {
    }

    public override void OnStay(Unit unitOther)
    {
       // Executer.TryAttackUnit(unitOther);
    }

}

public class PursueCommand : UnitCommand
{
    public PursueCommand(Unit paramUnit, Unit paramTarget)
    {
        Executer = paramUnit;
        Target = paramTarget;

        DoCommand();
    }

    public override void DoCommand()
    {
        if (Target)
        {
            Executer.MoveToPoint3D(Target.transform.position);
        }
    }
    public override void OnStay(Unit unit)
    {
       // Executer.TryAttackUnit(unit);

    }
}
public class ProtectionCommand : UnitCommand
{
    public ProtectionCommand(Unit paramGroup, Unit paramTarget)
    {
        Executer = paramGroup;
        Target = paramTarget;

        DoCommand();
    }

    public override void DoCommand()
    {
        if (Target)
        {
             Executer.MoveToPoint(Target.transform.position);
        }
    }
    public override void OnStay(Unit unit)
    {
       // Executer.TryAttackUnit(unit);
    }
}

