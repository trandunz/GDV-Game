using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CharacterMotor2D : MonoBehaviour
{
    [SerializeField] protected LayerMask m_GroundLayer;
    [SerializeField] protected float m_JumpPower;
    [SerializeField] protected float m_MovementSpeed;
    [SerializeField] protected float m_MaxJumpTime;

    protected Rigidbody2D m_RigidBody;

    Vector2 m_Velocity;
    float m_JumpTime = 0;
    bool m_IsJumping = false;

    protected void InitMotor()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    protected void Motor()
    {
        m_Velocity = Movement();
        m_Velocity *= m_MovementSpeed;
        m_RigidBody.velocity = new Vector2(m_Velocity.x, m_RigidBody.velocity.y);

        Jump();
    }

    #region Private Member Functions

    Vector2 Movement()
    {
        int x = 0;
        x = Input.GetKey(KeyCode.A) ? x -= 1 : x += 0;
        x = Input.GetKey(KeyCode.D) ? x += 1 : x += 0;

        Vector2 movement = new Vector2(x, 0);

        return movement.normalized;
    }

    void Jump()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            m_IsJumping = true;
            m_RigidBody.AddForce(Vector2.up * (m_JumpPower / 4) * m_RigidBody.mass, ForceMode2D.Impulse);
        }

        if (Input.GetKey(KeyCode.Space) && m_IsJumping)
        {
            if (m_JumpTime < m_MaxJumpTime)
            {
                m_RigidBody.velocity += new Vector2(0, m_JumpPower * Time.deltaTime);
                m_JumpTime += Time.deltaTime;
            }
            else
            {
                m_JumpTime = 0;
                m_IsJumping = false;
            }
        }
        else
        {
            m_IsJumping = false;
            m_JumpTime = 0;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(transform.position + new Vector3(0, -0.3f, 0), 0.25f, m_GroundLayer);
    }

    #endregion
}