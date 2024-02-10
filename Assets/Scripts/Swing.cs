using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Swing : MonoBehaviour
{
    public Transform startSwingHand;
    public float maxDistance = 35f;
    public LayerMask swingableLayer;
    public Transform predictionPoint;
    public Rigidbody playerRigidbody;
    public InputActionProperty swingAction;
    public LineRenderer lineRenderer;
    public InputActionProperty pullAction;
    public float pullingStrength = 500f;
    public AudioManager audioManager;
    private bool hasHit;
    private SpringJoint joint;
    private Vector3 swingPoint;

    void Update()
    {
        GetSwingPoint();

        if(swingAction.action.WasPressedThisFrame())
        {
            StartSwing();
        }
        else if(swingAction.action.WasReleasedThisFrame())
        {
            StopSwing();
        }

        PullRope();
        DrawRope();
    }
    public void PullRope()
    {
        if (!joint)
        {
            return;
        }
        if(pullAction.action.IsPressed() )
        {
            Vector3 direction = (swingPoint - startSwingHand.position).normalized;
            playerRigidbody.AddForce(direction * pullingStrength * Time.deltaTime);

            float distance = Vector3.Distance(playerRigidbody.position, swingPoint);
            joint.maxDistance = distance;
        }
    }
    public void StartSwing()
    {
        if(hasHit)
        {
            joint = playerRigidbody.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distance = Vector3.Distance(playerRigidbody.position, swingPoint);
            joint.maxDistance = distance;

            joint.spring = 4.5f;
            joint.damper = 7;
            joint.massScale = 4.5f;
            audioManager.Play("Swing");
        }
    }
    public void StopSwing()
    {
        audioManager.Stop("Swing");
        Destroy(joint);
    }
    public void GetSwingPoint()
    {
        if (joint)
        {
            predictionPoint.gameObject.SetActive(false);
            return;
        }

        RaycastHit raycastHit;

        hasHit = Physics.Raycast(startSwingHand.position,startSwingHand.forward,out raycastHit,maxDistance,swingableLayer);

        if (hasHit)
        {
            swingPoint = raycastHit.point;
            predictionPoint.gameObject.SetActive(true);
            predictionPoint.position = swingPoint;
        }
        else
        {
            predictionPoint.gameObject.SetActive(false);
        }
    }
    public void DrawRope()
    {
        if (!joint)
        {
            lineRenderer.enabled = false;
        }
        else
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0,startSwingHand.position);
            lineRenderer.SetPosition(1, swingPoint);
        }
    }
}
