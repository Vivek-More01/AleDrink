/**Displays Current Score and Stars */

using UnityEngine;
using TMPro;


public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI starCount;
    public TextMeshProUGUI score;
    [SerializeField]private Logic logic;

    private void Update()
    { 
        starCount.text = logic.ReturnStarCount().ToString();
        score.text = "Score: \n" + logic.CalculateScore().ToString();
    }
}
