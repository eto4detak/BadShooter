using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public List<GameObject> items;

    private void Wake()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].SetActive(false);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].SetActive(true);
        }
    }

}
