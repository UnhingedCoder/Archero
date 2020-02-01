using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Text highscore;

    private void OnEnable()
    {
        highscore.text = PlayerPrefs.GetInt("highscore", 0).ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNewGameClicked()
    {
        ResetGameValues();
        string sceneName = "Demo";
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

    void ResetGameValues()
    {
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetFloat("HP", 100);
        PlayerPrefs.SetString("sideshot", "false");
    }
}
