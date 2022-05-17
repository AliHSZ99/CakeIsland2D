using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
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

    void startDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        // Type each character 1 by 1
        foreach (char c in lines[index].ToCharArray())
        {
            text.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

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
        }
    }
}
