using System;

namespace CodeBase.Services
{
  public interface IUIService
  {
    event Action TapAreaClicked;

    void OnTapAreaClicked();
  }
}