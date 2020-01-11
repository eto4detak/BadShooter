using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour
{
    private RaycastHit mouseHit;
    private Ray mouseRay;
    private SelectObjects selecting;


    private void Start()
    {
        selecting = SelectObjects.instance;
    }

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
            CharacterHealth unitTarget;
            if (Physics.Raycast(mouseRay, out mouseHit, 100))
            {
                //attack
                unitTarget = mouseHit.collider.GetComponent<CharacterHealth>();
                if (unitTarget)
                {
                    OnClickRightObject(unitTarget);
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

    protected void OnClickLeftUnit(Unit unit)
    {
        selecting.Deselect();
        selecting.SelectUnit(unit);
    }

    protected void OnClickLeftTerrain(Terrain terrain)
    {
        if (selecting.HaveSelected()) selecting.Deselect();
        GManager.gameHUD.ClearPanel();
    }

    private void OnClickRightObject(CharacterHealth target)
    {
        if (selecting.HaveSelected())
        {


            if (selecting.Selected[0] == target) return;
            Unit uTarget = target.GetComponent<Unit>();
            if (!uTarget || uTarget.IsHostile(selecting.Selected[0]))
            {
                for (int i = 0; i < selecting.Selected.Count; i++)
                {
                    selecting.Selected[i].SetCommand(new AttackCommand(selecting.Selected[i], target));
                }
            }
        }
        GManager.gameHUD.SetTarget(target);
    }


    private void OnClickRightTerrain(Terrain terrain, Vector3 newPosition)
    {

        if (selecting.HaveSelected())
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(newPosition), out RaycastHit hit))
            {
                Unit.SetMoveCommand(selecting.Selected, hit.point);
            }
        }
        GManager.gameHUD.ClearTarget();
    }
}
