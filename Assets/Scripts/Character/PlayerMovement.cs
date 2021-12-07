using System;
using Assets.HeroEditor4D.Common.CharacterScripts;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    Character4D character;
    Rigidbody2D rb;

    bool m_Moving;

    void Awake()
    {
        character = GetComponent<Character4D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        character.AnimationManager.SetState(CharacterState.Idle);
        character.SetDirection(Vector2.down);
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        SetDirection();
        Move();
        //ChangeState();
    }
    
    private void SetDirection()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input = input.normalized;

        if (input != Vector2.zero)
        {
            try {
                character.SetDirection(input);
            } 
            catch (Exception)
            {
                //Do nothing
            }
        }
    }
    
    private void Move()
    {
        if (moveSpeed == 0) return;

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        input = input.normalized;

        if (input == Vector3.zero)
        {
            if (m_Moving)
            {
                character.AnimationManager.SetState(CharacterState.Idle);
                m_Moving = false;
            }
        }
        else
        {
            if (!m_Moving)
            {
                character.AnimationManager.SetState(CharacterState.Run);
            }
            input = input * moveSpeed * Time.deltaTime;
            rb.MovePosition(rb.transform.position + input);
            m_Moving = true;
        }
    }
    
    /*private void ChangeState()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            character.AnimationManager.SetState(CharacterState.Idle);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            character.AnimationManager.SetState(CharacterState.Ready);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            character.AnimationManager.SetState(CharacterState.Walk);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            character.AnimationManager.SetState(CharacterState.Run);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            character.AnimationManager.SetState(CharacterState.Jump);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            character.AnimationManager.SetState(CharacterState.Climb);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            character.AnimationManager.Die();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            character.AnimationManager.Hit();
        }
    }*/
}
