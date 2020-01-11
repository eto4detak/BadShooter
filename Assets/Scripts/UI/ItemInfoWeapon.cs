using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoWeapon : MonoBehaviour
{
    public Button btnFront;
    public Text textName;
    public Text count;

    private Weapon weapon;
    public void Setup(Weapon _weapon)
    {
        weapon = _weapon;
        textName.text = _weapon.name;
        btnFront.image.sprite = _weapon.frontImage;
        btnFront.onClick.AddListener(OnClickSelectWeapon);
    }


    private void OnClickSelectWeapon()
    {
        if(weapon)  weapon.SelectWeapon();
    }
}
