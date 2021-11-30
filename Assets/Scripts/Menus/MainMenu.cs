using UnityEngine;
using TMPro;

public class MainMenu : SimpleMenu<MainMenu>
{
  [SerializeField] private TextMeshProUGUI levelNoText;

  private void Start() 
  {
    levelNoText.text = (GameManager.Instance.CurrentLevelNumber.Value + 1).ToString();
  }

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
