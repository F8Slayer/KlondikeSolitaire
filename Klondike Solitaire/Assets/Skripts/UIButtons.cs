using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    int i = 0;
    public void ResetScene()
    {
        // find all the cards and remove them
        UpdateSprite[] cards = FindObjectsOfType<UpdateSprite>();
        print((i++).ToString());
        foreach (UpdateSprite card in cards)
        {
            Destroy(card.gameObject);
        }
        ClearTopValues();
        print((i++).ToString());
        // deal new cards
        FindObjectOfType<Solitaire>().PlayCards();
        print((i++).ToString());
    }

    void ClearTopValues()
    {
        Selectable[] selectables = FindObjectsOfType<Selectable>();
        print((i++).ToString());
        foreach (Selectable selectable in selectables)
        {
            if (selectable.CompareTag("Top"))
            {
                selectable.suit = null;
                print((i++).ToString());
                selectable.value = 0;
                print((i++).ToString());
            }
        }
    }

}
