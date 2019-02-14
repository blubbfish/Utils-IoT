using System;
using System.Collections.Generic;
using System.Globalization;
using BlubbFish.Utils.IoT.Connector;
using BlubbFish.Utils.IoT.Events;

namespace BlubbFish.Utils.IoT.JsonSensor {
  class Luminanz : AJsonSensor {
    public Luminanz(Dictionary<String, String> settings, String name, ADataBackend backend) : base(settings, name, backend) {
      this.GetBool = true;
      this.GetFloat = 0.0f;
      this.GetInt = 0;
      this.Datatypes = Types.Int;
    }

    protected override Boolean UpdateValue(BackendEvent e) {
      this.GetInt = Int32.Parse(e.Message, new CultureInfo("en-US"));
      return true;
    }
  }
}
