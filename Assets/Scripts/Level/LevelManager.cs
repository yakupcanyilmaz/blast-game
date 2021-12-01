using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Level[] levels;
    [SerializeField] private Ball[] ballPrefabs;
    [SerializeField] private Ball icePrefab;
    [SerializeField] private Ball bombPrefab;
    [SerializeField] private Egg eggPrefab;
    [SerializeField] private ChainController chainPrefab;

    private int eggNumber;
    private float ballSize;
    [SerializeField] private List<ChainController> currentLevelChains = new List<ChainController>();

    public void Setup(int currentLevelNumber)
    {
        Instantiate(levels[currentLevelNumber].borderPrefab, new Vector2(0f, 0f), Quaternion.identity);

        int columnNumber = levels[currentLevelNumber].ballsColumnNumber;
        int rowNumber = levels[currentLevelNumber].ballsRowNumber;
        int iceNumber = levels[currentLevelNumber].iceNumber;
        ballSize = levels[currentLevelNumber].ballSize;
        eggNumber = levels[currentLevelNumber].eggNumber;

        int chainNumber = levels[currentLevelNumber].chains.Length;

        if (chainNumber > 0)
        {
            for (int i = 0; i < chainNumber; i++)
            {
                ChainController newChain = Instantiate(chainPrefab, levels[currentLevelNumber].chains[i].position, Quaternion.identity);
                newChain.Init(levels[currentLevelNumber].chains[i].type, levels[currentLevelNumber].chains[i].numberToUnlock);
                currentLevelChains.Add(newChain);
            }
        }
        for (int i = 0; i < columnNumber; i++)
        {
            for (int j = 0; j < rowNumber; j++)
            {
                SpawnRandomBall(new Vector2(i * ballSize - (ballSize * 0.5f * (columnNumber - 1)), (j * ballSize - (ballSize * 0.5f * (rowNumber - 1)) + 5f)));
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
        MenuManager.Instance.UpdateGameMenu(eggNumber);
    }

    public void SpawnBomb(Vector2 position)
    {
        Instantiate(bombPrefab, position, Quaternion.identity);
    }

    public void SpawnRandomBall(Vector2 position)
    {
        int randomIndex = Random.Range(0, ballPrefabs.Length);
        Instantiate(ballPrefabs[randomIndex], position, Quaternion.identity);
    }

    public void OnSameTypeBallsDestroyed(BallType type, int numberOfBallsDestroyed)
    {
        for (int i = 0; i < numberOfBallsDestroyed; i++)
        {
            SpawnRandomBall(new Vector2(i * ballSize - (ballSize * 0.5f * (numberOfBallsDestroyed - 1)), 5f));
        }
        if (currentLevelChains.Count > 0)
        {
            if (currentLevelChains[0].Type == type)
            {
                if (currentLevelChains[0].NumberToUnlock > numberOfBallsDestroyed)
                {
                    currentLevelChains[0].NumberToUnlock -= numberOfBallsDestroyed;
                    currentLevelChains[0].NumberText.text = currentLevelChains[0].NumberToUnlock.ToString();
                    if (currentLevelChains[0].NumberToUnlock == 0)
                    {
                        Destroy(currentLevelChains[0].gameObject);
                        currentLevelChains.RemoveAt(0);
                    }
                }
                else
                {
                    Destroy(currentLevelChains[0].gameObject);
                    currentLevelChains.RemoveAt(0);
                }
            }
        }
    }

    public void OnBallsDestroyed(List<Ball> destroyedBalls)
    {
        Debug.Log("BallsDestroyed: " + destroyedBalls.Count);
        for (int i = 0; i < destroyedBalls.Count * 0.25f; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                SpawnRandomBall(new Vector2(i * ballSize - (ballSize * 0.5f * (destroyedBalls.Count * 0.25f - 1)), (j * ballSize - (ballSize * 0.5f * (2 - 1)) + 5f)));
            }
        }
        if (currentLevelChains.Count > 0)
        {
            foreach (Ball destroyedBall in destroyedBalls)
            {
                if (destroyedBall.Type == currentLevelChains[0].Type)
                {
                    if (currentLevelChains[0].NumberToUnlock > 0)
                    {
                        currentLevelChains[0].NumberToUnlock--;
                        currentLevelChains[0].NumberText.text = currentLevelChains[0].NumberToUnlock.ToString();
                        continue;
                    }
                    if (currentLevelChains[0].NumberToUnlock <= 0)
                    {
                        Destroy(currentLevelChains[0].gameObject);
                        currentLevelChains.RemoveAt(0);
                        break;
                    }
                }
            }
        }
    }

    public void OnEggCracked()
    {
        if (eggNumber > 0)
        {
            eggNumber--;
            MenuManager.Instance.UpdateGameMenu(eggNumber);
        }
        if (eggNumber <= 0)
        {
            LevelEndMenu.Show();
            LevelEndMenu.Instance.OpenWinMenu();
            GameManager.Instance.OnLevelCompleted();
        }
    }

    [System.Serializable]
    public class Chain
    {
        public BallType type;
        public Vector3 position;
        public int numberToUnlock;
    }

    [System.Serializable]
    public class Level
    {
        public GameObject borderPrefab;
        public int ballsColumnNumber = 8;
        public int ballsRowNumber = 5;
        public float ballSize = 0.5f;
        public int eggNumber = 1;
        public int iceNumber = 5;
        public Chain[] chains;
    }
}
