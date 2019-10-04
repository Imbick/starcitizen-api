namespace Imbick.StarCitizen.Api.HtmlSerializers {
    using System.Linq;
    using System.Text.RegularExpressions;
    using RestSharp;

    internal class LowerCaseSerializerStrategy
        : PocoJsonSerializerStrategy {
        protected override string MapClrMemberNameToJsonFieldName(string clrPropertyName) {
            return clrPropertyName.ToLower();
        }
    }
}