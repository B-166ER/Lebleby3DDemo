using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public BaseState currentState;
    public CoinIdleState CoinIdle = new CoinIdleState();
    public CoinMovingState CoinMoving = new CoinMovingState();
    private void Start()
    {
    }
    private void Update()
    {
    }

}
