using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovingState : BaseState
{
    public override void EnterState(CoinBehaviour coin)
    {
        Debug.Log("coin moving state enter");
    }

    public override void OnCollisionEnter(CoinBehaviour coin)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(CoinBehaviour coin)
    {
        if (coin.rb2d.velocity.x == 0 || coin.rb2d.velocity.z == 0)
        {
            coin.IStoped();
            coin.SwitchState(coin.CoinIdle);
        }

        RaycastHit[] hits;
        List<GameObject> others = CoinManager.instance.findNotMeCoins(coin);
        hits = Physics.RaycastAll(others[0].transform.position,
                                    others[1].transform.position - others[0].transform.position,
                                    (others[1].transform.position - others[0].transform.position).magnitude);

        Debug.DrawRay(others[0].transform.position, others[1].transform.position - others[0].transform.position, Color.black);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider.gameObject.GetComponent<CoinBehaviour>() == coin)
            {
                coin.PassedTheLine = true;
            }
        }
    }
}
