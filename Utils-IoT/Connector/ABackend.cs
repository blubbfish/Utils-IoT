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
      try {
        Type t = Type.GetType(object_sensor, true);
        return (ABackend)t.GetConstructor(new Type[] { typeof(Dictionary<String, String>) }).Invoke(new Object[] { settings });
      } catch (TypeLoadException) {
        throw new TypeLoadException("Configuration: " + settings["type"] + " is not a " + ty.ToString() + "Backend");
      } catch (System.IO.FileNotFoundException) {
        throw new DllNotFoundException("Driver " + object_sensor + " could not load!");
      } catch (Exception e) {
        throw new Exception("Something bad Happend while Loading Connectior: "+e.Message);
      }
    }

    protected void NotifyClientIncomming(BackendEvent value) => this.MessageIncomming?.Invoke(this, value);

    protected void NotifyClientSending(BackendEvent value) => this.MessageSending?.Invoke(this, value);

    public abstract void Dispose();
  }
}
