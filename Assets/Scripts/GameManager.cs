using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject happinessText;
    public GameObject hungerText;

    public GameObject namePanel;
    public GameObject nameInput;

    public GameObject nameText;
    public GameObject robot;
    public GameObject robotPanel;
    public GameObject[] robotList;

    void Start()
    {
        if (!PlayerPrefs.HasKey("looks")) ;
            PlayerPrefs.SetInt("looks", 0);
        createRobot(PlayerPrefs.GetInt("looks"));
    }

	void Update () {
        happinessText.GetComponent<Text>().text = robot.GetComponent<Robot>().happiness.ToString();
        hungerText.GetComponent<Text>().text = robot.GetComponent<Robot>().hunger.ToString();
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
                break;
            case (2):
                break;
            case (3):
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
        if (robotPanel.activeInHierarchy)
            robotPanel.SetActive(false);
        PlayerPrefs.SetInt("looks", i);
    }
}
