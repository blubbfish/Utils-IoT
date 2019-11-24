using System;

namespace BlubbFish.Utils.IoT.Events {
  public class UserEvent : BackendEvent {
    public UserEvent() : base() { }
    public UserEvent(String message, Int64 UserId, DateTime date) : base(message, UserId, date, "User") { }
  }
}
