using UnityEngine;
using UnityEngine.UI;

public class PassiveItemPanel : MonoBehaviour
{
    public Button btnFront;
    public Text textName;
    public Text count;

    private Weapon weapon;
    private PassiveUnitPanel panel;

    public void Setup(Weapon _weapon, PassiveUnitPanel _panel)
    {
        weapon = _weapon;
        panel = _panel;
        textName.text = _weapon.name;
        btnFront.image.sprite = _weapon.frontImage;
        btnFront.onClick.AddListener(OnClickSelectWeapon);
    }


    private void OnClickSelectWeapon()
    {
        if (!weapon) return;
        Unit selected =  weapon.owner.GetComponent<Unit>();
        selected.manager.ToGiveWeapon(panel.PassiveUnit, weapon);
        panel.ClearOwner();
    }
}
