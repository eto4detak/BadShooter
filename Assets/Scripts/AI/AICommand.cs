using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AICommand
{
    protected AIUnit executer;
    public abstract void DoCommand();

}
public class СhaseAICommand :  AICommand
{
    private Unit enemy;
    public СhaseAICommand(Unit _enemy)
    {
        enemy = _enemy;
        DoCommand();
    }

    public override void DoCommand()
    {
        executer.target = enemy;

        if (!executer.self.manager.Shooting.IsOnSight)
        {
            if (executer.currentTargetHideTime > executer.maxTargetHideTime)
            {
                executer.self.SetCommand(new MoveCommand(executer.self, executer.target.transform.position));
                executer.targetIsHidding = true;
                executer.currentTargetHideTime = 0;
            }
        }
        else if (executer.targetIsHidding)
        {
            executer.self.SetCommand(new StopCommand(executer.self));
            executer.targetIsHidding = false;
        }
        executer.self.SetCommand(new AttackCommand(executer.self, enemy.manager.Health));
    }
}

public class BackAICommand : AICommand
{

    public BackAICommand()
    {
        DoCommand();
    }

    public override void DoCommand()
    {

    }
}
