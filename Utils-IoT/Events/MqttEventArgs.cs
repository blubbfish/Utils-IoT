using System;

namespace BlubbFish.Utils.IoT.Events {
  public class MqttEventArgs : EventArgs {
    public MqttEventArgs() : base() { }
    public MqttEventArgs(String message, String topic) {
      this.Topic = topic;
      this.Message = message;
      this.Date = DateTime.Now;
    }
    public String Topic {
      get; private set;
    }
    public String Message {
      get; private set;
    }
    public DateTime Date {
      get; private set;
    }
  }
}
