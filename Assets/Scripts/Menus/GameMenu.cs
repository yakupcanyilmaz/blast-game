using UnityEngine;
using TMPro;

public class GameMenu : SimpleMenu<GameMenu>
{
  [SerializeField] private TextMeshProUGUI eggNumberText;

  public override void OnBackPressed()
  {
    PauseMenu.Show();
  }

  public void OnPauseButtonPressed()
  {
    PauseMenu.Show();
    GameManager.Instance.TogglePauseState();
  }

  public void UpdateEggNumber(int eggNumber)
  {
    eggNumberText.text = eggNumber.ToString();
  }

}
