using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChainController : MonoBehaviour
{
    [SerializeField] private BallType type;
    [SerializeField] private int numberToUnlock;
    [SerializeField] private TextMeshPro numberText;
    [SerializeField] private SpriteRenderer lockSprite;

    public BallType Type { get { return type; } }
    public int NumberToUnlock { get { return numberToUnlock; } set { numberToUnlock = value; } }
    public TextMeshPro NumberText { get { return numberText; } set { numberText = value; } }
    public SpriteRenderer LockSprite { get { return LockSprite; } set { lockSprite = value; } }

    public void Init(BallType type, int numberToUnlock)
    {
        this.type = type;
        this.numberToUnlock = numberToUnlock;
        this.numberText.text = numberToUnlock.ToString();
        SetLockColor(type);
    }

    private void SetLockColor(BallType type)
    {
        switch (type)
        {
            case BallType.blue:
                this.lockSprite.color = new Color32(21, 194, 238, 255);
                break;
            case BallType.green:
                this.lockSprite.color = new Color32(0, 209, 0, 255);
                break;
            case BallType.orange:
                this.lockSprite.color = new Color32(255, 181, 46, 255);
                break;
            case BallType.pink:
                this.lockSprite.color = new Color32(255, 59, 157, 255);
                break;
            case BallType.purple:
                this.lockSprite.color = new Color32(108, 11, 169, 255);
                break;
        }
    }

}
