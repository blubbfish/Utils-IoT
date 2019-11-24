using System;

namespace BlubbFish.Utils.IoT.Events {
  public class DataEvent : BackendEvent {
    public DataEvent() : base() { }
    public DataEvent(String message, String topic, DateTime date) : base(message, topic, date, "Data") { }
  }
}
