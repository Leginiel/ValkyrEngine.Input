using System;
using ValkyrEngine.Input.Tests.Mocks;
using Veldrid;
using Xunit;

namespace ValkyrEngine.Input.Tests.InputTracker
{
  public class InputTrackerTest
  {
    [Fact]
    [Trait("Category", "Unit")]
    public void RegisterReceiver_CallbackNull_ArgumentNullException()
    {
      // Arrange
      InputTrackerMock tracker = new InputTrackerMock();

      // Act / Assert

      Assert.Throws<ArgumentNullException>(() => tracker.RegisterReceiver(new KeyEvent(), null));
      Assert.Empty(tracker.Receiver);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void RegisterReceiver_ValidCallback_ReceiverRegistered()
    {
      // Arrange
      InputTrackerMock tracker = new InputTrackerMock();
      KeyEvent keyEvent = new KeyEvent();
      static void callback() { }
      // Act 

      tracker.RegisterReceiver(keyEvent, callback);

      // Assert

      Assert.Single(tracker.Receiver);
      Assert.Single(tracker.Receiver[keyEvent]);
      Assert.Contains(callback, tracker.Receiver[keyEvent]);
    }

    [Fact]
    [Trait("Category", "Unit")]
    public void UnregisterReceiver_ValidCallback_ReceiverUnregistered()
    {
      // Arrange
      InputTrackerMock tracker = new InputTrackerMock();
      KeyEvent keyEvent = new KeyEvent();
      static void callback() { }
      tracker.RegisterReceiver(keyEvent, callback);
      // Act 

      tracker.UnregisterReceiver(keyEvent, callback);

      // Assert

      Assert.Single(tracker.Receiver);
      Assert.Empty(tracker.Receiver[keyEvent]);
    }
  }
}
