using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int ChickensCollected = 0;
    public int BarriersCollected = 0;
    public int HerbsCollected = 0;

    void Start()
    {
        Instance = this;
    }
}
