using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
  [SerializeField] private LevelNoSO currentLevelNumber;

  private int ballsToSpawn = 0;
  public int BallsToSpawn { get { return ballsToSpawn; } }
  private int ballsRemaining;

  private bool isPaused;
  private bool isBallSpawned;
  private bool isParticlesSpawned;

  private void OnEnable()
  {
    isPaused = true;
    ToggleTimeScale();
  }

 public void StartGame()
  {
    LevelManager.Instance.Setup(currentLevelNumber.Value);
  }

  public void HandleTogglePauseState()
  {
    TogglePauseState();
    UpdateUIMenu();
  }

  public void TogglePauseState()
  {
    isPaused = !isPaused;
    ToggleTimeScale();
  }

  void ToggleTimeScale()
  {
    float newTimeScale = 0f;

    switch (isPaused)
    {
      case true:
        newTimeScale = 0f;
        break;
      case false:
        newTimeScale = 1f;
        break;
    }

    Time.timeScale = newTimeScale;
  }

  void UpdateUIMenu()
  {
    MenuManager.Instance.HandleBackPressed();
  }

}
