using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConstructorIO
{
    public class Util
    {
        /// <summary>
        /// Serializes url params in a rudimentary way, and you must write other helper methods to serialize other things.
        /// </summary>
        /// <param name="paramDict"> params HashMap of the parameters to encode.</param>
        /// <returns> The encoded parameters, as a String.</returns>
        public static string SerializeParams(IDictionary<string, object> paramDict)
        {
            var list = new List<string>();
            foreach (var item in paramDict)
            {
                list.Add(System.Uri.EscapeDataString(item.Key) + "=" + System.Uri.EscapeDataString((string)item.Value));
            }
            return string.Join("&", list);
        }
    }

    /// <summary>
    ///  StringValueAttribute
    /// </summary>

    public class StringValueAttribute : Attribute
    {
        private string m_sValue;

        public StringValueAttribute(string value)
        {
            m_sValue = value;
        }

        public string Value
        {
            get { return m_sValue; }
        }
    }

    /// <summary>
    /// StringEnum
    /// </summary>
    public class StringEnum
    {
        private static Hashtable m_hsStringValues = new Hashtable();

        public static string GetStringValue(Enum value)
        {
            string output = null;

            Type type = value.GetType();

            if (m_hsStringValues.ContainsKey(value))
                output = (m_hsStringValues[value] as StringValueAttribute).Value;
            else
            {
                FieldInfo fi = type.GetField(value.ToString());
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                if (attrs.Length > 0)
                {
                    m_hsStringValues.Add(value, attrs[0]);
                    output = attrs[0].Value;
                }
            }

            return output;
        }
    }
}