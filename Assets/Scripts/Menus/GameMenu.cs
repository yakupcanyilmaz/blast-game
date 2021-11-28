using UnityEngine;

public class GameMenu : SimpleMenu<GameMenu>
{

  public override void OnBackPressed()
  {
    PauseMenu.Show();
  }

  public void OnPauseButtonPressed()
  {
    PauseMenu.Show();
    GameManager.Instance.TogglePauseState();
  }

}
