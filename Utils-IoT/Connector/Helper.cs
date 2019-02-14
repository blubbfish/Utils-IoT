using System;

namespace BlubbFish.Utils.IoT.Connector {
  public class UserMessageEventArgs : EventArgs {
    public UserMessageEventArgs() : base() { }
    public UserMessageEventArgs(String message, Int64 UserId, DateTime date) {
      this.UserId = UserId;
      this.Message = message;
      this.Date = date;
    }
    public Int64 UserId { get; private set; }
    public String Message { get; private set; }
    public DateTime Date { get; private set; }
  }
  public class MqttEventArgs : EventArgs {
    public MqttEventArgs() : base() { }
    public MqttEventArgs(String message, String topic) {
      this.Topic = topic;
      this.Message = message;
      this.Date = DateTime.Now;
    }
    public String Topic { get; private set; }
    public String Message { get; private set; }
    public DateTime Date { get; private set; }
  }
}
