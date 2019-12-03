using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PController : MonoBehaviour
{
    [Header("Team")]
    public Teams Team;

    [Header("Units")]
    public List<Unit> PlayerUnits = new List<Unit>();
    public List<Unit> EnemiesUnits = new List<Unit>();

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
