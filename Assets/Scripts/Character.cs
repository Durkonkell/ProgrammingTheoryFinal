using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private bool moveWait = false;
    protected Animator animController;
    protected virtual float speed { get; set; } = 5;

    // Start is called before the first frame update
    void Start()
    {
        animController = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!moveWait)
        {
            StartCoroutine(Wander());
        }
    }

    protected virtual IEnumerator Wander()
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
        moveWait = false;
    }

    protected Vector3 RandomDirection()
    {
        float randomAngle = Random.Range(0f, 360f);

        Vector3 direction = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), 0, Mathf.Sin(randomAngle * Mathf.Deg2Rad));

        return direction;
    }

    protected virtual IEnumerator MoveRoutine(Vector3 direction)
    {
        int framesToMove = Random.Range(20, 80);
        Vector3 lookDir = new Vector3(direction.x, 0, direction.z);
        transform.rotation = Quaternion.LookRotation(lookDir);

        for (int i = 0; i < framesToMove; i++)
        {
            transform.Translate(direction * Time.deltaTime * speed, Space.World);
            yield return new WaitForFixedUpdate();
        }
    }

    protected virtual void ObjectiveComplete()
    {

    }

}
