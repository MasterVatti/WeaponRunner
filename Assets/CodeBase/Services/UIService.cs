using System;

namespace CodeBase.Services
{
  public class UIService : IUIService
  {
    public event Action TapAreaClicked;

    public void OnTapAreaClicked() => TapAreaClicked?.Invoke();
  }
}