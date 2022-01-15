using System;
using System.Collections.Generic;
using BlubbFish.Utils.IoT.Connector;
using BlubbFish.Utils.IoT.Events;

namespace BlubbFish.Utils.IoT.JsonSensor {
  class Pir : AJsonSensor {
    public Pir(Dictionary<String, String> settings, String name, ADataBackend backend) : base(settings, name, backend) => this.Datatypes = Types.Bool;

    protected override Boolean UpdateValue(BackendEvent e) {
      this.GetBool = e.Message.ToLower() == "on";
      return true;
    }
  }
}