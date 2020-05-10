using System;
using System.Collections.Generic;
using System.Numerics;
using ValkyrEngine.Input.InputTracker;
using Veldrid;

namespace ValkyrEngine.Input.Tests.Mocks
{
  internal class MouseTrackerMock : InputTracker<MouseEvent, Action<Vector2>>
  {
    public new InputSnapshot InputSnapshot => base.InputSnapshot;
    public new Dictionary<MouseEvent, HashSet<Action<Vector2>>> Receiver => base.Receiver;
    public new HashSet<MouseEvent> ExistingTrackedItems => base.ExistingTrackedItems;
    public new HashSet<MouseEvent> NewTrackedItems => base.NewTrackedItems;

    protected override void Update()
    {
    }
  }
}
