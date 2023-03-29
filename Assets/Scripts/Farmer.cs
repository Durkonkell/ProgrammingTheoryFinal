using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : Character
{
    private int chickenIndex = 4;

    protected override IEnumerator MoveRoutine(Vector3 direction)
    {
        animController.SetFloat("Speed_f", 0.4f);
        yield return base.MoveRoutine(direction);
        animController.SetFloat("Speed_f", 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PickupType.Pickup == PickupTypes.Chicken)
        {
            player.DropOff();
            GameManager.Instance.ChickensCollected++;
            moveEnabled = false;
            ActivateChicken();
        }
    }

    private void ActivateChicken()
    {
        chickenIndex++;
        if (chickenIndex < transform.childCount)
        {
            transform.GetChild(chickenIndex).gameObject.SetActive(true);
        }
        if (GameManager.Instance.ChickensCollected == 3)
        {
            Debug.Log("Detected all chickens collected");
            ObjectiveComplete();
        }
    }
}
