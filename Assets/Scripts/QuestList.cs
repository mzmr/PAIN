using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class QuestList : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = createQuestListString();
    }

    private string createQuestListString()
    {
        var sb = new StringBuilder();
        foreach (var quest in questManager.Quests)
        {
            if (questManager.Quests[quest.questNumber].gameObject.activeSelf &&
                !questManager.QuestsCompleted[quest.questNumber])
            {
                sb.Append(quest.StartText).Append("\n");
            }
        }

        return sb.ToString();
    }
}