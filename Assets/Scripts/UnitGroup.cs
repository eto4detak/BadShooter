using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGroup : MonoBehaviour
{
    [Header("Team")]
    public Teams team;

    public UnitCommand command;
    int unitIndent = 2;
    internal List<Unit> units = new List<Unit>();
    private IEnumerator commandCoroutine;
    public Sprite FrontImage { get; set; }

    public virtual void Awake()
    {
        GManager.groupCount++;
        command = new StopCommand(this);
        name = "UnitGroup " + GManager.groupCount;
        foreach (var unit in GetComponentsInChildren<Unit>())
        {
            unit.group = this;
            units.Add(unit);
        }
    }

    protected virtual void Start()
    {
        if (GManager.gMode.unions.CheckEnemies(GManager.pController.Team, team))
        {
            GManager.pController.EnemiesUnits.AddRange(units);
        }
        else if (team.Equals(GManager.pController.Team))
        {
            GManager.pController.PlayerUnits.AddRange(units);
        }
    }

    protected virtual void Update()
    {
        command.DoCommand();
    }







    public bool CheckStopped()
    {
        bool flag = true;
        for (int i = 0; i < units.Count; i++)
        {
            if(units[i] != null)
            {
               if( units[i].agent.remainingDistance > 0.2f)
                {
                    flag = false;
                }
            }
        }
        return flag;
    }

    public static void CreateUnits(Vector3 position)
    {
        UnitGroup group = GetPrefab();
        if (group != null)
        {
            UnitGroup cloneUnitGroup = Instantiate(group, position, Quaternion.identity);
        }
    }
    //public IEnumerator DoCommand()
    //{
    //    while (true)
    //    {
    //        command.DoCommand();
    //        yield return new WaitForSeconds(0.5f);
    //    }
    //}
    public static UnitGroup GetPrefab()
    {
        return Resources.Load<UnitGroup>("Prefabs/UniversalGroup");
    }

    public void MoveGroupToPoint2D(Vector3 newPosition2d, Vector3 groupOffset3d)
    {
        Ray ray = Camera.main.ScreenPointToRay(newPosition2d);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3[] newPositions = Formation.GetSquareGroupPositions(hit.point, units.Count, unitIndent);
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i] == null) continue;
                units[i].agent.destination = newPositions[i] + groupOffset3d;
            }
        }
    }

    public void MoveGroupToPoint3D(Vector3 newPosition)
    {
        Vector3[] newPositions = Formation.GetSquareGroupPositions(newPosition, units.Count, unitIndent);
        for (int i = 0; i < units.Count; i++)
        {
            if (units[i] == null) continue;
            units[i].agent.destination = newPositions[i];
        }
    }

    public static void SetMoveCommand(List<UnitGroup> groups, Vector3 newPosition)
    {
        Vector3[] groupPositions = Formation.GetSquareGroupPositions(new Vector3(), SelectObjects.selectedGroups.Count, Formation.groupIndent);
        for (int i = 0; i < groups.Count; i++)
        {
            groups[i].command = new MoveCommand(groups[i], newPosition, groupPositions[i]);
        }
    }

    public static void SetAttackCommand(List<UnitGroup> attackingGroups, UnitGroup target)
    {
        foreach (var attackingGroup in attackingGroups)
        {
            if (attackingGroup != null && attackingGroup != target)
            {
                attackingGroup.command = new AttackCommand(attackingGroup, target);
            }
        }
    }
    public static void SetPursueCommand(List<UnitGroup> pursuers, UnitGroup target)
    {
        foreach (var pursuer in pursuers)
        {
            if (pursuer != null && pursuer != target)
            {
                pursuer.command = new PursueCommand(pursuer, target);
            }
        }
    }
    //public void StartCoroutineCommand()
    //{
    //    if (commandCoroutine != null)
    //    {
    //        Debug.Log("cancel commandCoroutine");
    //        StopCoroutine(commandCoroutine);
    //        StartCoroutine(commandCoroutine);
    //    }
    //    else
    //    {
    //        Debug.Log("cancel commandCoroutine 1");
    //        commandCoroutine = DoCommand();
    //        StartCoroutine(commandCoroutine);
    //    }
    //}
    //public void StopCoroutineCommand()
    //{
    //    if (commandCoroutine != null)
    //    {
    //        StopCoroutine(commandCoroutine);
    //    }
    //}
    public void TryAttackUnit(Unit target)
    {
        if (GManager.gMode.unions.CheckEnemies(target.group.team, team))
        {
            target.ReceiveDamage();
        }
    }



}

