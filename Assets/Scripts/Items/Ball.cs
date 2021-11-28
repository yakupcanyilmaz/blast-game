using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallType { blue, green, pink, orange, purple, bomb, ice }

public class Ball : MonoBehaviour
{
    [SerializeField] private BallType type;
    public BallType Type { get { return type; } }
    [SerializeField] private LayerMask mask;
    public LayerMask Mask { get { return mask; } }
    [SerializeField] private Ball bombPrefab;
    [SerializeField] private Animator animator;
    public Animator Animator { get { return animator; } set { animator = value; } }

    private int destroyedBallNumberInOneClick;
    private bool mousePressed;

    [SerializeField] private List<Ball> connectedSameTypeBalls;
    [SerializeField] private bool isChecked;
    public bool IsChecked { get { return isChecked; } set { isChecked = value; } }


    private void Update()
    {
        if (mousePressed && Input.GetMouseButtonUp(0))
        {
            HandleMousePressed();
        }
    }

    private void OnMouseDown()
    {
        mousePressed = true;
    }

    private void HandleMousePressed()
    {
        mousePressed = false;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            Ball hitBall = hit.collider.GetComponent<Ball>();
            if (hitBall != null
            && hitBall.Type != BallType.ice
            && hitBall.Type != BallType.bomb)
            {
                SetConnectedSameTypeBalls(hitBall);
                destroyedBallNumberInOneClick = 0;
                if (connectedSameTypeBalls.Count > 1)
                {
                    AudioManager.Instance.PlaySound("Match");
                    foreach (Ball sameTypeBall in connectedSameTypeBalls)
                    {
                        destroyedBallNumberInOneClick++;
                        sameTypeBall.Animator.SetTrigger("Destroy");
                    }
                }
            }
            if (hitBall.Type == BallType.bomb)
            {
                AudioManager.Instance.PlaySound("Bomb");
                hitBall.Animator.SetTrigger("Destroy");
                RaycastHit2D[] rightBombHits = Physics2D.CircleCastAll(hitBall.transform.position, 0.25f, Vector2.right, LayerMask.GetMask("Ball"));
                RaycastHit2D[] leftBombHits = Physics2D.CircleCastAll(hitBall.transform.position, 0.25f, Vector2.left, LayerMask.GetMask("Ball"));

                if (rightBombHits != null)
                {
                    foreach (RaycastHit2D bombHit in rightBombHits)
                    {
                        Ball bombHitBall = bombHit.collider.GetComponent<Ball>();
                        if (bombHitBall) bombHitBall.Animator.SetTrigger("Destroy");
                    }
                }
                if (leftBombHits != null)
                {
                    foreach (RaycastHit2D bombHit in leftBombHits)
                    {
                        Ball bombHitBall = bombHit.collider.GetComponent<Ball>();
                        if (bombHitBall) bombHitBall.Animator.SetTrigger("Destroy");
                    }
                }
            }
        }
    }

    private void SetConnectedSameTypeBalls(Ball hitBall)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(hitBall.transform.position, 0.3f);
        if (colliders.Length != 0)
        {
            foreach (Collider2D collider in colliders)
            {
                Ball colliderBall = collider.GetComponent<Ball>();
                if (colliderBall != null)
                {
                    if (colliderBall.Type == hitBall.Type && colliderBall.isChecked == false
                    && (colliderBall.Type != BallType.bomb || colliderBall.Type != BallType.ice))
                    {
                        colliderBall.IsChecked = true;
                        connectedSameTypeBalls.Add(colliderBall);
                        SetConnectedSameTypeBalls(colliderBall);
                    }
                    if (colliderBall.Type == BallType.ice) colliderBall.Animator.SetTrigger("Destroy");
                }
            }
        }
    }

    // private void DestroySameTypes(Ball hitBall)
    // {
    //     Collider2D[] colliders = Physics2D.OverlapCircleAll(hitBall.transform.position, 0.3f);
    //     if (colliders.Length != 0)
    //     {
    //         foreach (Collider2D collider in colliders)
    //         {
    //             Ball colliderBall = collider.GetComponent<Ball>();
    //             if (colliderBall != null&&
    //                 colliderBall.Type == hitBall.Type)
    //             {
    //                 if (colliderBall.Type == BallType.ice)
    //                 {
    //                     Destroy(collider.gameObject);
    //                 }
    //                 if (colliderBall == hitBall)
    //                 {
    //                     Destroy(hitBall.gameObject);
    //                 }
    //                 if (colliderBall != hitBall )
    //                 {
    //                     DestroySameTypes(colliderBall);
    //                 }
    //             }
    //         }
    //     }
    // }

    void DestroyEvent()
    {
        Destroy(this.gameObject);
        if (destroyedBallNumberInOneClick > 3)
        {
            LevelManager.Instance.SpawnBomb(this.transform.position);
        }
    }
}
