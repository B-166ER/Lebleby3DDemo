using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    private void OnEnable()
    {
        instance = this;
    }

    public List<GameObject> AllCoins = new List<GameObject>();
    public List<GameObject> findNotMeCoins(CoinBehaviour cb)
    {
        List<GameObject> otherCoins = new List<GameObject>();
        foreach (var eachCoin in AllCoins)
        {
            if (eachCoin.GetComponent<CoinBehaviour>() != cb)
            {
                otherCoins.Add(eachCoin);
            }
        }
        return otherCoins;
    }
}
