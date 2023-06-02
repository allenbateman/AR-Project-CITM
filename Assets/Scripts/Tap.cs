using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour
{
    #region Public Var
    public GameObject previewParticle;
    #endregion

    #region Raycast Var
    private RaycastHit hit;
    private bool hasHit;
    #endregion

    public Action<Vector3> hitAction;

    void Start()
    {
        previewParticle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = Vector3.zero;

        if (Input.touchCount != 1)
        {
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        Ray raycast = Camera.main.ScreenPointToRay(pos);
        hasHit = Physics.Raycast(raycast, out hit);

        if (touch.phase == TouchPhase.Began)
        {
            if (hasHit)
            {
                if (hit.collider.tag == "Ground")
                {
                    previewParticle.SetActive(true);
                    Debug.Log("--- Touch began " + hit.transform.tag + " ---");
                    Debug.Log("TouchPhase Began" + hit.point);
                }
            }
        }

        if (touch.phase == TouchPhase.Moved)
        {
            if (hasHit)
            {
                if (hit.collider.tag == "Ground")
                {
                    transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z); 
                }
            }
        }

        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            Debug.Log("TouchPhase Ended");
            previewParticle.SetActive(false);

            SendEvent();
        }
    }

    void SendEvent()
    {
        hitAction?.Invoke(hit.point);
    }
}