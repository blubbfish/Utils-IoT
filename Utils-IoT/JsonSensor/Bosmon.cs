using System;
using System.Collections.Generic;
using System.Linq;
using BlubbFish.Utils.IoT.Connector;
using BlubbFish.Utils.IoT.Events;
using LitJson;

namespace BlubbFish.Utils.IoT.JsonSensor {
  class Bosmon : AJsonSensor {
    public Bosmon(Dictionary<String, String> settings, String name, ABackend backend) : base(settings, name, backend) {
    }

    public String Ric { get; private set; }
    public String Message { get; private set; }
    public String Func { get; private set; }
    public DateTime Time { get; private set; }

    protected override Boolean UpdateValue(BackendEvent e) {
      try {
        JsonData json = JsonMapper.ToObject(e.Message);
        if(json.ContainsKey("TYPE_POCSAG")) {
          if(this.settings["rics"].Split(';').ToList().Contains(json["Address"].ToString())) {
            this.Ric = json["Address"].ToString();
            this.Message = json["Msg"].ToString();
            this.Func = json["Func"].ToString();
            this.Time = new DateTime(Int64.Parse(json["Timestamp"].ToString()));
            return true;
          }
        }
      } catch(Exception) { }
      return false;
    }
  }
}
