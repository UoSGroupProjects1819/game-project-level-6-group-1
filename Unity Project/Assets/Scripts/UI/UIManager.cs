using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameManager))]
public class UIManager : MonoBehaviour {

    public static UIManager UIInstance = null;

    [SerializeField] [Tooltip("Set how smooth, the end of the movment should be")] private float movementSmoothness = 2.0f;
    [SerializeField] private GameObject MAINMENU_UI;
    [SerializeField] private Animator planetNameAnim;

    private Vector3 startPoint;
    private Vector3 endPoint;

    private void Awake()
    {
        if (UIInstance == null)
            UIInstance = this;
        else if (UIInstance != null)
            Destroy(gameObject);
    }

    private void Start()
    {
        startPoint = Camera.main.transform.position;
        endPoint = Camera.main.transform.position;
    }

    private void FixedUpdate()
    {
        Camera.main.transform.position = Vector3.Lerp(startPoint, endPoint, movementSmoothness * Time.deltaTime);
    }

    public void GOTOMAINMENU()
    {
        startPoint = Camera.main.transform.position;
        endPoint = new Vector3(0, 0, -10);
    }

    public void GOTOSETTINGS()
    {
        startPoint = Camera.main.transform.position;
        endPoint = new Vector3(18, 0, -10);
    }

    public void PLAYGAME()
    {
        //GameManager.GMInstance.StartGame();

        MAINMENU_UI.SetActive(false);
        planetNameAnim.Play("PlanetNameDropDown");
    }
}
