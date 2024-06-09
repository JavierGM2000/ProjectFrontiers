using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceGoalScore : MonoBehaviour
{

    public TextMeshProUGUI hostText, clientText;
    public int hostScore, clientScore;

    // Start is called before the first frame update
    void Start()
    {
        hostScore = 0;
        clientScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        hostText.text = hostScore.ToString();
        clientText.text = clientScore.ToString();
    }
}
