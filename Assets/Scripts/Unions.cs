using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Unions : MonoBehaviour
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

    #region Singleton
    static protected Unions s_Instance;
    static public Unions instance { get { return s_Instance; } }
    #endregion

    void Awake()
    {
        #region Singleton
        if (s_Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        s_Instance = this;
        #endregion
    }

    public bool CheckEnemies(Teams _team1, Teams _team2)
    {
        for (int i = 0; i < _Unions.Count; i++)
        {
            if ( (_Unions[i].Team1.Equals(_team1) && _Unions[i].Team2.Equals(_team2))
                || (_Unions[i].Team1.Equals(_team2) && _Unions[i].Team2.Equals(_team1)) )
            {
                if (_Unions[i].Union == p_union.Enemies) return true;
            }
        }
        return false;
    }

}
