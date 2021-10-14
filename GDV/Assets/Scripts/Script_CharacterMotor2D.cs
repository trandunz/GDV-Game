using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CharacterMotor2D : MonoBehaviour
{
    private Rigidbody2D m_RigidBody;
    private Vector2 m_Velocity;

    [SerializeField] private float TESTVARIABLE;

    [SerializeField] private LayerMask m_GroundLayer;

    [SerializeField] private float m_JumpPower;
    [SerializeField] private float m_MovementSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Velocity = Movement();
        m_Velocity *= m_MovementSpeed;
        m_RigidBody.velocity = new Vector2(m_Velocity.x, m_RigidBody.velocity.y);

        Jump();
    }

    private Vector2 Movement()
    {
        int x = 0;
        x = Input.GetKey(KeyCode.A) ? x -= 1 : x += 0;
        x = Input.GetKey(KeyCode.D) ? x += 1 : x += 0;

        Vector2 movement = new Vector2(x, 0);

        return movement.normalized;
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            m_RigidBody.AddForce(Vector2.up * m_JumpPower * m_RigidBody.mass, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(transform.position + new Vector3(0, -0.3f, 0), 0.25f, m_GroundLayer);
    }
}