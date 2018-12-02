using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Journal Item", menuName = "Items/Journal Item")]
public class JournalItem : ScriptableObject {

    new public string name = "New Item";
    public Sprite itemIcon;

    public virtual void DisplayItem()
    {
        
    }
}
