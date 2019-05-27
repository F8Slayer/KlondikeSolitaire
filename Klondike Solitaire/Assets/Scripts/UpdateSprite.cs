using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSprite : MonoBehaviour
{
    public CardShirtManager shirtManager = null;
    public Sprite cardFace;
    public Sprite cardBack;
    private SpriteRenderer spriteRenderer;
    private Selectable selectable;
    private Solitaire solitaire;
    private UserInput userInput;
    
    void Start()
    {
        if (PlayerPrefs.HasKey("CardShirtNumber"))
        {
            cardBack = shirtManager.GetShirtById(PlayerPrefs.GetInt("CardShirtNumber"));
        }
        else
        {
            cardBack = shirtManager.GetShirtById(0);
        }

        List<string> deck = Solitaire.GenerateDeck();
        solitaire = FindObjectOfType<Solitaire>();
        userInput = FindObjectOfType<UserInput>();

        int i = 0;
        foreach (string card in deck)
        {
            if (this.name == card)
            {
                cardFace = solitaire.cardFaces[i];
                break;
            }
            i++;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectable = GetComponent<Selectable>();
    }

    void Update()
    {
        if (selectable.faceUp == true)
        {
            spriteRenderer.sprite = cardFace;
        }
        if (selectable.faceUp == false)
        {
            spriteRenderer.sprite = cardBack;
        }

        if (userInput.slot1)
        {
            if (name == userInput.slot1.name)
            {
                spriteRenderer.color = Color.yellow;
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
        }
    }
}
