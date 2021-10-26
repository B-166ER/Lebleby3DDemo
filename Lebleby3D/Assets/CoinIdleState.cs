using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinIdleState : BaseState
{
    public override void EnterState(CoinBehaviour coin)
    {
        Debug.Log("coin idle enter state");
    }

    public override void OnCollisionEnter(CoinBehaviour coin)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(CoinBehaviour coin)
    {

        if (coin.rb2d.velocity.x != 0 || coin.rb2d.velocity.z != 0)
        {
            coin.SwitchState(coin.CoinMoving);
        }
            
    }
}
