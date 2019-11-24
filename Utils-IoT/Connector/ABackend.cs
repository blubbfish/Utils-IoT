using System;
using System.Collections.Generic;
using BlubbFish.Utils.IoT.Events;

namespace BlubbFish.Utils.IoT.Connector {
  public abstract class ABackend {
    public enum BackendType {
      Data,
      User
    }
    public event BackendMessage MessageIncomming;
    public event BackendMessage MessageSending;
    public delegate void BackendMessage(Object sender, BackendEvent e);

    protected Dictionary<String, String> settings;

    public abstract Boolean IsConnected { get; }

    public ABackend(Dictionary<String, String> settings) => this.settings = settings;

    public static ABackend GetInstance(Dictionary<String, String> settings, BackendType ty) {
      if (settings.Count == 0) {
        return null;
      }
      String object_sensor = "BlubbFish.Utils.IoT.Connector." + ty.ToString() + "." + settings["type"].ToUpperLower() + ", " + "Connector" + ty.ToString() + settings["type"].ToUpperLower();
      Type t;
      try {
        t = Type.GetType(object_sensor, true);
      } catch (TypeLoadException) {
        Console.Error.WriteLine("Configuration: " + settings["type"] + " is not a " + ty.ToString() + "Backend");
        return null;
      } catch (System.IO.FileNotFoundException) {
        Console.Error.WriteLine("Driver " + object_sensor + " could not load!");
        return null;
      }
      return (ABackend)t.GetConstructor(new Type[] { typeof(Dictionary<String, String>) }).Invoke(new Object[] { settings });
    }

    protected void NotifyClientIncomming(BackendEvent value) => this.MessageIncomming?.Invoke(this, value);

    protected void NotifyClientSending(BackendEvent value) => this.MessageSending?.Invoke(this, value);

    public abstract void Dispose();
  }
}
