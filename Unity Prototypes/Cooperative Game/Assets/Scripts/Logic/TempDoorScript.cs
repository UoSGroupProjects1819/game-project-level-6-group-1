using UnityEngine;

public class TempDoorScript : MonoBehaviour
{

    [SerializeField]
    private GameObject triggerOne;
    [SerializeField]
    private Vector3 openedPosition;

    private TriggerScript tOneScript;

    private Vector3 startPos;

    private bool isOpened   = false;

    void Start()
    {
        tOneScript = triggerOne.GetComponent<TriggerScript>();
        startPos = transform.position;
    }

    void Update()
    {
        if (tOneScript.isPressed)
        {
            gameObject.transform.position = openedPosition;

        }
        else
        {
            transform.position = startPos;
        }
    }
}
