using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI Text;

    public int Score = 0;

    public ParticleSystem ParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            ParticleSystem.GetComponent<ParticleSystem>();
            Instance = this;
        }

    }

    // Updatinamas tekstas kai paimamas coin
    public void ChangeScore(int coinValue, string operation)
    {
        ParticleSystem.Play();

        if (operation == "+")
            Score += coinValue;
        else if (operation == "-")
            Score -= coinValue;

        Text.text = "x" + Score.ToString();
    }
}
