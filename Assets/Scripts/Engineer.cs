using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Engineer : Character
{
    //Engineer specific changes and additions to Character base class

    [SerializeField] private GameObject debris;
    private int barrierIndex = 3;

    private void Awake()
    {
        //We want the engineer to be slower that the base speed
        speed = 1f;
    }

    // POLYMORPHISM
    // The engineer has to stay close to his site, so override the base MoveRoutine
    protected override IEnumerator MoveRoutine(Vector3 direction)
    {
        float distance = Vector3.Distance(transform.position, debris.transform.position);
        if (distance > 10)
        {
            //Debug.Log("Sending engineer back to site.");
            Vector3 newDirection = debris.transform.position - transform.position;
            direction = newDirection.normalized;
        }

        animController.SetFloat("Speed_f", 0.3f);
        yield return base.MoveRoutine(direction);
        animController.SetFloat("Speed_f", 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PickupType.Pickup == PickupTypes.Barrier)
        {
            
            GameManager.Instance.BarriersCollected++;
            player.DropOff();

            BarrierInstall();
        }
    }

    private void BarrierInstall()
    {
        barrierIndex++;
        if (barrierIndex < debris.transform.childCount)
        {
            debris.transform.GetChild(barrierIndex).gameObject.SetActive(true);
        }
        if (GameManager.Instance.BarriersCollected == 3)
        {
            //Debug.Log("Detected all barriers collected");

            // ABSTRACTION
            ObjectiveComplete();
        }
    }
}
