/**The main 180 line long code(without comments) that manages the game mechanics.
 Be Careful with modifying this script as everything might break due to a single mistake 

 Some remaining tasks are:
    1.making all time based variables based on time.deltatime(Optional)
    2.making score increase exponential(Optional)
    3.making a tutorial
    4.giving an angry animation to our character on missing a button(Optional)
    5.adding some more mechanics and optimization*/


using System;
using UnityEngine;

public class Logic : MonoBehaviour
{
    [SerializeField] private GameObject progressBar;        //Object for Score Bar
    [SerializeField] private GameObject drinkButton;        //Object for Button
    [SerializeField] private double progressBarFinal;       //Max length of progressBar
    [SerializeField] private int starValue;                 //A Multiplier for score
    private bool pressable = false;                         //Bool for knowing if it is possible to Input
    public bool animatable = false;                         //Bool for knowing if animations should be allowed
    private int successivePressCount = 1;                   //Successful Press count is a additive increment in score
    static public int perfectPressCount = 0;                //It is the star count and is a multiplicative increment in score
    private double progressBarInitialScaleX;                //Lowest length for progress bar, keep it 1
    private double progressBarCurrentScaleX;                //Stores the current length of progressBar
    private double cooldown;                                //Cooldown between successive clicks
    private float time;                                     //Stores the amount of time elapsed
    [SerializeField]private float min;                      //minimum value of cooldown
    [SerializeField]private float max;                      //maximum value of cooldown
    private int maxScore;                                   //maxScore


    private void Start()                //Initialise some variables
    { 
        progressBarInitialScaleX = progressBar.transform.localScale.x;
        cooldown = 200 * Time.deltaTime;
        time = 0;
        maxScore = 0;
    }

    private void Update()               //Update Block
    {
        progressBarCurrentScaleX = progressBar.transform.localScale.x;      //Get the current lrngth of progress Bar
        double progressReduction = 0.00001 * (progressBarCurrentScaleX + progressBarFinal); //Calculate progress Reduction factor
        
        //I added a mechanic by which the size of progress bar and score also decreases with time
        progressBar.transform.localScale = new Vector3(
                (float)Math.Clamp(progressBarCurrentScaleX - progressBarFinal * progressReduction, progressBarInitialScaleX, progressBarFinal), // Clamp to avoid exceeding the target
                progressBar.transform.localScale.y,
                progressBar.transform.localScale.z);

        //Cooldown is a variable I use to track all of the time in the game
        cooldown -= 0.01;
        pressable = IsPressPossible();          //Check if Input is possible
        animatable = IsAnimationPossible();     //Check if Animations are possible
        time += Time.deltaTime;                 //Purpose of time variable is for the time counter
        
        //When player misses a click, cooldown is reset
        if (cooldown < -1)
        {
            successivePressCount = 1;   //Resetting the value of sPC. Explained in UpdateSquareScale() method

            //I randomized the cooldown between min and max
            cooldown = UnityEngine.Random.Range((int)min, (int)max);
        }
    }

    //Method for Updating the Progress Bar
    public void UpdateProgressBar()
    {
       if (progressBar != null) //Check if progressBar is assigned
        { 
            double scaleFactor = (progressBarFinal - progressBarInitialScaleX) / 10;    //Calculate a scalefactor independent of current bar length
            
            //successfulPressCount scales the scale factor, its value depends on past inputs of user
            double newYScale = scaleFactor * successivePressCount;
            

            // Debugging output to confirm the scale change logic
            Debug.Log($"Updating Square Y Scale: {newYScale}");

            // Update only the X scale of the progressBar, I modified the function to be a combination of final length, current length and newYscale
            progressBar.transform.localScale = new Vector3(
                (float)Math.Clamp((newYScale) * (progressBarFinal- progressBarCurrentScaleX)/10 + progressBarCurrentScaleX, 0 , progressBarFinal), // Clamp to avoid exceeding the target
                progressBar.transform.localScale.y,
                progressBar.transform.localScale.z
            );
            Debug.Log($"Updating Square Y Scale: {newYScale * progressBarCurrentScaleX + progressBarCurrentScaleX}");
        }
        else
        {
            Debug.LogError("Square GameObject is not assigned.");
        }        
    }

    //Function that is called on input
    public void Input()
    {
        if (!pressable) //Check if input is not possible
        {
            Debug.Log($"Wait for cooldown: {cooldown}");
            successivePressCount = 1;   //Reset value of succesive counts
        }
        else
        {   
            //Call the below functions to execute the input logic
            UpdateProgressBar();
            CheckAccuracy();
            CalculateScore();

            //Reset Cooldown
            cooldown = UnityEngine.Random.Range((int)min, (int)max);
            
            //I added a difficulty mechanic. As the number of Drinks/input increases, the cooldown decreases.
            if(min > 4)         //A limiter is used in order to prevent cooldown becoming permanently zero
            {
                min -= 0.2f;    //Decrease value of lower cooldown value
                max -= 0.2f;    //Decrease value of upper cooldown value
            }
        }
    }

    //CheckAccuracy classifies a given input into perfect presses and normal presses
    public void CheckAccuracy()
    {
        double timeDif = cooldown;
        if (Math.Abs(timeDif) < 0.5) //if cooldown between 0.5 and -0.5, it's perfect press 
        {
          
            perfectPressCount++;    //Update count of perfect presses
            CalculateScore();       //Calculate score
            
            if (successivePressCount < 3)
            {
                successivePressCount += 2;  //Increase successive press count by two
            };
        }
        else    //For non-perfect presses
        {
            if (successivePressCount < 3)
            {
                successivePressCount++;     //Increase successive press count by one
            };
        }
    }

    //Calculating Score
    public int CalculateScore()
    {
        //Score is calculated as a product of the below terms
        int score = (int)(progressBarCurrentScaleX-1)*(int)progressBarCurrentScaleX * perfectPressCount * starValue;

        if (maxScore < score)   // Check if Max Score is less than current Score
        {
            maxScore = score;   // Update max Score
        }
        return score;           //Return Score for other scripts
    }

    //Check if Input possible
    private bool IsPressPossible()
    {
        if (Math.Abs(cooldown) <= 1)
        {
            return true;
        }
        else { return false; }
    }

    //Check if Animation possible
    public bool IsAnimationPossible()
    {
        //print(cooldown);
        if (cooldown <= 1.5)
        {
            return true;
        }
        else { return false; }
    }

    //Function to reset values of all variables
    public void ResetValues()
    {
        cooldown = 200f * Time.deltaTime;
        animatable = false;
        perfectPressCount = 0;
        successivePressCount = 0;
        time = 0;
        Time.timeScale = 1f;
        progressBarCurrentScaleX = progressBarInitialScaleX;
        Debug.Log($"{time}");
    }

    //Below functions are for message Passing to other scripts
    public double ReturnCoolDown()
    {
        return cooldown;
    }
    public int ReturnStarCount()
    {
        return perfectPressCount;
    }

    public int ReturnTimeValue() {
        return (int)time;   
    }

    public float ReturnTime()
    {
        return time;
    }

    public int MaxScore()
    {
        return maxScore;
    }
}
