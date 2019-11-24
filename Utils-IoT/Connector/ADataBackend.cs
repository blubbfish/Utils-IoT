using System;
using System.Collections.Generic;

namespace BlubbFish.Utils.IoT.Connector {
  public abstract class ADataBackend : ABackend {
    public ADataBackend(Dictionary<String, String> settings) : base(settings) { }
    public abstract void Send(String topic, String data);
  }
}
