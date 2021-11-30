using UnityEngine;

public class PauseMenu : SimpleMenu<PauseMenu>
{
  public void OnQuitPressed()
  {
    Hide();
    GameMenu.Hide();
    MenuManager.Instance.LoadMainMenu();
  }

  public override void OnBackPressed()
  {
    base.OnBackPressed();
  }

  public void OnResumeButtonPressed()
  {
    base.OnBackPressed();
    GameManager.Instance.TogglePauseState();
  }
}
