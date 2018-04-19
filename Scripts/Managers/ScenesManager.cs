using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Castle
{
    public class ScenesManager : MonoBehaviour
    {
        private void Update()
        {
            CheckUserInput();
        }
        public void LoadScene(int sceneIndex)
        {
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
            if (Input.GetKey(KeyCode.Escape))
            {
                Quit();
            }
        }

        private void Quit()
        {
            Application.Quit();
        }
    }
}

