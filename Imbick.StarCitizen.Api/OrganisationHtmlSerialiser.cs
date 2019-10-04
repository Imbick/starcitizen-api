namespace Imbick.StarCitizen.Api {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HtmlAgilityPack;
    using Models;

    public class OrganisationHtmlSerialiser {

        public IEnumerable<Organisation> DeserialiseList(string orgs) {
            _doc.LoadHtml(orgs);
            return _doc.DocumentNode.ChildNodes
                .Where(n => n.Name == "div")
                .Select(Deserialise);
        }

        public Organisation Deserialise(string org) {
            _doc.LoadHtml(org);
            return Deserialise(_doc.DocumentNode);
        }

        private Organisation Deserialise(HtmlNode orgNode) {
            try {
                return new Organisation {
                    Name = HtmlEntity.DeEntitize(orgNode.SelectSingleNode("./a[@class=\"trans-03s clearfix\"]/span[2]/span[2]/h3[@class=\"trans-03s name\"]").InnerText),
                    Symbol = orgNode.SelectSingleNode("./a[@class=\"trans-03s clearfix\"]/span[2]/span[2]/span[@class=\"symbol\"]").InnerText,
                    ThumbnailUrl = orgNode.SelectSingleNode("./a[@class=\"trans-03s clearfix\"]/span[2]/span[1]/img").GetAttributeValue("src", string.Empty),
                    Archetype = ToArchetype(orgNode.SelectSingleNode("./a[@class=\"trans-03s clearfix\"]/span[3]/span[1]/span[1]//span[2]").InnerText),
                    Commitment = (Commitment)Enum.Parse(typeof(Commitment), orgNode.SelectSingleNode("./a[@class=\"trans-03s clearfix\"]/span[3]/span[1]/span[3]//span[2]").InnerText),
                    Language = orgNode.SelectSingleNode("./a[@class=\"trans-03s clearfix\"]/span[3]/span[1]/span[2]//span[2]").InnerText,
                    IsRecruiting = orgNode.SelectSingleNode("./a[@class=\"trans-03s clearfix\"]/span[3]/span[2]/span[1]//span[2]").InnerText == "Yes",
                    IsRolePlay = orgNode.SelectSingleNode("./a[@class=\"trans-03s clearfix\"]/span[3]/span[2]/span[2]//span[2]").InnerText == "Yes",
                    MemberCount = long.Parse(orgNode.SelectSingleNode("./a[@class=\"trans-03s clearfix\"]/span[3]/span[2]/span[3]//span[2]").InnerText)
                };
            }
            catch (Exception e) {
                throw new Exception("Unable to parse organisation. Probably the HTML has changed.", e);
            }
        }

        private Archetype ToArchetype(string value) {
            switch (value.ToLower()) {
                case "organization":
                    return Archetype.Organization;
                case "corporation":
                    return Archetype.Corporation;
                case "pmc":
                    return Archetype.Pmc;
                case "faith":
                    return Archetype.Faith;
                case "syndicate":
                    return Archetype.Syndicate;
            }

            throw new ArgumentException(value);
        }

        private readonly HtmlDocument _doc = new HtmlDocument();
    }
}