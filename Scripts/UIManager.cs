/*Redundant Script, Exists in loading scene to avoid getting stuck accidentally*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("Game_Screen");
    }
}
