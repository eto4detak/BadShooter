using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

public class RunUnitManager : MonoBehaviour
{
    HighlightManager highlight = new HighlightManager();
    SelectObjects selecting;

    private void Awake()
    {
       
    }

    void Start()
    {
        selecting = SelectObjects.instance;
    }



    public void SetSelectedUnits(List<Unit> units)
    {
        foreach (var unit in units)
        {
            selecting.SelectUnit(unit);
        }
    }

    public void SetSelectedUnit(Unit unit)
    {
        selecting.SelectUnit(unit);
    }
    public void RemoveSelectedUnit(Unit unit)
    {
        selecting.SelectUnit(unit);
    }

    internal void UnitMouseOverHandler(Unit unit)
    {
        if (Input.GetMouseButton(0))
        {
            selecting.Deselect();
            selecting.SelectUnit(unit);
        }
        if (Input.GetMouseButtonDown(1))
        {
            if(selecting.Selected.Count > 0)
            {
                List<Unit> targets = new List<Unit>();
                targets.Add(unit);
                foreach (var attacking in selecting.Selected)
                {

                    if (attacking != null)
                    {
                        attacking.SetAttackTarget(targets);
                    }
                }
            }
        }
    }

    private void MoveUnit()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                foreach (var selected in selecting.Selected)
                {
                    selected.agent.destination = hit.point;
                }
            }
        }
    }

    void OnDrawGizmosSelected1111()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(100, 100, Camera.main.nearClipPlane));
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(p, 0.1F);
    }

}