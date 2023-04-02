using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuUI : MonoBehaviour
{
    private DataManager dataManager;
    [SerializeField] private TMP_InputField playerNameInput;

    private void Start()
    {
        dataManager = DataManager.Instance;
    }

    public void StartGame()
    {
        if (playerNameInput.text == "")
        {
            dataManager.playerName = "You";
        }
        else
        {
            dataManager.playerName = playerNameInput.text;
        }
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
