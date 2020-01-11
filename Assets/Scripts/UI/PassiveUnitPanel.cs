using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveUnitPanel : MonoBehaviour
{
    public PassiveItemPanel itemInstance;
    public GameObject itemContent;

    private List<PassiveItemPanel> items = new List<PassiveItemPanel>();
    private CharacterManager weaponSource;
    private CharacterManager passiveUnit;

    public CharacterManager PassiveUnit { get => passiveUnit; private set => passiveUnit = value; }

    #region Singleton
    static protected PassiveUnitPanel s_Instance;
    static public PassiveUnitPanel instance { get { return s_Instance; } }

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

    public void SetOwner(CharacterManager _weaponSource, CharacterManager _passiveUni)
    {
        gameObject.SetActive(true);
        weaponSource = _weaponSource;
        PassiveUnit = _passiveUni;
        if(weaponSource != null) UpdateWeaponItems();
        StartCoroutine(LivePanel());
    }

    public void ClearOwner()
    {
        gameObject.SetActive(false);
        weaponSource = null;
    }

    public void RemoveTarget()
    {
        gameObject.SetActive(false);
        weaponSource = null;
    }


    private void UpdateWeaponItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].gameObject.SetActive(false);
        }
        List<Weapon> recievedWeapons = weaponSource.GetWeapons();
        if (recievedWeapons != null)
        {
            for (int i = 0; i < recievedWeapons.Count; i++)
            {
                if (i >= items.Count)
                {
                    items.Add(Instantiate(itemInstance, transform.position, Quaternion.identity, itemContent.transform));
                }
                items[i].Setup(recievedWeapons[i], this);
                items[i].gameObject.SetActive(true);
            }
        }
    }


    private IEnumerator LivePanel()
    {
        yield return new WaitForSeconds(5f);
        ClearOwner();
    }


}
