using System;
using System.Collections.Generic;
using ValkyrEngine.Input.InputTracker;
using Veldrid;

namespace ValkyrEngine.Input.Tests.Mocks
{
  internal class InputTrackerMock : InputTracker<KeyEvent, Action>
  {
    public new InputSnapshot InputSnapshot => base.InputSnapshot;
    public new Dictionary<KeyEvent, HashSet<Action>> Receiver => base.Receiver;
    public new HashSet<KeyEvent> ExistingTrackedItems => base.ExistingTrackedItems;
    public new HashSet<KeyEvent> NewTrackedItems => base.NewTrackedItems;

    protected override void Update()
    {
    }
  }
}
