using System;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static float startPositionY = 5.5f;
    public static float deltaPositionY = 0.1f;
    public static float minPositionY = -0.1f;
    public static int groupCount = 0;
    public static List<string> gameCommands = new List<string>();
    public static GMode gMode;
    public static PController pController;
    public static GameHUD gameHUD;

    public SelectObjects selectObjects;
    public Transform spawnPoint;
    public GameObject characterPrefab;
    public List<CharacterManager> Characters;
    public WeaponPanel weaponPanel;
    public PassiveUnitPanel passivePanel;

    private List<Unit> units;

    void Awake()
    {
        gameHUD = (GameHUD)FindObjectOfType(typeof(GameHUD));
        selectObjects = (SelectObjects)FindObjectOfType(typeof(SelectObjects));
        gMode = (GMode)FindObjectOfType(typeof(GMode));
        pController = (PController)FindObjectOfType(typeof(PController));
        weaponPanel = (WeaponPanel)FindObjectOfType(typeof(WeaponPanel));
        passivePanel = (PassiveUnitPanel)FindObjectOfType(typeof(PassiveUnitPanel));
        units = Unit.allUnits;
    }

    void Start()
    {
        SetupUnitManager();
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
                CharacterInfoPanel healthPanel = Instantiate(instanciate, instanciate.transform.position + new Vector3(i * 100, 0, 0),
                    Quaternion.identity, gameHUD.transform.parent);
                healthPanel.gameObject.SetActive(true);

                Characters[i].instance =
                    Instantiate(characterPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
                Characters[i].Setup();
                Characters[i].SetupHealthPanel(healthPanel);
            }
        }
    }


    private void SetupUnitManager()
    {
        for (int i = 0; i < units.Count; i++)
        {
            CharacterManager cm = new CharacterManager();
            cm.Setup(units[i].gameObject);
        }
    }


    private void SetSettings()
    {
        RectTransform rt = gameHUD.GetComponent(typeof(RectTransform)) as RectTransform;
        selectObjects.SetForbiddenPosition(rt.offsetMax);
    }

}
