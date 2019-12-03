using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameHUD : MonoBehaviour
{

    [Header("Images")]
    public GameObject frontImg;
    public GameObject targetImg;
    public GameObject targetList;

    [Header("Commands Buttons")]
    public GameObject btnAttack;
    public GameObject btnMove;
    public GameObject btnStop;


    [Header("UnitUI")]
    public Text damage;
    public Text armor;
    public Text speed;


    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public static Sprite GetFrontImage<T>(T obj) where T : class
    {
        if(obj is UnitGroup) {
            return Resources.Load<Sprite>("Sprite/SqareUnit");
        }
        return null;
    }

    public void SelectUnit<T>(T self) where T : class
    {
        if (self is UnitGroup)
        {
            frontImg.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("Sprite/sqareUnit");
            UnitGroup group = self as UnitGroup;
            damage.text = group.units[0].domage.ToString();
            armor.text = group.units[0].armor.ToString();
            speed.text = group.units[0].speed.ToString();
            if (group.command is AttackCommand)
            {
                btnAttack.GetComponent<Button>().image.color = Color.red;
                targetImg.GetComponent<Button>().image.sprite = null;
                targetList.GetComponentInChildren<Text>().text = "";
            }
        }
    }

    public void SetTarget<T>(T target) where T : class
    {
        if (target is UnitGroup)
        {
            targetImg.GetComponent<Button>().image.sprite = Resources.Load<Sprite>("Sprite/sqareUnit");
            targetList.GetComponentInChildren<Text>().text = (target as UnitGroup).name;
        }
    }
    public void ClearTarget()
    {
        targetImg.GetComponent<Button>().image.sprite = null;
        targetList.GetComponentInChildren<Text>().text = null;
    }

    public void ClearPanel()
    {
        frontImg.GetComponent<Button>().image.sprite = null;
        targetImg.GetComponent<Button>().image.sprite = null;
        targetList.GetComponentInChildren<Text>().text = "";
        btnAttack.GetComponent<Button>().image.color = Color.white;
        btnMove.GetComponent<Button>().image.color = Color.white;
        btnStop.GetComponent<Button>().image.color = Color.white;
    }

}
