using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("移動速度"), Range(1, 1000)]
    public float speed = 50;
    [Header("跳躍高度"), Range(10, 1000)]
    public float jump = 100;
    [Header("動畫參數：跳躍")]
    public string paraJump = "跳躍開關";
    [Header("動畫參數：死亡")]
    public string paraDead = "死亡觸發";
    [Header("地板標籤")]
    public string tagGround = "地板";
    [Header("中心點位移")]
    public Vector3 offset;
    [Header("地板偵測長度")]
    public float length = 0.1f;

    private Rigidbody2D rig;
    private Animator ani;
    public bool isGrounded
    {
        get
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + offset, transform.up, length);
            if (hit)
                return hit.collider.tag == tagGround;
            else
                return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + offset, transform.up * length);
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        Jump();
        Move();
    }

    private void Move()
    {
        ani.SetBool(paraJump, !isGrounded);
        transform.Translate(transform.right * speed * Time.deltaTime / 50);
    }

    private void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rig.AddForce(transform.up * jump);
            rig.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
