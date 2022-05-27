using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Script used for the dialogue text in the tutorial level.
public class Dialogue : MonoBehaviour
{

    // Variables
    public TextMeshProUGUI text;
    public string[] lines;
    public float textSpeed;
    private int index;
    int line;

    // Start is called before the first frame update
    void Start()
    {
        text.text = string.Empty;
        line = 0;
        startDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        updateDialogue();
    }

    // Method used to start the dialogue text.
    void startDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    // Method used to have a character appear one by one.
    IEnumerator TypeLine()
    {
        // Type each character 1 by 1
        foreach (char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    // Method used to for the next line of text.
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            text.text = string.Empty;
            StartCoroutine(TypeLine());
        } else
        {
            //gameObject.SetActive(false);
        }
    }

    // Method used to change the dialogue after each player input.
    void updateDialogue()
    {
        
        if (Input.GetButtonDown("Horizontal") && text.text == lines[index] && line == 0)
        {
            NextLine();
            line++;
        }
        else if (Input.GetButtonDown("Jump") && text.text == lines[index] && line == 1)
        {
            NextLine();
            line++;
        } else if (PlayerController.isCollected && text.text == lines[index] && line == 2)
        {
            NextLine();
            line++;
        } else if (Input.GetButton("Fire1") && text.text == lines[index] && line == 3)
        {
            NextLine();
        }
    }
}
