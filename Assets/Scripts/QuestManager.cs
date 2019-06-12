using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] public QuestObject[] Quests;

    [SerializeField] public bool[] QuestsCompleted;

    [SerializeField] public DialogueManager DialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        QuestsCompleted = new bool[Quests.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowQuestText(string questText)
    {
        DialogueManager.ShowBox(questText);
    }
}
