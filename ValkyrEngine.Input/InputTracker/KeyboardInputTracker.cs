using System;
using Veldrid;

namespace ValkyrEngine.Input.InputTracker
{
  internal class KeyboardInputTracker : InputTracker<KeyEvent, Action>
  {
    protected override void Update()
    {
      foreach (KeyEvent e in InputSnapshot.KeyEvents)
      {
        if (e.Down)
        {
          AddTrackedItem(e);
        }
        else
        {
          RemoveTrackedItem(e);
        }
      }
    }
  }
}
