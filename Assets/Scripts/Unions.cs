using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Unions
{
    [Serializable]
    public enum p_union
    {
        Allies,
        Enemies,
        Neitrals,
    }
    [Serializable]
    public struct p_unions
    {
        public string Name;
        public Teams Team1;
        public p_union Union;
        public Teams Team2;
    }

    public List<p_unions> _Unions = new List<p_unions>();

    public bool CheckEnemies(Teams _team1, Teams _team2)
    {
        for (int i = 0; i < _Unions.Count; i++)
        {
            if ( (_Unions[i].Team1.Equals(_team1) && _Unions[i].Team2.Equals(_team2))
                || (_Unions[i].Team1.Equals(_team2) && _Unions[i].Team2.Equals(_team1)) )
            {
                switch (_Unions[i].Union)
                {
                    case p_union.Enemies:
                        return true;
                }
            }
        }
        return false;
    }

}
