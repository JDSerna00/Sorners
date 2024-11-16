using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject Instructions;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] Slider HealthBar;
    [SerializeField] TextMeshProUGUI EnemiesText;
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject LosePanel; 
    public static UIManager Instance;
    int enemiesKilled = 0;

    void Awake()
    {
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        Instructions.SetActive(true);
        PauseMenu.SetActive(false);
        WinPanel.SetActive(false);
        LosePanel.SetActive(false); 
    }
    public void ToggleInstructions(){
        Instructions.SetActive(false);
    }

    public void UpdateHealth(float value){
        HealthBar.value = value;
    }

    public void UpdateEnemyCount(){
        enemiesKilled++;
        EnemiesText.text = enemiesKilled.ToString()+"/1";
        if(enemiesKilled ==1) Win();
    }

    public void Resume(){
        Time.timeScale = 1.0f;
        PauseMenu.SetActive(false);
    }

    public void Pause(){
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
    }

    public void Restart(){
        Time.timeScale = 1.0f;
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    private void Win(){
        WinPanel.SetActive(true);
    }

    public void Lose(){
        LosePanel.SetActive(true);
    }

}