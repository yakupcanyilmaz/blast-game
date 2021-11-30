using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Blast Game/Level")]
public class LevelSO : ScriptableObject
{
    [System.Serializable]
    public class Chain
    {
        public BallType type;
        public Vector3 position;
        public int numberToUnlock;
    }
    public GameObject borderPrefab;
    public int ballsColumnNumber = 8;
    public int ballsRowNumber = 5;
    public float ballSize = 0.5f;
    public int eggNumber = 1;
    public int iceNumber = 5;
    public Chain[] chains;
}