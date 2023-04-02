using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private float baseSpeed = 6;
    private Animator animController;

    public event Action OnInteract;

    public GameObject invChicken;
    public GameObject invBarrier;
    public GameObject invHerb;

    // ENCAPSULATION
    public GameObject objectInRange { get; private set; } = null;

    private void Awake()
    {
        invChicken = transform.GetChild(5).gameObject;
        invBarrier = transform.GetChild(6).gameObject;
        invHerb = transform.GetChild(7).gameObject;
    }

    private void Start()
    {
        animController = GetComponent<Animator>();
    }


    void Update()
    {
        Move();
        Interact();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && OnInteract != null && objectInRange != null)
        {
            OnInteract();

            switch (PickupType.Pickup)
            {
                case PickupTypes.Chicken:
                    //Debug.Log("Picked up chicken");
                    objectInRange = null;
                    invChicken.SetActive(true);
                    animController.SetInteger("WeaponType_int", 1);
                    break;
                case PickupTypes.Barrier:
                    //Debug.Log("Picked up barrier");
                    objectInRange = null;
                    invBarrier.SetActive(true);
                    animController.SetInteger("WeaponType_int", 1);
                    break;
                case PickupTypes.Herb:
                    //Debug.Log("Picked up herb");
                    objectInRange = null;
                    invHerb.SetActive(true);
                    animController.SetInteger("WeaponType_int", 1);
                    break;
                default:
                    //Debug.Log("No pickup found");
                    break;
            }
        }
    }

    public void DropOff()
    {
        invChicken.SetActive(false);
        invBarrier.SetActive(false);
        invHerb.SetActive(false);

        PickupType.Pickup = PickupTypes.None;
        animController.SetInteger("WeaponType_int", 0);

        DialogueManager.DialogueInterface.UpdateInkVariables();
    }

    void Move()
    {
        float speed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = baseSpeed * 2;
        }
        else
        {
            speed = baseSpeed + 1;
        }

        float vert = Input.GetAxis("Vertical");
        float horz = Input.GetAxis("Horizontal");

        Vector3 course = new Vector3(horz, 0, vert);

        transform.Translate(course * Time.deltaTime * speed, Space.World);

        if (course != Vector3.zero)
        {
            animController.SetFloat("Speed_f", 1);
            transform.rotation = Quaternion.LookRotation(course);
        }
        else
        {
            animController.SetFloat("Speed_f", 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup") || other.CompareTag("Character"))
        {
            objectInRange = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        objectInRange = null;
    }
}
