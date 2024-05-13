using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{    
    public Button[] levelButtons;
    private int _currentLevel = 1;

    void Awake()
    {
        //If Game Manager Object is missing from the Hierarchy
        if(GameManager.Instance == null)
        {
            _currentLevel = 1;
        }
        else
        {
            _currentLevel = GameManager.Instance.GetCurrentLevelNumber();
        }
        
        //turns all the buttons off so you can't click on them
        for(int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = false;
        }
    }

    void Start()
    {
        //Turn button active based on current available level
        for(int i = 0; i < _currentLevel; i++)
        {
            levelButtons[i].interactable = true;
            AssignOnButtonClick(i);
        }
    }
    private void AssignOnButtonClick(int number)
    {
        levelButtons[number].onClick.AddListener(() => OnButtonClicked(number + 1));
    }

    private void OnButtonClicked(int buttonNumber)
    {
        //Debug.Log("I clicked button number " + buttonNumber);
        
        //Go to scene based on the name: Level 1, Level 2, etc
        string sceneName = "Level " + buttonNumber;
        SceneManager.LoadScene(sceneName);
    }

}
