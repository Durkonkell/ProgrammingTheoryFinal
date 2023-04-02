using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Druid : Character
{
    [SerializeField] private GameObject druidHuman;
    [SerializeField] private GameObject druidAnimal;
    [SerializeField] private GameObject druidGrove;

    [SerializeField] private ParticleSystem transformPoof;

    private void Awake()
    {
        speed = 8f;
    }

    protected override IEnumerator MoveRoutine(Vector3 direction)
    {
        float distance = Vector3.Distance(transform.position, druidGrove.transform.position);
        if (distance > 10)
        {
            //Debug.Log("Sending druid back to grove.");
            Vector3 newDirection = druidGrove.transform.position - transform.position;
            direction = newDirection.normalized;
        }

        animController.SetFloat("Speed_f", 0.6f);
        yield return base.MoveRoutine(direction);
        animController.SetFloat("Speed_f", 0f);
    }

    protected override void ObjectiveComplete()
    {
        Instantiate(victoryBoom, transform.position, victoryBoom.transform.rotation);
    }

    public void TriggerTransformation()
    {
        StartCoroutine(TransformHuman());
    }

    IEnumerator TransformHuman()
    {
        moveEnabled = false;
        animController.SetBool("Eat_b", true);
        yield return new WaitForSeconds(5f);

        Instantiate(transformPoof, transform.position, transformPoof.transform.rotation);

        druidAnimal.SetActive(false);
        druidHuman.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PickupType.Pickup == PickupTypes.Herb)
        {
            
            GameManager.Instance.HerbsCollected++;
            player.DropOff();
            moveEnabled = false;

            ObjectiveComplete();
        }
    }
}
