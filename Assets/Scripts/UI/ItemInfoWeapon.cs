using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoWeapon : MonoBehaviour
{
    public Button btnFront;
    public Text textName;
    public Text count;

    private Weapon _weapon;
    public void Setup(Weapon weapon)
    {
        _weapon = weapon;
        textName.text = weapon.name;
        btnFront.image.sprite = weapon.frontImage;
        btnFront.onClick.AddListener(OnClickSelectWeapon);
    }


    private void OnClickSelectWeapon()
    {
        if(_weapon)  _weapon.SelectWeapon();
    }
}
