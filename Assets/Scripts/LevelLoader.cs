using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private string nextLevelName;

    private bool canChangeLevel;

    // Start is called before the first frame update
    void Start()
    {
        canChangeLevel = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canChangeLevel && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log($"Loading level: {nextLevelName}");
            SceneManager.LoadScene(nextLevelName);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canChangeLevel = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canChangeLevel = false;
        }
    }
}
