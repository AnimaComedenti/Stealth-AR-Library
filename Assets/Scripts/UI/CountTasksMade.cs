using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CountTasksMade : MonoBehaviour
{
    [SerializeField] private Text tasksCompleted;

    private UiTask[] tasks;
    private int tasksCount = 0;
    private int completedTasks = 0;

    private MyGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        tasks = FindObjectsOfType<UiTask>();
        tasksCount = tasks.Length;
        gameManager = MyGameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(tasks.Length <= 0)
        {
            gameManager.hasHiderWon = true;
            return;
        }
        for(int i = 0; i < tasks.Length; i++)
        {
            if(tasks[i].isGameCompleted == true)
            {
                completedTasks++;
                tasks = tasks.Except(new UiTask[] { tasks[i] }).ToArray();
            }
        }

        ShowTasks();
    }

    private void ShowTasks()
    {
        tasksCompleted.text = completedTasks + "/" + tasksCount;
    }
}
