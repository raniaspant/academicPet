using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Robot : MonoBehaviour {

    [SerializeField]
    private int _hunger;
    [SerializeField]
    private int _happiness;
    [SerializeField]
    private string _name;
    private bool _serverTime;
    private int _clickCount;
	// Use this for initialization
	void Start () {
        updateStatus();
        if (!PlayerPrefs.HasKey("name"))
            PlayerPrefs.SetString("name", "Robot");
        _name = PlayerPrefs.GetString("name");
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Animator>().SetBool("Jump", gameObject.transform.position.y > -2.22f);
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 v = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(v), Vector2.zero);
            if (hit)
            {
                //Debug.Log(hit.transform.gameObject.name);
                if(hit.transform.gameObject.tag == "Robot")
                {
                    _clickCount++;
                    if(_clickCount >= 3)
                    {
                        _clickCount = 0;
                        updateHappiness(1);
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1000000));
                    }
                }
            }
        }
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

        TimeSpan ts = getTimeSpan();
        _hunger -= (int)(ts.TotalHours * 2);
        if (_hunger < 0)
            _hunger = 0;
        _happiness -= (int)((100 - _hunger) * (ts.TotalHours / 5));
        if (_happiness < 0)
            _happiness = 0;
        Debug.Log(_happiness);
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

    public string name
    {
        get { return _name; }
        set { _name = value; }
    }

    public void updateHappiness(int i)
    {
        happiness += i;
        if (happiness > 100)
            happiness = 100;
    }
}
