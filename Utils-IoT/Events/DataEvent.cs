using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlubbFish.Utils.IoT.Events {
  public class DataEvent : BackendEvent {
    public DataEvent(String data) : base() { }
    public DataEvent(String message, String topic, DateTime date) : base(message, topic, date, "Data") { }
  }
}
