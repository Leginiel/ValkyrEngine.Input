using Moq;
using System.Collections.Generic;
using System.Numerics;
using ValkyrEngine.Input.InputTracker;
using Veldrid;
using Xunit;

namespace ValkyrEngine.Input.Tests.InputTracker
{
  public class MouseInputTrackerTest
  {
    [Fact]
    [Trait("Category", "Unit")]

    public void UpdateStateTest_NewInputSnapShotWithMouseDown_MouseDownReceiverTriggered()
    {
      // Arrange
      MouseInputTracker tracker = new MouseInputTracker();
      Mock<InputSnapshot> inputSnapshotMock = new Mock<InputSnapshot>();
      MouseEvent mouseEvent = new MouseEvent(MouseButton.Left, true);
      bool receiverTriggered = false;
      Vector2 expectedPosition = new Vector2(2, 2);
      Vector2 actualPosition = default;

      inputSnapshotMock.Setup((_) => _.MouseEvents).Returns(new List<MouseEvent>() { mouseEvent });
      inputSnapshotMock.Setup((_) => _.MousePosition).Returns(expectedPosition);
      tracker.RegisterReceiver(mouseEvent, (position) =>
      {
        receiverTriggered = true;
        actualPosition = position;
      });

      // Act
      tracker.UpdateState(inputSnapshotMock.Object);

      // Assert
      Assert.True(receiverTriggered);
      Assert.Equal(expectedPosition, actualPosition);
    }

    [Fact]
    [Trait("Category", "Unit")]

    public void UpdateStateTest_SameInputSnapShotWithMouseDown_ReceiverNotTriggered()
    {
      // Arrange
      MouseInputTracker tracker = new MouseInputTracker();
      Mock<InputSnapshot> inputSnapshotMock = new Mock<InputSnapshot>();
      MouseEvent mouseEvent = new MouseEvent(MouseButton.Left, true);
      bool receiverTriggered = false;
      Vector2 expectedPosition = new Vector2(2, 2);
      Vector2 actualPosition = default;

      inputSnapshotMock.Setup((_) => _.MouseEvents).Returns(new List<MouseEvent>() { mouseEvent });
      inputSnapshotMock.Setup((_) => _.MousePosition).Returns(expectedPosition);
      tracker.RegisterReceiver(mouseEvent, (position) =>
      {
        receiverTriggered = true;
        actualPosition = position;
      });
      tracker.UpdateState(inputSnapshotMock.Object);

      actualPosition = default;
      receiverTriggered = false;
      // Act
      tracker.UpdateState(inputSnapshotMock.Object);

      // Assert
      Assert.False(receiverTriggered);
    }
  }
}
