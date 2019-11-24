using System;

namespace BlubbFish.Utils.IoT.Events {
  public class UserMessageEventArgs : EventArgs {
    public UserMessageEventArgs() : base() { }
    public UserMessageEventArgs(String message, Int64 UserId, DateTime date) {
      this.UserId = UserId;
      this.Message = message;
      this.Date = date;
    }
    public Int64 UserId {
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
