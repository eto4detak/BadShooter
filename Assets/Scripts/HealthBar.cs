using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public Sprite myHealthSpriteBar;
    public SpriteManager HealthSpriteManager;
    // Start is called before the first frame update
    void Start()
    {
       // HealthSpriteManager = GameObject.Find("healthbar_SpriteManager").GetComponent<SpriteManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRotation();
        UpdateRotation2();
    }

    private void UpdateRotation()
    {
        //if(transform.parent.gameObject.GetComponent<Unit>().Selected == true)
        //{
        //    if (myHealthSpriteBar. == true)
        //    {
        //        HealthSpriteManager.ShowSprite(myHealthSpriteBar);
        //        HealthSpriteManager.UpdateBounds();
        //    }

        //    transform.eulerAngles = new Vector3(
        //        Camera.main.transform.eulerAngles.x,
        //        Camera.main.transform.parent.gameObject.transform.eulerAngles.y,
        //        Camera.main.transform.eulerAngles.z
        //        );
        //}
        //else
        //{
        //    if (myHealthSpriteBar.hidden == false)
        //    {
        //        HealthSpriteManager.HideSprite(myHealthSpriteBar);
        //    }
        //}
    }

    private void UpdateRotation2()
    {
        Debug.Log("test123");
        var camPosition = Camera.main.transform.position;
        // camPosition.
        transform.LookAt(Camera.main.transform.position);
         var wantedPos = Camera.main.WorldToViewportPoint(GManager.character.transform.position);
        // transform.position = wantedPos;
        //  transform.rotation.x = 
    }

}
