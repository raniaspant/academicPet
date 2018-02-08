using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Robot : MonoBehaviour {

    [SerializeField]
    private int _hunger;
    [SerializeField]
    private int _happiness;

    private bool _serverTime;

	// Use this for initialization
	void Start () {
        updateStatus();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void updateStatus()
    {
        if (!PlayerPrefs.HasKey("_hunger"))
        {
            _hunger = 100;
            PlayerPrefs.SetInt("_hunger", _hunger);
        }else
        {
            _hunger = PlayerPrefs.GetInt("_hunger");
            // TODO update the hunger and stuff
        }
        if (!PlayerPrefs.HasKey("_happiness"))
        {
            _happiness = 100;
            PlayerPrefs.SetInt("_happiness", _happiness);
        }
        else
        {
            _happiness = PlayerPrefs.GetInt("_happiness");
            // TODO update the hunger and stuff
        }
        if (!PlayerPrefs.HasKey("then"))
            PlayerPrefs.SetString("then", getStringTime());
        if (_serverTime)
            updateServer();
        else
            InvokeRepeating("updateDevice", 0f, 30f);
    }

    void updateServer()
    {
        PlayerPrefs.SetString("then", getStringTime());
    }

    void updateDevice()
    {

    }

    TimeSpan getTimeSpan()
    {
        if (_serverTime)
            return new TimeSpan();
        else
            return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("then"));
    }

    string getStringTime()
    {
        DateTime now = DateTime.Now;
        return now.Month + "/" + now.Day + "/" + now.Year + " " + now.Hour + ":" + now.Minute + ":" + now.Second;
    }

    public int hunger
    {
        get { return _hunger; }
        set { _hunger = value; }
    }

    public int happiness
    {
        get { return _happiness; }
        set { _happiness = value; }
    }
}
