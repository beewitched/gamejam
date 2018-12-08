using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    private Queue<string> sentences = new Queue<string>();
    public KeyCode skip = KeyCode.Space;

    private void Update()
    {
        if(Input.GetKeyDown(skip))
        {
            DisplayNextSentence();
        }
    }

    private void Start()
    {
        animator.SetBool("IsOpen", false);
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        char[] sentenceToChar = sentence.ToCharArray();
        for(int i = 0; i < sentenceToChar.Length; i++)
        {
            if(sentenceToChar[i] == '<') {
                int count = 0;

                for (int j = i; j < sentenceToChar.Length; j++)
                {
                    if(sentenceToChar[i] == '>')
                    {
                        count++;
                        break;
                    } else
                    {
                        count++;
                    }
                }

                if(count > 0)
                {
                    string command = "";
                    for(int x = i; x < i + count; x++)
                    {
                        command += sentenceToChar[x];
                    }

                    if(!string.IsNullOrEmpty(command))
                    {
                        dialogueText.text += command;
                        i += count -1;
                    }
                }
            }

            dialogueText.text += sentenceToChar[i];
            yield return null;
        }

    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
