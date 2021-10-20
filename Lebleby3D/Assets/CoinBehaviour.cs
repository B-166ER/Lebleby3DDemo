using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    Rigidbody rb2d;
    bool isBeingHeldDown;
    Vector3 mouseStartPosition;
    Vector3 mouseEndPosition;
    public bool iAmMoving = false;
    [SerializeField] int deadZoneSize;

    [SerializeField] LineRenderer lineR;
    public int Factor;
    public Vector3 diff;
    public Vector3 invertedDiff;
    RaycastHit[] hits;
    public float LineMultiplier;
    public float CubePushForce;

    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody>();
        lineR = gameObject.GetComponent<LineRenderer>();
    }

    private void OnMouseDown()
    {
        isBeingHeldDown = true;
        lineR.enabled = true;
        mouseStartPosition = Input.mousePosition;
    }

    [SerializeField] float multiplier;
    Vector3 finalPushForce;
    private void OnMouseUp()
    {
        lineR.enabled = false;
        mouseEndPosition = Input.mousePosition;
        if (isBeingHeldDown)
        {
            if (Mathf.Abs(diff.x) > deadZoneSize || Mathf.Abs(diff.z) > deadZoneSize)
            {
                finalPushForce = mouseStartPosition - mouseEndPosition *  multiplier;
                rb2d.AddForce(invertedDiff.normalized * finalPushForce.magnitude);
                iAmMoving = true;
            }
        }

        isBeingHeldDown = false;

    }

    void Update()
    {

        if (iAmMoving)
        {
            List<GameObject> others = CoinManager.instance.findNotMeCoins(this);
            hits = Physics.RaycastAll(others[0].transform.position,
                                        others[1].transform.position - others[0].transform.position,
                                        (others[1].transform.position - others[0].transform.position).magnitude);
            
            Debug.DrawRay(others[0].transform.position, others[1].transform.position - others[0].transform.position,Color.black);
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                if (hit.collider.gameObject.GetComponent<CoinBehaviour>() == this)
                {
                    Debug.LogError("I AM PASSING THE RIGHT LINE");
                }
            }
        }

        if (isBeingHeldDown)
        {
            //vector3 diff
            diff = Input.mousePosition - mouseStartPosition;
            //switch for 3d
            diff.z = diff.y;
            diff.y = 0;
            Debug.DrawRay(gameObject.transform.position, gameObject.transform.position + diff);
            //Vector3 invertedDiff;
            invertedDiff.x = diff.x * -1;
            invertedDiff.z = diff.z * -1;
            Debug.DrawRay(gameObject.transform.position, gameObject.transform.position + invertedDiff, Color.red);

            //line renderer
            Vector3 pos = gameObject.transform.position;
            pos.y = 0;
            lineR.SetPosition(0, pos);
            lineR.SetPosition(1, (pos+ invertedDiff.normalized * LineMultiplier) );
        }

        if (rb2d.velocity.x == 0 && rb2d.velocity.z == 0)
            iAmMoving = false;
        else
            iAmMoving = true;
    }
}
