namespace Imbick.StarCitizen.Api.HtmlSerializers {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HtmlAgilityPack;
    using Models;

    public class OrgHtmlSerializer {

        public IEnumerable<Org> DeserializeList(string orgs) {
            _doc.LoadHtml(orgs);
            return _doc.DocumentNode.ChildNodes
                .Where(n => n.Name == "div")
                .Select(Deserialize)
                .Where(m => !String.IsNullOrWhiteSpace(m.Symbol)); ;
        }

        public Org Deserialize(string org) {
            _doc.LoadHtml(org);
            return Deserialize(_doc.DocumentNode);
        }

        private Org Deserialize(HtmlNode orgNode) {
            try {
                var memberCount = orgNode.SelectSingleNode("./a[@class=\"trans-03s clearfix\"]/span[3]/span[2]/span[3]//span[2]").InnerText;
                var archetype = orgNode.SelectSingleNode("./a[@class=\"trans-03s clearfix\"]/span[3]/span[1]/span[1]//span[2]").InnerText;
                var commitment = orgNode.SelectSingleNode("./a[@class=\"trans-03s clearfix\"]/span[3]/span[1]/span[3]//span[2]").InnerText;
                return new Org {
                    Name = HtmlEntity.DeEntitize(orgNode.SelectSingleNode("./a[@class=\"trans-03s clearfix\"]/span[2]/span[2]/h3[@class=\"trans-03s name\"]").InnerText),
                    Symbol = orgNode.SelectSingleNode("./a[@class=\"trans-03s clearfix\"]/span[2]/span[2]/span[@class=\"symbol\"]").InnerText,
                    ThumbnailUrl = orgNode.SelectSingleNode("./a[@class=\"trans-03s clearfix\"]/span[2]/span[1]/img").GetAttributeValue("src", string.Empty),
                    Archetype = String.IsNullOrWhiteSpace(archetype) ? OrgArchetype._UNDEFINED_ : ToArchetype(archetype),
                    Commitment = String.IsNullOrWhiteSpace(commitment) ? OrgCommitment._UNDEFINED_ : (OrgCommitment)Enum.Parse(typeof(OrgCommitment), commitment),
                    Language = orgNode.SelectSingleNode("./a[@class=\"trans-03s clearfix\"]/span[3]/span[1]/span[2]//span[2]").InnerText,
                    IsRecruiting = orgNode.SelectSingleNode("./a[@class=\"trans-03s clearfix\"]/span[3]/span[2]/span[1]//span[2]").InnerText == "Yes",
                    IsRolePlay = orgNode.SelectSingleNode("./a[@class=\"trans-03s clearfix\"]/span[3]/span[2]/span[2]//span[2]").InnerText == "Yes",
                    MemberCount = String.IsNullOrWhiteSpace(memberCount) ? 0 : long.Parse(memberCount)
                };
            }
            catch (Exception e) {
                throw new Exception("Unable to parse organisation. Probably the HTML has changed.", e);
            }
        }

        private OrgArchetype ToArchetype(string value) {
            switch (value.ToLower()) {
                case "organization":
                    return OrgArchetype.Organization;
                case "corporation":
                    return OrgArchetype.Corporation;
                case "pmc":
                    return OrgArchetype.Pmc;
                case "faith":
                    return OrgArchetype.Faith;
                case "syndicate":
                    return OrgArchetype.Syndicate;
                case "club":
                    return OrgArchetype.Club;
            }

            throw new ArgumentException(value);
        }

        private readonly HtmlDocument _doc = new HtmlDocument();
    }
}