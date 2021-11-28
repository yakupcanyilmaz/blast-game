using UnityEngine;

public class MainMenu : SimpleMenu<MainMenu>
{
  public void OnPlayPressed()
  {
    GameMenu.Show();
    GameManager.Instance.StartGame();
    GameManager.Instance.TogglePauseState();
    AudioManager.Instance.PlaySound("Click");
  }

  public void OnOptionsPressed()
  {
    OptionsMenu.Show();
  }

  public override void OnBackPressed()
  {
    Application.Quit();
  }
}
