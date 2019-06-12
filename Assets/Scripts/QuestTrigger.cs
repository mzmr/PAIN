using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    private QuestManager QuestManager;

    public int QuestNumber;

    public bool StartQuest;

    public bool EndQuest;

    // Start is called before the first frame update
    void Start()
    {
        QuestManager = FindObjectOfType<QuestManager>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.ToLower() == "player")
        {
            if (QuestManager.QuestsCompleted[QuestNumber])
            {
                return;
            }

            if (StartQuest && !QuestManager.Quests[QuestNumber].gameObject.activeSelf)
            {
                QuestManager.Quests[QuestNumber].gameObject.SetActive(true);
                QuestManager.Quests[QuestNumber].StartQuest();
            }

            if (EndQuest && QuestManager.Quests[QuestNumber].gameObject.activeSelf)
            {
                QuestManager.Quests[QuestNumber].EndQuest();
            }
        }
    }
}
