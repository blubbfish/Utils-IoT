using System;
using System.Collections.Generic;
using BlubbFish.Utils.IoT.Events;

namespace BlubbFish.Utils.IoT.Connector {
  public abstract class ADataBackend : ABackend {
    public ADataBackend(Dictionary<String, String> settings) : base(settings) { }
    public abstract void Send(String topic, String data);
  }
}
