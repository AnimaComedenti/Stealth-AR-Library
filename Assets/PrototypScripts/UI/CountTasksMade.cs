using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using StealthLib;

namespace StealthDemo
{
    public class CountTasksMade : MonoBehaviour
    {
        [SerializeField] private Text tasksCompleted;

        private UITask[] tasks;
        private int tasksCount = 0;
        private int completedTasks = 0;

        private MyGameManager gameManager;

        void Start()
        {
            tasks = FindObjectsOfType<UITask>();
            tasksCount = tasks.Length;
            gameManager = MyGameManager.Instance;
        }

        void Update()
        {
            if (tasks.Length <= 0)
            {
                gameManager.hasHiderWon = true;
                return;
            }
            for (int i = 0; i < tasks.Length; i++)
            {
                if (tasks[i].isGameCompleted == true)
                {
                    completedTasks++;
                    tasks = tasks.Except(new UITask[] { tasks[i] }).ToArray();
                }
            }

            ShowTasks();
        }

        private void ShowTasks()
        {
            tasksCompleted.text = completedTasks + "/" + tasksCount;
        }
    }
}
