using System;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static List<UnitGroup> allGroups = new List<UnitGroup>();
    public static Character character;
    public static float deltaPositionY = 0.1f;
    public static int groupCount = 0;
    public static List<string> gameCommands = new List<string>();
    public static GMode gMode;
    public static PController pController;
    private HumanWarrior humanWarrior;
    public static GameHUD gameHUD;
    public static float minPositionY = -0.1f;
    public static RunUnitManager runUnitManager;
    public static float startPositionY = 5.5f;
    public static SelectObjects selectObjects;

    void Awake()
    {
        gameHUD = (GameHUD)FindObjectOfType(typeof(GameHUD));
        character = (Character)FindObjectOfType(typeof(Character));
        selectObjects = (SelectObjects)FindObjectOfType(typeof(SelectObjects));
        runUnitManager = (RunUnitManager)FindObjectOfType(typeof(RunUnitManager));
        gMode = (GMode)FindObjectOfType(typeof(GMode));
        pController = (PController)FindObjectOfType(typeof(PController));
        gameCommands.Add("Red");
        gameCommands.Add("Blue");
    }

    void Start()
    {
       // SetSettings();

        StartWorld();
        
       // CreateHumanWarrior();
    }



    void Update()
    {
        
    }


    public void CreateHumanWarrior()
    {
        GameObject prefab = HumanWarrior.GetPrefab();
        if (prefab != null)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector3 position = transform.position;
                position.x += i * 1.0f;
                position.y = 5f;
                GameObject prefabWrapper =  Instantiate(prefab, position, Quaternion.identity);
                HumanWarrior warrion = prefabWrapper.GetComponent<HumanWarrior>();

                warrion.NewPosition = new Vector3(10f, 5f, 10f);
            }
        }
    }

    private void SetSettings()
    {
        RectTransform rt = gameHUD.GetComponent(typeof(RectTransform)) as RectTransform;
        selectObjects.SetForbiddenPosition(rt.offsetMax);
    }


    private void StartWorld()
    {
        
        UnitGroup.CreateUnits(new Vector3(40, 7f, 20));
        UnitGroup.CreateUnits(new Vector3(40, 5.6f, 40));
        UnitGroup.CreateUnits(new Vector3(60, 5.6f, 40));

    }

}
