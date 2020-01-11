using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveUnit : MonoBehaviour
{
    private Unit self;
    private Unit playerUnit;
    private float actionRadius = 5;
    private float maxWaitingTime = 2f;
    private float curentWatingTime = 0;

    private void Update()
    {
        FindPlayerUnit();
    }


    public Unit GetPlayerUnit()
    {
        return playerUnit;
    }

    public void FindPlayerUnit()
    {
        curentWatingTime += Time.deltaTime;
        if (curentWatingTime < maxWaitingTime) return;

        curentWatingTime = 0;
        Collider[] radiusUnits = Physics.OverlapSphere(transform.position, actionRadius);
        List<Unit> pUnits = new List<Unit>();
        for (int i = 0; i < radiusUnits.Length; i++)
        {
            Unit pUnit = radiusUnits[i].GetComponent<Unit>();
            if (pUnit && pUnit.faction == Teams.Player)
            {
                playerUnit = pUnit;
                return;
            }
        }
        playerUnit = null;
    }
}
