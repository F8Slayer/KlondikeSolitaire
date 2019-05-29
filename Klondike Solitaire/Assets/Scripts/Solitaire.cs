﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Solitaire : MonoBehaviour
{
    public static Toggle toggle_1;
    public static Toggle toggle_3;
    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public GameObject deckButton;
    public GameObject[] bottomPos;
    public GameObject[] topPos;
    public List<string> tripsOnDisplay = new List<string>();
    public List<List<string>> deckTrips = new List<List<string>>();

    public static string[] suits = new string[] { "C", "D", "H", "S" };
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    public List<string>[] bottoms;
    public List<string>[] tops;
    public CardShirtManager shirtManager = null;

    private List<string> bottom0 = new List<string>();
    private List<string> bottom1 = new List<string>();
    private List<string> bottom2 = new List<string>();
    private List<string> bottom3 = new List<string>();
    private List<string> bottom4 = new List<string>();
    private List<string> bottom5 = new List<string>();
    private List<string> bottom6 = new List<string>();

    public List<string> deck;
    public List<string> discardPile = new List<string>();
    private int deckLocation;
    public int trips;
    public int tripsRemainder;

    public List<Sprite> cardbacks;

    void Start()
    {
        deckButton.GetComponent<SpriteRenderer>().sprite = shirtManager.GetShirtById(PlayerPrefs.HasKey("CardShirtNumber") ? PlayerPrefs.GetInt("CardShirtNumber") : 0);

        bottoms = new List<string>[] { bottom0, bottom1, bottom2, bottom3, bottom4, bottom5, bottom6 };

        PlayCards();
    }

    public void PlayCards()
    {
        foreach (List<string> list in bottoms)
        {
            list.Clear();
        }
        deck = GenerateDeck();
        Shuffle(deck);
        //test the cards in the deck
        foreach (string card in deck)
        {
            print(card);
        }
        SolitaireSort();
        StartCoroutine(SolitaireDeal());
        SortDeckIntoTrips();
    }

    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach (string s in suits)
        {
            foreach (string v in values)
            {
                newDeck.Add(s + v);
            }
        }
        return newDeck;
    }

    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    IEnumerator SolitaireDeal()
    {
        for (int i = 0; i < 7; i++)
        {
            float yOffset = 0;
            float zOffset = 0.03f;
            foreach (string card in bottoms[i])
            {
                yield return new WaitForSeconds(0.10f);
                GameObject newCard = Instantiate(cardPrefab, new Vector3(bottomPos[i].transform.position.x, bottomPos[i].transform.position.y - yOffset, bottomPos[i].transform.position.z - zOffset), Quaternion.identity, bottomPos[i].transform);

                newCard.name = card;
                newCard.GetComponent<Selectable>().row = i;
                if (card == bottoms[i][bottoms[i].Count - 1])
                {
                    newCard.GetComponent<Selectable>().faceUp = true;
                }
                else
                {
                    newCard.GetComponent<Selectable>().faceUp = false;
                }

                yOffset = yOffset + 4f;
                zOffset = zOffset + 1f;
                discardPile.Add(card);
            }
        }

        foreach (string card in discardPile)
        {
            if (deck.Contains(card))
            {
                deck.Remove(card);
            }
        }
        discardPile.Clear();
    }

    void SolitaireSort()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = i; j < 7; j++)
            {
                bottoms[j].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count - 1);
            }
        }
    }

    public void SortDeckIntoTrips()
    {

        if (Data.val == 1)
        {
            trips = deck.Count / 3;
            tripsRemainder = deck.Count % 3;
            deckTrips.Clear();

            int modifier = 0;
            for (int i = 0; i < trips; i++)
            {
                List<string> myTrips = new List<string>();
                for (int j = 0; j < 3; j++)
                {
                    myTrips.Add(deck[j + modifier]);
                }
                deckTrips.Add(myTrips);
                modifier = modifier + 3;
            }
            if (tripsRemainder != 0)
            {
                List<string> myRemainders = new List<string>();
                modifier = 0;
                for (int k = 0; k < tripsRemainder; k++)
                {
                    myRemainders.Add(deck[deck.Count - tripsRemainder + modifier]);
                    modifier++;
                }
                deckTrips.Add(myRemainders);
                trips++;
            }
            deckLocation = 0;
        }
        if (Data.val == 2)
        {
            trips = deck.Count / 1;
            tripsRemainder = deck.Count % 1;
            deckTrips.Clear();

            int modifier = 0;
            for (int i = 0; i < trips; i++)
            {
                List<string> myTrips = new List<string>();
                for (int j = 0; j < 1; j++)
                {
                    myTrips.Add(deck[j + modifier]);
                }
                deckTrips.Add(myTrips);
                modifier = modifier + 1;
            }
            if (tripsRemainder != 0)
            {
                List<string> myRemainders = new List<string>();
                modifier = 0;
                for (int k = 0; k < tripsRemainder; k++)
                {
                    myRemainders.Add(deck[deck.Count - tripsRemainder + modifier]);
                    modifier++;
                }
                deckTrips.Add(myRemainders);
                trips++;
            }
            deckLocation = 0;
        }
    }

    public void DealFromDeck()
    {
        // add remaining card to discard pile

        foreach (Transform child in deckButton.transform)
        {
            if (child.CompareTag("Card"))
            {
                deck.Remove(child.name);
                discardPile.Add(child.name);
                Destroy(child.gameObject);
            }
        }

        if (deckLocation < trips)
        {
            //draw 3 new cards
            tripsOnDisplay.Clear();
            float xOffset = 25f;
            float zOffset = -0.2f;

            foreach (string card in deckTrips[deckLocation])
            {
                GameObject newTopCard = Instantiate(cardPrefab, new Vector3(deckButton.transform.position.x + xOffset, deckButton.transform.position.y, deckButton.transform.position.z + zOffset), Quaternion.identity, deckButton.transform);
                xOffset = xOffset + 7f;
                zOffset = zOffset - 0.2f;
                newTopCard.name = card;
                tripsOnDisplay.Add(card);
                newTopCard.GetComponent<Selectable>().faceUp = true;
                newTopCard.GetComponent<Selectable>().inDeckPile = true;
            }
            deckLocation++;
        }
        else
        {
            //Restack the top deck
            RestackTopDeck();
        }
    }

    void RestackTopDeck()
    {
        deck.Clear();
        foreach (string card in discardPile)
        {
            deck.Add(card);
        }
        discardPile.Clear();
        SortDeckIntoTrips();
    }

}
