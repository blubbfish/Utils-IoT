using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using BlubbFish.Utils.IoT.Connector;
using BlubbFish.Utils.IoT.Events;

namespace BlubbFish.Utils.IoT.JsonSensor {
  public abstract class AJsonSensor : IDisposable {
    protected String topic;
    protected Int32 pollcount;
    protected Dictionary<String, String> settings;
    protected ABackend backend;
    private Thread pollingThread;
    private Boolean pollEnabled = false;

    public AJsonSensor(Dictionary<String, String> settings, String name, ABackend backend) {
      this.GetBool = true;
      this.GetFloat = 0.0f;
      this.GetInt = 0;
      this.topic = settings.Keys.Contains("topic") ? settings["topic"] : "";
      this.settings = settings;
      this.Title = settings.Keys.Contains("title") ? settings["title"] : "";
      this.Name = name;
      this.backend = backend;
      this.backend.MessageIncomming += this.IncommingMqttMessage;
      if (settings.Keys.Contains("polling")) {
        this.pollEnabled = true;
        this.Polling = Int32.Parse(settings["polling"]);
        this.pollcount = this.Polling;
        this.pollingThread = new Thread(this.SensorPolling);
        this.pollingThread.Start();
      }
    }

    private void SensorPolling() {
      while(this.pollEnabled) {
        Thread.Sleep(1000);
        this.Poll();
      }
    }

    private void IncommingMqttMessage(Object sender, BackendEvent e) {
      if(Regex.Match(e.From.ToString(), this.topic).Success) {
        if (this.UpdateValue(e)) {
          this.Timestamp = DateTime.Now;
          this.Update?.Invoke(this, e);
        }
      }
    }

    public static AJsonSensor GetInstance(Dictionary<String, ABackend> backends, Dictionary<String, String> settings, String name) {
      String object_sensor = "BlubbFish.Utils.IoT.JsonSensor." + Char.ToUpper(settings["type"][0]) + settings["type"].Substring(1).ToLower();
      Type t;
      try {
        t = Type.GetType(object_sensor, true);
      } catch(TypeLoadException) {
        throw new ArgumentException("Sensor: " + object_sensor + " is not a Sensor");
      }
      if(!settings.ContainsKey("backend") || !backends.ContainsKey(settings["backend"])) {
        throw new ArgumentException("Backend not specified!");
      }
      return (AJsonSensor)t.GetConstructor(new Type[] { typeof(Dictionary<String, String>), typeof(String), typeof(ABackend) }).Invoke(new Object[] { settings, name, backends[settings["backend"]] });
    }
    
    protected virtual void Poll() {
      if(this.pollcount++ >= this.Polling) {
        this.pollcount = 1;
        if (this.backend is ADataBackend) {
          ((ADataBackend)this.backend).Send(this.topic + "/get", "");
        }
      }
    }

    public virtual void SetBool(Boolean v) {
      if (this.backend is ADataBackend) {
        ((ADataBackend)this.backend).Send(this.topic + "/set", v ? "on" : "off");
      }
    }

    protected abstract Boolean UpdateValue(BackendEvent e);

    public Single GetFloat { get; protected set; }
    public Boolean GetBool { get; protected set; }
    public Int32 GetInt { get; protected set; }
    public Types Datatypes { get; protected set; }
    public DateTime Timestamp { get; protected set; }
    public Int32 Polling { get; private set; }
    public String Title { get; protected set; }
    public String Name { get; internal set; }

    public enum Types {
      Bool,
      Int,
      Float
    }
    public delegate void UpdatedValue(Object sender, EventArgs e);
    public event UpdatedValue Update;

    #region IDisposable Support
    private Boolean disposedValue = false;
    

    protected virtual void Dispose(Boolean disposing) {
      if(!this.disposedValue) {
        if(disposing) {
          this.pollEnabled = false;
          if (this.pollingThread != null && this.pollingThread.ThreadState == ThreadState.Running) {
            this.pollingThread.Abort();
            while (this.pollingThread.ThreadState != ThreadState.Aborted) { }
          }
          this.backend.MessageIncomming -= this.IncommingMqttMessage;
        }
        this.settings = null;
        this.pollingThread = null;
        this.disposedValue = true;
      }
    }

    ~AJsonSensor() {
      this.Dispose(false);
    }

    public void Dispose() {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }
    #endregion
  }
}