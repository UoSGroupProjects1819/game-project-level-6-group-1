using UnityEngine;

public class DoorScript : MonoBehaviour {

    [SerializeField]
    private GameObject triggerOne;
    [SerializeField]
    private GameObject triggerTwo;
    [SerializeField]
    private Vector3 openedPosition;

    private TriggerScript tOneScript;
    private TriggerScript tTwoScript;

    private bool isOpened = false;
    
	void Start () {
        tOneScript = triggerOne.GetComponent<TriggerScript>();
        tTwoScript = triggerTwo.GetComponent<TriggerScript>();
	}
	
	void Update () {
        if ((tOneScript.isPressed && tTwoScript.isPressed) && !isOpened)
        {
            isOpened = true;
            gameObject.transform.position += openedPosition;
            
        }
	}
}
