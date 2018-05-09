using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Castle
{
    public class ScenesManager : MonoBehaviour
    {
        public static ScenesManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void Update()
        {
            CheckUserInput();
        }

        public void LoadScene(int sceneIndex)
        {
            DataManager.WriteDataToDisk();
            try
            {
                SceneManager.LoadScene(sceneIndex);
            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());
                Quit();
            }
        }

        public void OnExit()
        {
            Quit();
        }

        private void CheckUserInput()
        {
            if (Input.GetKey(KeyCode.Escape)) {
                Quit();
            }
        }

        private void Quit()
        {
            DataManager.WriteDataToDisk();
            Application.Quit();
        }
    }
}

