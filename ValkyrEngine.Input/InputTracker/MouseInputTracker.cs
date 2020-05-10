using System;
using System.Numerics;
using Veldrid;

namespace ValkyrEngine.Input.InputTracker
{
  internal class MouseInputTracker : InputTracker<MouseEvent, Action<Vector2>>
  {
    protected override void Update()
    {
      foreach (MouseEvent e in InputSnapshot.MouseEvents)
      {
        if (e.Down)
        {
          AddTrackedItem(e, InputSnapshot.MousePosition);
        }
        else
        {
          RemoveTrackedItem(e, InputSnapshot.MousePosition);
        }
      }
    }
  }
}
