using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreUI;

    public void Setscore(int score)
    {
        scoreUI.text = "Score : " + score;
    }
}
