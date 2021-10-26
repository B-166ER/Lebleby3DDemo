using UnityEngine;

public abstract class BaseState
{
    public abstract void EnterState(CoinBehaviour coin);
    public abstract void UpdateState(CoinBehaviour coin);
    public abstract void OnCollisionEnter(CoinBehaviour coin);
}
