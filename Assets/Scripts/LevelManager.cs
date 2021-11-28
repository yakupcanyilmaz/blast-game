using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Level[] levels;
    [SerializeField] private Ball[] ballPrefabs;
    [SerializeField] private Ball icePrefab;
    [SerializeField] private Ball bombPrefab;
    [SerializeField] private Egg eggPrefab;
    [SerializeField] private Chain chainPrefab;

    public void Setup(int currentLevelNumber)
    {
        Instantiate(levels[currentLevelNumber].BorderPrefab, new Vector2(0f, 0f), Quaternion.identity);

        float ballSize = levels[currentLevelNumber].BallSize;
        int columnNumber = levels[currentLevelNumber].BallsColumnNumber;
        int rowNumber = levels[currentLevelNumber].BallsRowNumber;
        int iceNumber = levels[currentLevelNumber].IceNumber;
        int eggNumber = levels[currentLevelNumber].EggNumber;

        for (int i = 0; i < columnNumber; i++)
        {
            for (int j = 0; j < rowNumber; j++)
            {
                int randomIndex = Random.Range(0, ballPrefabs.Length);
                Ball newBall = Instantiate(ballPrefabs[randomIndex], new Vector2(i * ballSize - (ballSize * 0.5f * (columnNumber - 1)), (j * ballSize - (ballSize * 0.5f * (rowNumber - 1)) + 5f)), Quaternion.identity);
            }
        }
        for (int i = 0; i < iceNumber; i++)
        {
            Instantiate(icePrefab, new Vector2(0f, 5f), Quaternion.identity);
        }
        for (int i = 0; i < eggNumber; i++)
        {
            Instantiate(eggPrefab, new Vector2(0f, 5f), Quaternion.identity);
        }
    }


    public void SpawnBomb(Vector2 position)
    {
        Instantiate(bombPrefab, position, Quaternion.identity);
    }


  [System.Serializable]
  public class Level
  {
    [SerializeField] private GameObject borderPrefab;
    [SerializeField] private int ballsColumnNumber = 8;
    [SerializeField] private int ballsRowNumber = 5;
    [SerializeField] private float ballSize = 0.5f;
    [SerializeField] private int eggNumber = 1;
    [SerializeField] private int iceNumber = 5;
    [SerializeField] private int chainNumber = 0;
    public GameObject BorderPrefab {get{return borderPrefab;}}
    public int BallsColumnNumber {get{return ballsColumnNumber;}}
    public int BallsRowNumber {get{return ballsRowNumber;}}
    public float BallSize {get{return ballSize;}}
    public int EggNumber {get{return eggNumber;}}
    public int IceNumber {get{return iceNumber;}}
    public int ChainNumber {get{return chainNumber;}}

  }
}
