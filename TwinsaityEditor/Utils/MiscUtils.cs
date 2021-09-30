using Twinsanity;
using System;

namespace TwinsaityEditor.Utils
{
    static class TextUtils
    {

        public static bool Pref_TruncateObjectNames = true;
        public static bool Pref_EnableAnyObjectNames = true;

        public static string TruncateObjectName(string obj_name, ushort ObjectID, string prefix, string suffix)
        {
            if (Pref_TruncateObjectNames)
            {
                if (obj_name != string.Empty && obj_name.Split('|').Length > 1)
                {
                    obj_name = obj_name.Split('|')[obj_name.Split('|').Length - 1];
                }
                if (obj_name != string.Empty && obj_name.StartsWith("act_"))
                {
                    obj_name = obj_name.Substring(4);
                }
            }
            if (obj_name == string.Empty && Pref_EnableAnyObjectNames)
            {
                if (Enum.IsDefined(typeof(DefaultEnums.ObjectID), ObjectID))
                {
                    obj_name = prefix + (DefaultEnums.ObjectID)ObjectID + suffix;
                }
            }
            return obj_name;
        }
    }
}
