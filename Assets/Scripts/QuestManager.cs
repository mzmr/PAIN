using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] protected QuestObject[] Quests;

    [SerializeField] public bool[] questsCompleted;

    [SerializeField] public DialogueManager DialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        questsCompleted = new bool[Quests.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowQuestText(string questText)
    {
        DialogueManager.DialogueText.text = questText;
    }
}
