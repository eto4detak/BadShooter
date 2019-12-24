using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour
{
    private RaycastHit mouseHit;
    private Ray mouseRay;

    void Update()
    {
        if (!IsPointerOverUIObject())
        {
            CheckLeftClick();
            CheckRightClick();
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData e = new PointerEventData(EventSystem.current);
        e.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(e, results);
        return results.Count > 0;
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
            Unit unitTarget;
            if (Physics.Raycast(mouseRay, out mouseHit, 100))
            {
                //attack
                unitTarget = mouseHit.collider.GetComponent<Unit>();
                if (unitTarget)
                {
                    OnClickRightUnit(unitTarget);
                    return;
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
        Debug.Log("OnClickLeftUnit");
        SelectObjects.Deselect();
        SelectObjects.SelectUnit(unit);
    }

    protected static void OnClickLeftTerrain(Terrain terrain)
    {
        Debug.Log("OnClickLeftTerrain");
        if (SelectObjects.HaveSelected()) SelectObjects.Deselect();
        GManager.gameHUD.ClearPanel();
    }

    private static void OnClickRightUnit(Unit target)
    {
        Debug.Log("OnClickRightUnit ");

        if (SelectObjects.HaveSelected())
        {
            if (SelectObjects.selectedObjects[0] == target) return;

            if (GManager.pController.Team.Equals(target))
            {
                Debug.Log("OnClickRightUnit Team");

                for (int i = 0; i < SelectObjects.selectedObjects.Count; i++)
                {
                    SelectObjects.selectedObjects[i].SetCommand(new PursueCommand(SelectObjects.selectedObjects[i],target));
                }
            }
            else
            {
                Debug.Log("OnClickRightUnit enemy");

                for (int i = 0; i < SelectObjects.selectedObjects.Count; i++)
                {
                    SelectObjects.selectedObjects[i].SetCommand(new AttackCommand(SelectObjects.selectedObjects[i], target));
                }
            }
        }
        
        GManager.gameHUD.SetTarget(target);
    }

    private static void OnClickRightTerrain(Terrain terrain, Vector3 newPosition)
    {
        Debug.Log("OnClickRightTerrain " + SelectObjects.HaveSelected());
        if (SelectObjects.HaveSelected())
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(newPosition), out RaycastHit hit))
                Unit.SetMoveCommand(SelectObjects.selectedObjects, hit.point);
        }
        GManager.gameHUD.ClearTarget();
    }
}
