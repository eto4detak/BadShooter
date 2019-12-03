using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    private RaycastHit mouseHit;
    private Ray mouseRay;
    void Start()
    {
        
    }
 
    void Update()
    {
        CheckLeftClick();
        CheckRightClick();
    }


    private void CheckLeftClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out mouseHit, 100))
            {
                Unit filter = mouseHit.collider.GetComponent(typeof(Unit)) as Unit;
                if (filter)
                {
                    OnClickLeftUnit(filter);
                    return;
                }
                Terrain filterTerrain = mouseHit.collider.GetComponent(typeof(Terrain)) as Terrain;
                if (filterTerrain)
                {
                    OnClickLeftTerrain(filterTerrain);

                    return;
                }
            }
        }
    }

    private void CheckRightClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {

            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(mouseRay, out mouseHit, 100))
            {
                //attack

                Unit unitTarget = mouseHit.collider.GetComponent(typeof(Unit)) as Unit;
                if (unitTarget)
                {
                    UnitGroup target = unitTarget.group;
                    if (target)
                    {
                        OnClickRightUnit(target);
                        
                        return;
                    }
                }

                //move

                Terrain filterTerrain = mouseHit.collider.GetComponent(typeof(Terrain)) as Terrain;
                if (filterTerrain)
                {
                    OnClickRightTerrain(filterTerrain, Input.mousePosition);

                    return;
                }
            }
        }
    }

    protected static void OnClickLeftUnit(Unit unit)
    {
        SelectObjects.Deselect();
        SelectObjects.TrySelectUnit(unit);
        GManager.gameHUD.SelectUnit(unit.group);
    }
    protected static void OnClickLeftTerrain(Terrain terrain)
    {
        if (SelectObjects.HaveSelected())
        {
            SelectObjects.Deselect();
        }
        GManager.gameHUD.ClearPanel();
    }

    internal static void OnClickRightUnit(UnitGroup target)
    {
        if (SelectObjects.HaveSelected())
        {
            if (GManager.pController.Team.Equals(target.team))
            {
                UnitGroup.SetPursueCommand(SelectObjects.selectedGroups, target);
            }
            else
            {
                UnitGroup.SetAttackCommand(SelectObjects.selectedGroups, target);
            }
        }
        
        GManager.gameHUD.SetTarget(target);
    }
    internal static void OnClickRightTerrain(Terrain terrain, Vector3 newPosition)
    {
        if (SelectObjects.HaveSelected())
        {
            UnitGroup.SetMoveCommand(SelectObjects.selectedGroups, newPosition);
        }
        GManager.gameHUD.ClearTarget();
    }
}
