using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ValkyrEngine.Input.InputTracker;
using ValkyrEngine.Input.Messages;
using ValkyrEngine.Messages;
using ValkyrEngine.MessageSystem;
using ValkyrEngine.Window;
using Veldrid;

namespace ValkyrEngine.Input
{
  public class InputSystem : System<InputSettings>
  {
    private readonly IGameWindow window;
    private readonly List<IInputTracker> inputTracker = new List<IInputTracker>();

    public InputSystem(IMessageSystem messageSystem, IGameWindow window)
      : base(messageSystem)
    {
      this.window = window;
    }

    protected override void Setup()
    {
      if (SystemSettings.UseKeyboardInput)
      {
        inputTracker.Add(new KeyboardInputTracker());
      }
      if (SystemSettings.UseMouseInput)
      {
        inputTracker.Add(new MouseInputTracker());
      }
      window.FocusGained += WindowFocusGained;
      window.FocusLost += WindowFocusLost;

    }

    protected override void SetupMessageHandler()
    {
      MessageSystem.RegisterReceiver<FrameUpdateMessage>(HandleFrameUpdateMessage);
      MessageSystem.RegisterReceiver<KeyboardActionMessage>(HandleKeyboardActionMessage);
      MessageSystem.RegisterReceiver<MouseActionMessage>(HandleMouseActionMessage);
    }

    public override void CleanUp()
    {
      inputTracker.Clear();
    }

    protected override void CleanUpMessageHandler()
    {
      MessageSystem.UnregisterReceiver<FrameUpdateMessage>(HandleFrameUpdateMessage);
      MessageSystem.UnregisterReceiver<KeyboardActionMessage>(HandleKeyboardActionMessage);
      MessageSystem.UnregisterReceiver<MouseActionMessage>(HandleMouseActionMessage);
    }

    private void WindowFocusGained(object sender, EventArgs e)
    {
      ClearState();
    }
    private void WindowFocusLost(object sender, EventArgs e)
    {
      ClearState();
    }

    private void ClearState()
    {
      foreach (IInputTracker inputTracker in inputTracker)
      {
        inputTracker.ClearState();
      }
    }
    #region MessageHandler

    private Task HandleFrameUpdateMessage(FrameUpdateMessage message)
    {
      return Task.Run(() =>
      {
        InputSnapshot snapshot = window.GetInputSnapshot();

        foreach (IInputTracker inputTracker in inputTracker)
        {
          inputTracker.UpdateState(snapshot);
        }
      });
    }
    private Task HandleKeyboardActionMessage(KeyboardActionMessage message)
    {
      return Task.Run(() =>
      {
        KeyboardInputTracker tracker = (KeyboardInputTracker)inputTracker.Find((_) => _ is KeyboardInputTracker);

        switch (message.ActionType)
        {
          case ActionTypes.Registration:
            tracker.RegisterReceiver(message.KeyEvent, message.Callback);
            break;
          case ActionTypes.Deregistration:
            tracker.UnregisterReceiver(message.KeyEvent, message.Callback);
            break;
          default:
            break;
        }
      });
    }
    private Task HandleMouseActionMessage(MouseActionMessage message)
    {
      return Task.Run(() =>
      {
        MouseInputTracker tracker = (MouseInputTracker)inputTracker.Find((_) => _ is MouseInputTracker);

        switch (message.ActionType)
        {
          case ActionTypes.Registration:
            tracker.RegisterReceiver(message.MouseEvent, message.Callback);
            break;
          case ActionTypes.Deregistration:
            tracker.UnregisterReceiver(message.MouseEvent, message.Callback);
            break;
          default:
            break;
        }
      });
    }
    #endregion
  }
}
