using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{

    public float waitTime = 1f;
    public Dialogue dialogue;
    public GameObject textBox;
    public GameObject dialoguebox;
    public GameObject dialogManager;

    // Use this for initialization
    void Start()
    {
        textBox.SetActive(false);
        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(waitTime);
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartNext()
    {
        dialogManager.SetActive(false);
        textBox.SetActive(true);
        dialoguebox.SetActive(false);
    }
}
