using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject happinessText;
    public GameObject hungerText;
    public GameObject moneyText;

    public GameObject namePanel;
    public GameObject nameInput;

    public GameObject nameText;
    public GameObject robot;
    public GameObject robotPanel;
    public GameObject[] robotList;

    public GameObject homePanel;
    public Sprite[] homeTileSprites;
    public GameObject[] homeTiles;

    public GameObject background;
    public Sprite[] backgroundOptions;

    public GameObject foodPanel;
    public Sprite[] foodIcons;

    public GameObject taskPanel;
    public Sprite[] taskIcons;

    public GameObject taskInProgressPanel;
    public GameObject noFundsAvailable;

    private float taskTimer;
    private int taskSelected;

    void Start()
    {
        taskSelected = -1;
        taskTimer = 0f;
        if (!PlayerPrefs.HasKey("looks")) 
            PlayerPrefs.SetInt("looks", 0);
        createRobot(PlayerPrefs.GetInt("looks"));
        if(!PlayerPrefs.HasKey("tiles"))
            PlayerPrefs.SetInt("tiles", 0);
        changeTiles(PlayerPrefs.GetInt("tiles"));
        if (!PlayerPrefs.HasKey("background"))
            PlayerPrefs.SetInt("background", 0);
        changeBackground(PlayerPrefs.GetInt("background"));

    }

    void Update () {
        if(taskSelected == 0)   // Experiment in progress
        {
            taskTimer -= Time.deltaTime;
            if (taskTimer < 0) // Done experimenting
            {
                robot.GetComponent<Robot>().updateMoney(2);
                taskSelected = -1;
            }
        }
        else if (taskSelected == 1)   // Experiment in progress
        {
            taskTimer -= Time.deltaTime;
            if (taskTimer < 0) // Done experimenting
            {
                robot.GetComponent<Robot>().updateMoney(5);
                taskSelected = -1;
            }
        }
        happinessText.GetComponent<Text>().text = robot.GetComponent<Robot>().happiness.ToString();
        hungerText.GetComponent<Text>().text = robot.GetComponent<Robot>().hunger.ToString();
        moneyText.GetComponent<Text>().text = robot.GetComponent<Robot>().money.ToString();
        nameText.GetComponent<Text>().text = robot.GetComponent<Robot>().name;

    }

    public void triggerNamePanel(bool b)
    {
        namePanel.SetActive(!namePanel.activeInHierarchy);
        if (b)
        {
            robot.GetComponent<Robot>().name = nameInput.GetComponent<InputField>().text;
            PlayerPrefs.SetString("name", robot.GetComponent<Robot>().name);
        }
    }

    public void buttonBehavior(int i)
    {
        switch (i)
        {
            case (0):
            default:
                robotPanel.SetActive(!robotPanel.activeInHierarchy);
                break;
            case (1):
                homePanel.SetActive(!homePanel.activeInHierarchy);
                break;
            case (2):
                if(taskSelected == -1)
                    foodPanel.SetActive(!foodPanel.activeInHierarchy);
                else
                    StartCoroutine(TasksTimer());
                break;
            case (3):
                if (taskSelected == -1)
                {
                    taskPanel.SetActive(!taskPanel.activeInHierarchy);
                }
                else
                {
                    StartCoroutine(TasksTimer());
                }
                break;
            case (4):
                robot.GetComponent<Robot>().saveRobot();
                Application.Quit();
                break;

        }
    }

    public void createRobot(int i)
    {
        if (robot)
            Destroy(robot);
        robot = Instantiate(robotList[i], Vector3.zero, Quaternion.identity) as GameObject;
        toggle(robotPanel);
        PlayerPrefs.SetInt("looks", i);
    }

    public void changeTiles(int t)
    {
        for (int i = 0; i < homeTiles.Length; i++)
            homeTiles[i].GetComponent<SpriteRenderer>().sprite = homeTileSprites[t];
        toggle(homePanel);
        PlayerPrefs.SetInt("tiles", t);
    }

    public void changeBackground(int i)
    {
        background.GetComponent<SpriteRenderer>().sprite = backgroundOptions[i];
        toggle(homePanel);
        PlayerPrefs.SetInt("background", i);
    }

    public void selectFood(int i)
    {
        toggle(foodPanel);
        robot.GetComponent<Robot>().updateHunger(i); 
    }

    public void toggle(GameObject g)
    {
        if (g.activeInHierarchy)
            g.SetActive(false);
    }

    public void selectTask(int i)
    {
        toggle(taskPanel);
        taskSelected = i;
        if (i == 0)
            taskTimer = 4f;
        else if (i == 1)
            taskTimer = 7f;
    }

    IEnumerator TasksTimer()
    {
        taskInProgressPanel.SetActive(!taskInProgressPanel.activeInHierarchy);
        yield return new WaitForSeconds(1);
        taskInProgressPanel.SetActive(!taskInProgressPanel.activeInHierarchy);
    }

    public void NoFunds()
    {
        StartCoroutine(NoFundsRoutine());
    }

    IEnumerator NoFundsRoutine()
    {
        noFundsAvailable.SetActive(!noFundsAvailable.activeInHierarchy);
        yield return new WaitForSeconds(1);
        noFundsAvailable.SetActive(!noFundsAvailable.activeInHierarchy);
    }

}
