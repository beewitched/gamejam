using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour {

    private static DialogueManager instance;
    public static DialogueManager Instance
    {
        get
        {
            return instance;
        }
    }
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animator;
    private Queue<string> sentences = new Queue<string>();
    public KeyCode skip = KeyCode.Space;
    private bool isActive = false;
    public bool IsActive { get; private set; }
    private UnityEvent onDialogueEnd = new UnityEvent();

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        animator.SetBool("IsOpen", false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(skip))
        {
            DisplayNextSentence();
        }
    }
    private void LateUpdate()
    {
        IsActive = isActive;
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
        }

        yield return null;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        onDialogueEnd = dialogue.onDialogueEnd;
        animator.SetBool("IsOpen", true);
        isActive = true;
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
        if (sentences.Count == 0)
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
        isActive = false;
        animator.SetBool("IsOpen", false);

        if (onDialogueEnd != null) onDialogueEnd.Invoke();
    }
}
