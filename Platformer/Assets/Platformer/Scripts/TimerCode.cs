using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerCode : MonoBehaviour
{
    public TextMeshProUGUI timer;
    private int time = 105;
    private bool complete = false;
    private bool shown = false;
    private GameObject player;
    private GameObject goal;


    private void Start()
    {
        player = GameObject.Find("Player");
        goal = GameObject.Find("Goal(Clone)");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int wholeSecond = (int)Mathf.Floor(Time.realtimeSinceStartup);
        if (time - wholeSecond >= 0)
        {
            timer.text = $"Time \n   {(time - wholeSecond).ToString()}";
        }
        
        if(player.transform.position.x >= goal.transform.position.x + -3 &&
           player.transform.position.y <= goal.transform.position.y + 2)
        {
            complete = true;
            Debug.Log("Player reached goal");
        }

        if (time - wholeSecond <= 0 && complete == false)
        {
            if (shown == false)
            {
                Debug.Log("Player Loses time hit 0");
            }
            shown = true;
        }
    }
}
