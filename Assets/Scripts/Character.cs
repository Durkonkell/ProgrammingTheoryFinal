using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private bool moveWait = false;
    protected bool moveEnabled = true;
    protected Animator animController;
    protected PlayerController player;

    protected float speed = 5f;

    [SerializeField] protected ParticleSystem victoryBoom;


    // Start is called before the first frame update
    void Start()
    {
        animController = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.OnInteract += ConversationStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveWait && moveEnabled)
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
        if (diceroll > 3 && moveEnabled)
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
            transform.Translate(speed * Time.deltaTime * direction, Space.World);
            yield return new WaitForFixedUpdate();
        }
    }

    protected void ConversationStart()
    {
        if (player.objectInRange == gameObject)
        {
            DialogueManager.DialogueInterface.DisplayDialogue(gameObject.name);
            //Debug.Log("Attempted to start a conversation with " + gameObject.name);
        }
    }

    protected virtual void ObjectiveComplete()
    {
        //Debug.Log("Objective Complete called: " + gameObject.name);
        animController.SetBool("Jump_b", true);

        Instantiate(victoryBoom, transform.position, victoryBoom.transform.rotation);
    }

}
