using Moq;
using System.Collections.Generic;
using ValkyrEngine.Input.InputTracker;
using Veldrid;
using Xunit;

namespace ValkyrEngine.Input.Tests.InputTracker
{
  public class KeyboardInputTrackerTest
  {
    [Fact]
    [Trait("Category", "Unit")]

    public void UpdateStateTest_NewInputSnapShotWithKeyDown_KeyDownReceiverTriggered()
    {
      // Arrange
      KeyboardInputTracker tracker = new KeyboardInputTracker();
      Mock<InputSnapshot> inputSnapshotMock = new Mock<InputSnapshot>();
      KeyEvent keyEvent = new KeyEvent(Key.A, true, ModifierKeys.None);
      bool receiverTriggered = false;

      inputSnapshotMock.Setup((_) => _.KeyEvents).Returns(new List<KeyEvent>() { keyEvent });
      tracker.RegisterReceiver(keyEvent, () => receiverTriggered = true);

      // Act
      tracker.UpdateState(inputSnapshotMock.Object);

      // Assert
      Assert.True(receiverTriggered);
    }

    [Fact]
    [Trait("Category", "Unit")]

    public void UpdateStateTest_SamenputSnapShotWithKeyDown_KeyDownReceiverNotTriggered()
    {
      // Arrange
      KeyboardInputTracker tracker = new KeyboardInputTracker();
      Mock<InputSnapshot> inputSnapshotMock = new Mock<InputSnapshot>();
      KeyEvent keyEvent = new KeyEvent(Key.A, true, ModifierKeys.None);
      bool receiverTriggered = false;

      inputSnapshotMock.Setup((_) => _.KeyEvents).Returns(new List<KeyEvent>() { keyEvent });
      tracker.RegisterReceiver(keyEvent, () => receiverTriggered = true);
      tracker.UpdateState(inputSnapshotMock.Object);
      receiverTriggered = false;

      // Act

      tracker.UpdateState(inputSnapshotMock.Object);
      // Assert
      Assert.False(receiverTriggered);
    }
  }
}
