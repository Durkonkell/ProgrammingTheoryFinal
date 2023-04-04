using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Destroy the tutorial text panel after ten seconds.
    void Start()
    {
        StartCoroutine(TutorialTimer());
    }

    IEnumerator TutorialTimer()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
