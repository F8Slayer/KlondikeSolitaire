using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    public int CardShirtNumber;
    public CardShirtManager shirtManager = null;

    void Start()
    {
        if (PlayerPrefs.HasKey("CardShirtNumber"))
        {
            CardShirtNumber = PlayerPrefs.GetInt("CardShirtNumber");
        }
        else
        {
            CardShirtNumber = 0;
        }
        ShowImage();
    }

    private void ShowImage()
    {
        GetComponent<Image>().sprite = shirtManager.GetShirtById(CardShirtNumber);
    }

    public void GoToNextImage()
    {
        CardShirtNumber = ++CardShirtNumber % shirtManager.Count;
        ShowImage();
        PlayerPrefs.SetInt("CardShirtNumber", CardShirtNumber);
    }
}