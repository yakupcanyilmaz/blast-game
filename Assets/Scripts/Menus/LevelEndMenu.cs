using UnityEngine;
using UnityEngine.UI;

public class LevelEndMenu : SimpleMenu<LevelEndMenu>
{

  public void OnMainPressed()
  {
    MenuManager.Instance.LoadMainMenu();
  }

  public void OpenWinMenu()
  {
    GameManager.Instance.TogglePauseState();
    AudioManager.Instance.PlaySound("Win");
  }

  public void OpenLoseMenu()
  {
    GameManager.Instance.TogglePauseState();
    AudioManager.Instance.PlaySound("Lose");
  }
}