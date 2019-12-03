using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.Collections.Generic;
using System;

public class RunUnitManager : MonoBehaviour
{
    HighlightManager highlight = new HighlightManager();
    
    private void Awake()
    {
       
    }


    void Start()
    {

    }

    void Update()
    {

    }


    public static void SetSelectedUnits(List<Unit> units)
    {
        foreach (var unit in units)
        {
            SelectObjects.TrySelectUnit(unit);
        }
    }
    public static void SetSelectedUnit(Unit unit)
    {
        SelectObjects.TrySelectUnit(unit);
    }
    public static void RemoveSelectedUnit(Unit unit)
    {
        SelectObjects.TrySelectUnit(unit);
    }


    //private void FindGameObjectUnderMouse()
    //{
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        SelectObjects.Deselect();
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            if (hit.collider != null  )
    //            {
    //                Unit unit = hit.collider.gameObject.GetComponent<Unit>();
    //                if (unit != null)
    //                {
    //                    SelectObjects.selectedObjects.Add(unit);
    //                }
    //            }
    //        }
    //    }
    //}

    internal static void UnitMouseOverHandler(Unit unit)
    {
        if (Input.GetMouseButton(0))
        {
            SelectObjects.Deselect();
            SelectObjects.TrySelectUnit(unit);
        }
        if (Input.GetMouseButtonDown(1))
        {
            if(SelectObjects.selectedObjects.Count > 0)
            {
                List<Unit> targets = new List<Unit>();
                targets.Add(unit);
                foreach (var attacking in SelectObjects.selectedObjects)
                {

                    if (attacking != null)
                    {
                        Debug.Log(attacking.name + "attacing 111 " + attacking.name);
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
            Debug.Log("run " + this.name);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                foreach (var selected in SelectObjects.selectedObjects)
                {
                    selected.agent.destination = hit.point;
                }
            }
        }

        //if (agent.velocity.magnitude > 2f)
        //{
        //    animator.SetBool("walk", true);
        //}
        //else
        //{
        //    animator.SetBool("walk", false);
        //}
    }

    void OnDrawGizmosSelected1111()
    {
        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(100, 100, Camera.main.nearClipPlane));
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(p, 0.1F);
    }

}