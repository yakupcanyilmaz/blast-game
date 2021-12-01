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
    [SerializeField] private List<Ball> destroyedBallsInOneClick;
    private bool mousePressed;

    private List<Ball> connectedSameTypeBalls;
    private bool isChecked;
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
            destroyedBallsInOneClick = new List<Ball>();
            connectedSameTypeBalls = new List<Ball>();
            Ball hitBall = hit.collider.GetComponent<Ball>();
            if (hitBall != null
            && hitBall.Type != BallType.ice
            && hitBall.Type != BallType.bomb)
            {
                SetConnectedSameTypeBalls(hitBall);
                destroyedBallNumberInOneClick = 0;
                if (connectedSameTypeBalls.Count > 1)
                {
                    LevelManager.Instance.OnSameTypeBallsDestroyed(hitBall.Type, connectedSameTypeBalls.Count);
                    AudioManager.Instance.PlaySound("Match");
                    foreach (Ball sameTypeBall in connectedSameTypeBalls)
                    {
                        destroyedBallNumberInOneClick++;
                        sameTypeBall.Animator.SetTrigger("Destroy");
                    }
                    Debug.Log(destroyedBallNumberInOneClick);
                }
            }
            if (hitBall.Type == BallType.bomb)
            {
                AudioManager.Instance.PlaySound("Bomb");
                hitBall.Animator.SetTrigger("Destroy");
                Vector2 castStartPosition = new Vector2((hitBall.transform.position - Vector3.right * 10f).x, hitBall.transform.position.y);
                Debug.Log(castStartPosition);
                                                                                    
                RaycastHit2D[] bombHits = Physics2D.CircleCastAll(castStartPosition, 0.25f, Vector2.right, 20f, LayerMask.GetMask("Ball"));
                Debug.DrawLine(castStartPosition, castStartPosition + Vector2.right * 20f, Color.white, 10.0f, false);

                if (bombHits != null)
                {
                    for (int i = 0; i < bombHits.Length; i++)
                    {
                        Ball bombHitBall = bombHits[i].collider.GetComponent<Ball>();
                        if (bombHitBall)
                        {
                            bombHitBall.Animator.SetTrigger("Destroy");
                            destroyedBallsInOneClick.Add(bombHitBall);
                        }
                        if (i == bombHits.Length - 1) LevelManager.Instance.OnBallsDestroyed(destroyedBallsInOneClick);
                    }
                }
                Debug.Log("BallsDestroyed: " + destroyedBallsInOneClick.Count);
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
                        if(colliderBall != hitBall)
                        {
                            colliderBall.isChecked = true;
                            connectedSameTypeBalls.Add(colliderBall);
                            SetConnectedSameTypeBalls(colliderBall);
                        }
                    }
                    if (colliderBall.Type == BallType.ice && hitBall.isChecked == true) colliderBall.Animator.SetTrigger("Destroy");
                }
            }
        }
    }

    void DestroyEvent()
    {
        Destroy(this.gameObject);
        if (destroyedBallNumberInOneClick > 3)
        {
            LevelManager.Instance.SpawnBomb(this.transform.position);
        }
    }
}
