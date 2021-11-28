using UnityEngine;

public class PauseMenu : SimpleMenu<PauseMenu>
{
  [SerializeField] private GameObject mobileControls;
  [SerializeField] private GameObject desktopControls;

  private void Start()
  {

  }

  public void OnQuitPressed()
  {
    Hide();
    Destroy(this.gameObject); // This menu does not automatically destroy itself

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
