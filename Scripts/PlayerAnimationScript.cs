using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{

    private Animator animator;



    //animation status
    const string PLAYER_IDLE = "Idle";
    const string WALK_BACK = "WalkBack";
    const string WALK_BACK_LEFT = "WalkBackLeft";
    const string WALK_BACK_RIGHT = "WalkBackRight";
    const string WALK_FRONT = "WalkFront";
    const string WALK_FRONT_LEFT = "WalkFrontLeft";
    const string WALK_FRONT_RIGHT = "WalkFrontRight";


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent <Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovmentAnimation(Vector2 movment)
    {
        

        if (movment.x == 0 && movment.y == 0)
        {
            animator.Play(PLAYER_IDLE);
        }else
        if (movment.x == 0 && movment.y > 0)
        {
            animator.Play(WALK_BACK);
        }else
        if (movment.x == 0 && movment.y < 0)
        {
            animator.Play(WALK_FRONT);
        } else

        if (movment.x > 0 && movment.y > 0)
        {
            animator.Play(WALK_BACK_RIGHT);
        }else
        if (movment.x > 0 && movment.y < 0)
        {
            animator.Play(WALK_FRONT_RIGHT);
        }else
        if (movment.x < 0 && movment.y > 0)
        {
            animator.Play(WALK_BACK_LEFT);
        }else
        if (movment.x < 0 && movment.y < 0)
        {
            animator.Play(WALK_FRONT_LEFT);
        }else
             if (movment.x <0 && movment.y == 0)
        {
            animator.Play(WALK_FRONT_LEFT);
        }else
             if (movment.x > 0 && movment.y == 0)
        {
            animator.Play(WALK_FRONT_RIGHT);
        }

    }
}
