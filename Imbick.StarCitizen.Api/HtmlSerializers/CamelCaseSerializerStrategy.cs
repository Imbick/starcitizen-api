namespace Imbick.StarCitizen.Api.HtmlSerializers {
    using System.Linq;
    using System.Text.RegularExpressions;
    using RestSharp;

    internal class CamelCaseSerializerStrategy
        : PocoJsonSerializerStrategy {
        protected override string MapClrMemberNameToJsonFieldName(string clrPropertyName) {
            //lower-case first characher or any character behind an underscore
            return char.ToLower(clrPropertyName[0]) + Regex.Replace(clrPropertyName.Substring(1), @"_([A-Z]{1})", m => m.ToString().ToLower());
        }
    }
}