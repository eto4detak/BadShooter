using System;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static Character character;
    public static float startPositionY = 5.5f;
    public static float deltaPositionY = 0.1f;
    public static float minPositionY = -0.1f;
    public static int groupCount = 0;
    public static List<string> gameCommands = new List<string>();
    public static GMode gMode;
    public static PController pController;
    public static GameHUD gameHUD;
    public static RunUnitManager runUnitManager;

    public static SelectObjects selectObjects;
    public GameObject characterPrefab;
    public  List<CharacterManager> Characters;
    public  WeaponPanel weaponPanel;

    void Awake()
    {
        gameHUD = (GameHUD)FindObjectOfType(typeof(GameHUD));
        character = (Character)FindObjectOfType(typeof(Character));
        selectObjects = (SelectObjects)FindObjectOfType(typeof(SelectObjects));
        runUnitManager = (RunUnitManager)FindObjectOfType(typeof(RunUnitManager));
        gMode = (GMode)FindObjectOfType(typeof(GMode));
        pController = (PController)FindObjectOfType(typeof(PController));
        weaponPanel = (WeaponPanel)FindObjectOfType(typeof(WeaponPanel));
        gameCommands.Add("Red");
        gameCommands.Add("Blue");
    }

    void Start()
    {
        StartWorld();
        SpawnAllCharacters();


    }


    private void SpawnAllCharacters()
    {
        CharacterInfoPanel instanciate = (CharacterInfoPanel)FindObjectOfType(typeof(CharacterInfoPanel));
        if (instanciate)
        {
            instanciate.gameObject.SetActive(false);
            for (int i = 0; i < Characters.Count; i++)
            {
                CharacterInfoPanel healthPanel = null;
                    healthPanel = Instantiate(instanciate, instanciate.transform.position + new Vector3(i * 100, 0, 0),
                        Quaternion.identity, gameHUD.transform.parent);
                    healthPanel.gameObject.SetActive(true);
                   // healthPanel.Setup(Characters[i].);

                Characters[i].instance =
                    Instantiate(characterPrefab, Characters[i].SpawnPoint.position, Characters[i].SpawnPoint.rotation) as GameObject;
                Characters[i].PlayerNumber = i + 1;
                Characters[i].Setup(healthPanel);
            }
        }

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
        


    }

}
