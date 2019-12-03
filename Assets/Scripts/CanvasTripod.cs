using UnityEngine;
using UnityEngine.UI;

public class CanvasTripod : MonoBehaviour
{
    private GameObject cam;
    private float objectScale = 0.1f;
    private bool orientate = true;
    private bool scale = false;
    private Vector3 initialScale;

    private Unit currentUnit;
    [Tooltip("Image component displaying health left")]
    public Image healthBarImage;
    [Tooltip("The floating healthbar pivot transform")]
    public Transform healthBarPivot;
    [Tooltip("Whether the health bar is visible when at full health or not")]
    public bool hideFullHealthBar = false;

    // Use this for initialization
    void Start()
    {
        currentUnit = gameObject.GetComponentInParent<Unit>();
        cam = Camera.main.gameObject;
        initialScale = transform.localScale;
    }
    void Update()
    {
        UpdateRatation();
        UpdateValues();
    }


    private void UpdateValues()
    {
        if (currentUnit != null)
        {
            healthBarImage.fillAmount = currentUnit.Health / currentUnit.maxHealth;

            if (hideFullHealthBar)
            {
                healthBarPivot.gameObject.SetActive(healthBarImage.fillAmount != 1);
            }
        }
    }

    private void UpdateRatation()
    {
        //billboarding the canvas
        if (orientate)
        {
            transform.LookAt(transform.position + cam.transform.rotation * Vector3.back, cam.transform.rotation * Vector3.up);
            this.transform.Rotate(0, 180, 0);
        }
        //making it properly scaled
        if (scale)
        {
            Plane plane = new Plane(cam.transform.forward, cam.transform.position);
            float dist = plane.GetDistanceToPoint(transform.position);
            transform.localScale = initialScale * dist * objectScale;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {


    }
}
