using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public GameObject dialoguePanel;
    public string[] sentences;
    public float typingSpeed;
    
    private int index;

    private void Start()
    {
        StartCoroutine(ShowText());
    }

    private void Update()
    {
        if (textDisplay.text == sentences[index] && Input.GetKeyDown(KeyCode.Space))
            NextSentence();
    }

    IEnumerator ShowText()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(ShowText());
        }
        else
        {
            textDisplay.text = "";
            dialoguePanel.SetActive(false);
            UIManager.instance.ShowStartingSeeds();
        }
    }
}
