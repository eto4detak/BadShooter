using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CharacterInfoPanel : MonoBehaviour
{
    public GameObject frontImg;
    public Slider Health;
    public Scrollbar Morale;
    public CharacterHealth target;

    void Update()
    {
        if (target)
        {
            UpData();
        }
    }

    public void Setup(CharacterHealth newTarget)
    {
        target = newTarget;
        frontImg.GetComponent<Image>().sprite = target.fillImage;
    }


    private void UpData()
    {

       // Health.value = target.panel / target.maxHealth;
       // Morale.value = target.morale / target.maxMorale;
    }




}
