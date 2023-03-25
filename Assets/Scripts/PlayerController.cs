using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float baseSpeed = 5;
    private Animator animController;

    private void Start()
    {
        animController = GetComponent<Animator>();
    }


    void Update()
    {
        Move();
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

}
