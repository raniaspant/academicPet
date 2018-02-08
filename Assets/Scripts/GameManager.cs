using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject happinessText;
    public GameObject hungerText;

    public GameObject robot;


	void Update () {
        happinessText.GetComponent<Text>().text = robot.GetComponent<Robot>().happiness.ToString();
        hungerText.GetComponent<Text>().text = robot.GetComponent<Robot>().hunger.ToString();
    }
}
