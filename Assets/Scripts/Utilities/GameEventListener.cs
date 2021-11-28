using System;
using UnityEngine.Events;

[Serializable]
public class GameEventListener
{
  public GameEventSO gameEvent;
  public UnityEvent unityEvent;

  public void RaiseEvent()
  {
    unityEvent.Invoke();
  }
}
