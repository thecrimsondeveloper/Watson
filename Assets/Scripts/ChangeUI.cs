using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeUI : MonoBehaviour
{
      public GameObject panel;

      public GameObject prop1;
      public GameObject prop2;
      public GameObject prop3;
      public GameObject prop4;

      private bool panelOn;

      private void Start() {
        panelOn = false;
        panel.SetActive(false);
      }

      public void SwapTitleCards()
      {
        if (panelOn) {
          panel.SetActive(false);
          panelOn = false;
        } else {
            panel.SetActive(true);
            panelOn = true;
        }


          // StartCoroutine(WaitAndChangeLevel());
      }

      IEnumerator WaitAndChangeLevel()
      {
          // suspend execution for 5 seconds
          yield return new WaitForSeconds(5);
          LoadScene();
      }

      public void LoadScene()
      {
          // SceneManager.LoadScene("OtherSceneName", LoadSceneMode.Additive);
          // UnityEngine.SceneManagement.SceneManager.LoadScene(levelName);
      }

      public void ReloadDemo()
      {
          StartCoroutine(WaitAndReloadGame());
      }

      IEnumerator WaitAndReloadGame()
      {
          // suspend execution for 5 seconds
          yield return new WaitForSeconds(45);
          UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
      }
}
