using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialogueBox;

    public Text DialogueText;

    public bool IsActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive && Input.GetKeyDown(KeyCode.Tab))
        {
            DialogueBox.SetActive(false);
            IsActive = false;
        }
    }

    public void ShowBox(string dialogue)
    {
        IsActive = true;
        DialogueBox.SetActive(true);
        DialogueText.text = dialogue;
    }
}
