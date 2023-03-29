using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PickupTypes type;
    protected PlayerController player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.OnInteract += Collect;
    }

    protected void Collect()
    {
        if (PickupType.Pickup == PickupTypes.None && player.objectInRange == gameObject)
        {
            PickupType.Pickup = type;
            Debug.Log("Collected: " + gameObject.name);
            gameObject.SetActive(false);
        }
    }
}
