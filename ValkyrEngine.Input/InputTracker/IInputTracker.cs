using System;
using Veldrid;

namespace ValkyrEngine.Input.InputTracker
{
  public interface IInputTracker : IDisposable
  {
    void UpdateState(InputSnapshot snapshot);
    void ClearState();
  }
}
