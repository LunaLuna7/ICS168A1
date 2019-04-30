using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{

    public GameObject gameOver;
    public TextMeshProUGUI goalTimeUi;
    public float currentTime;
    public TextMeshProUGUI timerUI;
    public float goalTime;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {

        UpdateTime();
        if (Player.playersDead >= 2)
            {
                goalTime = currentTime;
                gameOver.SetActive(true);
                goalTimeUi.text = goalTime.ToString("F2");
                Time.timeScale = 0;
            }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void UpdateTime()
    {
        currentTime += Time.deltaTime;
        timerUI.text = currentTime.ToString("F2");

      
    }
}
