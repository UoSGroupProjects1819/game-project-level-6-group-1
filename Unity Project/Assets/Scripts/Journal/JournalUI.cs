using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject journalUI;

    Journal journal;
    JournalSlot[] slots;

    void Start() {
        journal = Journal.instance;
        journal.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<JournalSlot>();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < journal.journalItems.Count)
            {
                slots[i].AddItem(journal.journalItems[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
