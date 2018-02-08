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
}
