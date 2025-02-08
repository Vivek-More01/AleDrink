/**This Script is used to Display the Highest Score the player achieved */

using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Logic logic;
    public void ScoreDisplay()
    { 
        textMeshProUGUI.text = logic.MaxScore().ToString();
    }
}
