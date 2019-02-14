using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlubbFish.Utils.IoT {
  static class Helper {
    internal static String ToUpperLower(this String s) {
      if (s.Length == 0) {
        return "";
      }
      if (s.Length == 1) {
        return s.ToUpper();
      }
      return s[0].ToString().ToUpper() + s.Substring(1).ToLower();
    }
  }
}
