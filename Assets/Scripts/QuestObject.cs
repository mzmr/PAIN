using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObject : MonoBehaviour
{
    public int questNumber;

    public QuestManager QuestManager;

    public string StartText;
    public string EndText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartQuest()
    {
        QuestManager.ShowQuestText(StartText);
    }

    public void EndQuest()
    {
        QuestManager.ShowQuestText(EndText);
        QuestManager.questsCompleted[questNumber] = true;
        gameObject.SetActive(false);
    }
}
