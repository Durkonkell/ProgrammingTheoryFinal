using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Pickup
{
    private bool moveWait = false;
    private float speed = 3.5f;

    private Animator animController;

    private void Start()
    {
        animController = GetComponent<Animator>();
    }

    void Update()
    {
        if (!moveWait)
        {
            StartCoroutine(Wander());
        }
    }

    private IEnumerator Wander()
    {
        moveWait = true;
        float delay = Random.Range(3f, 7f);
        yield return new WaitForSeconds(delay);
        int diceroll = Random.Range(1, 7);
        //Debug.Log("Dice Roll: " + diceroll);
        if (diceroll > 3)
        {
            Vector3 direction = RandomDirection();
            StartCoroutine(MoveRoutine(direction));
        }
        else if (diceroll == 3)
        {
            StartCoroutine(Peck());
        }
        moveWait = false;
    }

    private Vector3 RandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);

        Vector3 direction = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), 0, Mathf.Sin(randomAngle * Mathf.Deg2Rad));

        return direction;
    }

    private IEnumerator MoveRoutine(Vector3 direction)
    {
        animController.SetFloat("Speed_f", 0.6f);
        int framesToMove = Random.Range(20, 80);
        Vector3 lookDir = new Vector3(direction.x, 0, direction.z);
        transform.rotation = Quaternion.LookRotation(lookDir);

        for (int i = 0; i < framesToMove; i++)
        {
            transform.Translate(direction * Time.deltaTime * speed, Space.World);
            yield return new WaitForFixedUpdate();
        }
        animController.SetFloat("Speed_f", 0f);
    }

    private IEnumerator Peck()
    {
        animController.SetBool("Eat_b", true);
        yield return new WaitForSeconds(2);
        animController.SetBool("Eat_b", false);
    }
}
