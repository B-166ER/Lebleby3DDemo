﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinBehaviour : StateManager
{
    public Rigidbody rb2d;
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
    public bool PassedTheLine = false;

    public void SwitchState(BaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody>();
        lineR = gameObject.GetComponent<LineRenderer>();


        currentState = CoinIdle;
        currentState.EnterState(this);
    }

    private void OnMouseDown()
    {
        isBeingHeldDown = true;
        lineR.enabled = true;
        mouseStartPosition = Input.mousePosition;
    }

    [SerializeField] float multiplier;
    Vector3 MouseVector;
    [SerializeField] float finalFactorMax;
    private void OnMouseUp()
    {
        lineR.enabled = false;
        PassedTheLine = false;
        mouseEndPosition = Input.mousePosition;
        if (isBeingHeldDown)
        {
            if (Mathf.Abs(diff.x) > deadZoneSize || Mathf.Abs(diff.z) > deadZoneSize)
            {
                MouseVector = (mouseStartPosition - mouseEndPosition) *  multiplier;
                float finalFactor = MouseVector.magnitude;
                if (finalFactor > finalFactorMax)
                    finalFactor = finalFactorMax;
                rb2d.AddForce(invertedDiff * finalFactor);
                //SwitchState(CoinMoving);
            }
        }

        isBeingHeldDown = false;

    }

    void Update()
    {
        currentState.UpdateState(this);

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
            lineR.SetPosition(1, pos + invertedDiff * LineMultiplier );
        }

        /*
        if (rb2d.velocity.x == 0 && rb2d.velocity.z == 0)
        {
            if (iAmMoving == true)
                IStoped();
            iAmMoving = false;
        }
        else
            iAmMoving = true;
        */
    }
    public void IStoped()
    {
        if (!PassedTheLine)
        {
            Debug.Log("stopped but didnt passed the line");

        }else if (PassedTheLine)
        {
            Debug.Log("stopped and passed the line");
        }
        //lose condition reload scene
        //if ( PassedTheLine == false )
            //SceneManager.LoadScene("SampleScene");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GoalPostBehaviour>() != null && PassedTheLine)
        {
            Debug.Log("GOAL");
        }
    }
}
