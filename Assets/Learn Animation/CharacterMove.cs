using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float Speed = 3f;
    public float TurnSpeed = 10f;

    private Vector3 move;

    Animator anim;

    Rigidbody rigid;

    float forwardAmount;
    float turnAmount;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = new Vector3(x, 0, z) * Speed;
        Vector3 localMove = transform.InverseTransformVector(move);

        forwardAmount = localMove.z;
        turnAmount = Mathf.Atan2(localMove.x, localMove.z);

        UpdateAnim();
    }

    private void FixedUpdate()
    {
        rigid.velocity = forwardAmount * transform.forward * Speed;
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(0, turnAmount * TurnSpeed, 0));
    }

    void UpdateAnim()
    {
        anim.SetFloat("Turn", turnAmount);
        anim.SetFloat("Forward", forwardAmount);
    }
}
