using System;
using System.Collections.Generic;

namespace BlubbFish.Utils.IoT.Connector {
  public abstract class AUserBackend : ABackend {
    public AUserBackend(Dictionary<String, String> settings) : base(settings) {}
    public abstract void Send(String message);
    public abstract void Send(String message, String[] buttons);
  }
}
