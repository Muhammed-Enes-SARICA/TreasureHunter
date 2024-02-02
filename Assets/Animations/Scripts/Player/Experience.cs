using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Experience : MonoBehaviour
{
    public Image expImg;

    public Text levelText;
    public int currentLevel;

    public float currentExperience;
    public float expToNextLevel;

    public AudioSource levelUpAS;

    public static Experience instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance =this;
                
        }
    }
    void Start()
    {
        expImg.fillAmount = currentExperience / expToNextLevel;
        currentLevel = 1;
        levelText.text = currentLevel.ToString();

        currentExperience = PlayerPrefs.GetFloat("Experience", 0);
        expToNextLevel = PlayerPrefs.GetFloat("ExperienceTNL", expToNextLevel);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel",1);

    }

    // Update is called once per frame
    void Update()
    {
        expImg.fillAmount = currentExperience / expToNextLevel;
        levelText.text = currentLevel.ToString();
    }

    public void expMod(float experience)
    {
       
        currentExperience =currentExperience+experience;

        expToNextLevel = PlayerPrefs.GetFloat("ExperienceTNL", expToNextLevel);


        expImg.fillAmount = currentExperience / expToNextLevel;
        if (currentExperience >= expToNextLevel)
        {
            expToNextLevel *= 2;
            currentExperience = 0;
            currentLevel ++;
            levelText.text = currentLevel.ToString();
            PlayerHealth.instance.maxHealt += 15;
            PlayerHealth.instance.currentHealt +=15;
          
            
            AudioManager.instance.PlayAudio(levelUpAS);

            //currentLevel = PlayerPrefs.GetInt("CurrentLevel", currentLevel);
        }

    }
    public void DataSave()
    {

        DataManager.instance.ExperienceData(currentExperience);
        DataManager.instance.ExperienceToNextLevelData(expToNextLevel);
        DataManager.instance.LevelData(currentLevel);



        DataManager.instance.Currenthealth(PlayerHealth.instance.currentHealt);
        PlayerHealth.instance.currentHealt = PlayerPrefs.GetFloat("CurrentHealth");

        DataManager.instance.Maxhealth(PlayerHealth.instance.maxHealt);
        PlayerHealth.instance.maxHealt = PlayerPrefs.GetFloat("MaxHealth");

        currentExperience = PlayerPrefs.GetFloat("Experience");
        expToNextLevel = PlayerPrefs.GetFloat("ExperienceTNL");
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");

        DataManager.instance.CurrentStars(StarBank.instance.BankStar);
        StarBank.instance.BankStar = PlayerPrefs.GetInt("StarAmount");


        GameData.instance.ClearAllDataList();
        GameManagerTwo.instance.GetComponent<Inventory>().InventoryToData();
        GameData.instance.Save();

        DataManager.instance.LastSavedScene(SceneManager.GetActiveScene().buildIndex);
        GameManagerTwo.instance.sceneIndex = PlayerPrefs.GetInt("LastSavedScene");

    }
}
