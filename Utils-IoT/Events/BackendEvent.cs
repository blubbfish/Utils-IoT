using System;

namespace BlubbFish.Utils.IoT.Events {
  public class BackendEvent : EventArgs {
    public BackendEvent() : base() { }
    public BackendEvent(String message, Object from, DateTime date, String label) {
      this.From = from;
      this.Message = message;
      this.Date = date;
      this.Label = label;
    }

    public Object From { get; private set; }
    public String Message { get; private set; }
    public DateTime Date { get; private set; }
    public String Label { get; private set; }
  }
}
