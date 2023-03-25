using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Druid : Character
{
    [SerializeField]private GameObject druidHuman;
    [SerializeField]private GameObject druidAnimal;



    protected override IEnumerator MoveRoutine(Vector3 direction)
    {
        animController.SetFloat("Speed_f", 0.6f);
        yield return base.MoveRoutine(direction);
        animController.SetFloat("Speed_f", 0f);
    }

    IEnumerator TransformHuman()
    {
        animController.SetBool("Eat_b", true);
        yield return new WaitForSeconds(5f);

        druidAnimal.SetActive(false);
        druidHuman.SetActive(true);
    }
}
