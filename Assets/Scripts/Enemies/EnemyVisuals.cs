using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyVisuals : MonoBehaviour
{
    [SerializeField] List<EnemyVisualsEntry> enemyVisuals ;
    Animator animator;
    SpriteRenderer sr;
    Vector2 lastPosition;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    public void SetSprite(EnemyType type)
    {
        foreach(EnemyVisualsEntry enemy in enemyVisuals)
        {
            if(enemy.Type == type)
            {
                print(animator);
                print(enemy.Animator);
                animator.runtimeAnimatorController =enemy.Animator;
                print (animator);
            }
        }
    }
    private void LateUpdate()
    {
        Vector2 delta = (Vector2)transform.position - lastPosition;
        if (delta.x > 0) sr.flipX = true;
        else sr.flipX = false;
        if(Mathf.Abs(delta.x)>Mathf.Abs(delta.y))
        {
            animator.Play("SideWalk");
        }
        else if (delta.y > 0)
        {
            animator.Play("UpWalk");
        }
        else
        {
            animator.Play("DownWalk");
        }
        lastPosition = transform.position;
    }
}
