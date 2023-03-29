using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Druid : Character
{
    [SerializeField] private GameObject druidHuman;
    [SerializeField] private GameObject druidAnimal;

    [SerializeField] private ParticleSystem transformPoof;

    private void Awake()
    {
        speed = 8f;
    }

    protected override IEnumerator MoveRoutine(Vector3 direction)
    {
        animController.SetFloat("Speed_f", 0.6f);
        yield return base.MoveRoutine(direction);
        animController.SetFloat("Speed_f", 0f);
    }

    protected override void ObjectiveComplete()
    {
        StartCoroutine(TransformHuman());

        Instantiate(victoryBoom, transform.position, victoryBoom.transform.rotation);
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
            player.DropOff();
            GameManager.Instance.HerbsCollected++;
            moveEnabled = false;

            ObjectiveComplete();
        }
    }
}
