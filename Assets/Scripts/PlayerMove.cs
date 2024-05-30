using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Tilemaps;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D body;
    Animator animator;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    public float inertia;

    public float runSpeed = 20.0f;

    bool canMove = true;

    public bool isMoving = false;

    public GameObject[] meleeColliders;

    public int activeCollider;

    public TileBase tb;
    public Tilemap tm;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        animator.SetFloat("hor", 0);
        animator.SetFloat("ver", -1); //tell animator character is facing down at level start
    }

    void Update()
    {
        // Get axis float values from player inputs
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        // Swing pickaxe
        if(Input.GetKeyUp(KeyCode.Space) && canMove) 
        {
            animator.SetTrigger("pickaxeSwing");

            StartCoroutine(CheckPickaxeHit());

            StartCoroutine(EnablePlayerMove());

            canMove = false; // temporarily disable player movement

        }

        UpdateAnimator();

        isMoving = (horizontal != 0 || vertical != 0);
        animator.SetBool("isMoving", isMoving);
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if (horizontal != 0 && vertical != 0) // Check for diagonal movement
            {
                // limit movement speed diagonally, so you move at 70% speed
                horizontal *= moveLimiter;
                vertical *= moveLimiter;
            }

            Vector2 newVelocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

            body.velocity = Vector2.Lerp(body.velocity, newVelocity, inertia); // slowly build up speed 
        }
        else
        {
            body.velocity = Vector2.zero; // ...and slowly lose speed
        }
    }

    void UpdateAnimator()
    {
        if (canMove)
        {
            if (horizontal != 0)
            {
                animator.SetFloat("hor", horizontal);
                animator.SetFloat("ver", 0);

                if (horizontal > 0)
                {
                    activeCollider = 1;
                }
                else activeCollider = 3;
            }

            if (vertical != 0)
            {
                animator.SetFloat("ver", vertical);
                animator.SetFloat("hor", 0);

                if (vertical > 0)
                {
                    activeCollider = 0;
                }
                else activeCollider = 2;
            }

            if (horizontal == 0 && vertical == 0)
            {
                animator.Play(0, 0);
            }
        }
    }

    IEnumerator CheckPickaxeHit()
    {
        // convert active melee collider world position to cell position, store as variable
        Vector3 p = meleeColliders[activeCollider].transform.position;
        Vector3 o = new Vector3(0.5f, -0.5f, 0f);

        Vector3Int meleePos = tm.WorldToCell(p + o);

        yield return new WaitForSeconds(0.25f); // sync tile creation with pickaxe strike anim
        // if no tile at melee position, create one
        if(!tm.GetTile(meleePos))
        {
            tm.SetTile(meleePos, tb);
        }
    }

    IEnumerator EnablePlayerMove()
    {
        var anim = animator.GetCurrentAnimatorClipInfo(0);
        float animLength = anim[0].clip.length + 0.25f;
        yield return new WaitForSeconds(animLength);

        canMove = true;
    }
}
