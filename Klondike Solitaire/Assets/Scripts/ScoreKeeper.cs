using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    public float Score = 10000;
    public float ScoreDecrease = 10;
    public float timeInterval = 5;

    private float currentTimeinterval = 0;

    public Selectable[] topStacks;
    public GameObject highScorePanel;
    public Text scoreTextField;

    void Update()
    {
        if (currentTimeinterval <= 0)
        {
            Score -= ScoreDecrease;
            Debug.Log(Score);
            currentTimeinterval = timeInterval;
        }
        else
        {
            currentTimeinterval -= Time.deltaTime;
        }
        CheckWon();
    }

    public void CheckWon()
    {
        int i = 0;
        foreach (Selectable topstack in topStacks)
        {
            i += topstack.value;
        }
        if (i >= 52)
        {
            Win();
        }
    }

    public void Win()
    {
        highScorePanel.SetActive(true);
        scoreTextField.text = $"Your Score: {(Score > 0 ? (int)Score : 0)}";
        print("You have won!");
    }
}
