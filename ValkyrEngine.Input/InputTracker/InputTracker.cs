using System;
using System.Collections.Generic;
using Veldrid;

namespace ValkyrEngine.Input.InputTracker
{
  internal abstract class InputTracker<TrackingType, CallbackType> : IInputTracker
    where TrackingType : struct
    where CallbackType : Delegate
  {
    protected readonly Dictionary<TrackingType, HashSet<CallbackType>> Receiver = new Dictionary<TrackingType, HashSet<CallbackType>>();

    protected HashSet<TrackingType> ExistingTrackedItems { get; } = new HashSet<TrackingType>();
    protected HashSet<TrackingType> NewTrackedItems { get; } = new HashSet<TrackingType>();
    protected InputSnapshot InputSnapshot { get; private set; }

    public void ClearState()
    {
      ExistingTrackedItems.Clear();
      NewTrackedItems.Clear();
    }

    public void UpdateState(InputSnapshot snapshot)
    {
      InputSnapshot = snapshot;
      NewTrackedItems.Clear();
      Update();
    }
    public void RegisterReceiver(TrackingType type, CallbackType callback)
    {
      if (callback == null)
        throw new ArgumentNullException(nameof(callback));

      if (!Receiver.ContainsKey(type))
      {
        Receiver.Add(type, new HashSet<CallbackType>() { callback });
      }
      else
      {
        Receiver[type].Add(callback);
      }
    }
    public void UnregisterReceiver(TrackingType type, CallbackType callback)
    {
      if (Receiver.ContainsKey(type))
      {
        Receiver[type].Remove(callback);
      }
    }
    public void Dispose()
    {
      ClearState();
      Receiver.Clear();
    }
    protected void NotifyReceiver(TrackingType type, params object[] arguments)
    {
      if (Receiver.ContainsKey(type))
      {
        foreach (CallbackType action in Receiver[type])
        {
          action.Method.Invoke(action.Target, arguments);
        }
      }
    }

    protected void AddTrackedItem(TrackingType type, params object[] arguments)
    {
      if (ExistingTrackedItems.Add(type))
      {
        NewTrackedItems.Add(type);
        NotifyReceiver(type, arguments);
      }
    }

    protected void RemoveTrackedItem(TrackingType type, params object[] arguments)
    {
      NewTrackedItems.Remove(type);
      ExistingTrackedItems.Remove(type);
      NotifyReceiver(type, arguments);
    }

    protected abstract void Update();
  }
}
