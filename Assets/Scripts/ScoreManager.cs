using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI text;

    public int score = 0;

    public ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            particleSystem.GetComponent<ParticleSystem>();
            instance = this;
        }
        
    }

    // Updatinamas tekstas kai paimamas coin
    public void ChangeScore(int coinValue)
    {
        particleSystem.Play();
        score += coinValue;
        text.text = "x" + score.ToString();
    }

    public void MinusScore(int coinValue)
    {
        score -= coinValue;
        text.text = "x" + score.ToString();
    }

}
