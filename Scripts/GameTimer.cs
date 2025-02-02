/**This Script manages the display of time remaining*/

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private Logic logic;
    [SerializeField] private TextMeshProUGUI minutesText;
    [SerializeField] private TextMeshProUGUI secondsText;
    private float timer;

    private void Start()
    {
        timer = logic.ReturnTime();
        DisplayTime(timer);
    }

    private void Update()
    {
        if (timer < 120f)
        {
            timer = logic.ReturnTime();     //Gets the value of time from logic class object
            DisplayTime(timer);             //Calling DisplayTime
        }
    }
    private void DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);        //Get the minute value
        float seconds = 60 - Mathf.FloorToInt(time % 60);   //Get the second value

        minutesText.text = minutes.ToString();      //Write to the display
        secondsText.text = seconds.ToString();
    }
}
