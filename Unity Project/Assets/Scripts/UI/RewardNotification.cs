using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardNotification : MonoBehaviour {

    [SerializeField] private Image NotificationIcon;
    [SerializeField] private TMPro.TMP_Text NotificationText;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// Shows a screen pop-up, with the item that the player was rewarded.
    /// Script uses the item's name, and sprite property to display the data.
    /// </summary>
    /// <param name="item"></param>
    public void DisplayNotification(InventoryItem item)
    {
        NotificationText.text = item.name;
        NotificationIcon.sprite = item.itemIcon;

        StartCoroutine(DisplayNotif());
    }

    private IEnumerator DisplayNotif()
    {
        anim.Play("SlideIn");
        yield return new WaitForSeconds(2.2f);

        anim.Play("SlideOut");
        yield return new WaitForSeconds(1.1f);

        gameObject.SetActive(false);

        // Clean up after displaying the notification.
        NotificationIcon.sprite = null;
        NotificationText.text   = "";
        yield break;
    }
}
