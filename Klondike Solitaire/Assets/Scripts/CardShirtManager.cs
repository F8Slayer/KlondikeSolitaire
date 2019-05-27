using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardShirtManager", menuName = "GameResources/CardShirtManager")]

public class CardShirtManager : ScriptableObject
{
    public List<Sprite> Shirts = new List<Sprite>();

    public int Count { get { return Shirts.Count; } }

    public Sprite GetShirtById(int id)
    {
        return Shirts[id];
    }
}
