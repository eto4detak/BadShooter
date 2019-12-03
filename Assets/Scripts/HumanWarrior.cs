using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanWarrior : Unit
{
    public override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Health = 44f;
        maxHealth = 77f;
        faction = Faction.Hostile;
    }

    protected override void Update()
    {
        base.Update();
        RunToPoint();
    }


    public static GameObject GetPrefab()
    {
        if (prefab == null)
        {
            prefab = GameObject.Find("HumanWarrior");
        }
        return prefab;
    }




}
