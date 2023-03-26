using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Farmer : Character
{

    protected override IEnumerator MoveRoutine(Vector3 direction)
    {
        animController.SetFloat("Speed_f", 0.4f);
        yield return base.MoveRoutine(direction);
        animController.SetFloat("Speed_f", 0f);
    }
}
