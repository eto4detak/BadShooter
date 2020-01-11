using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    #region Singleton
    static protected AIManager s_Instance;
    static public AIManager instance { get { return s_Instance; } }
    #endregion
    private List<AIUnit> listAI = new List<AIUnit>();
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

}
