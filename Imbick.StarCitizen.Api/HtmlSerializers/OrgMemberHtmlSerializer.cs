namespace Imbick.StarCitizen.Api.HtmlSerializers {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HtmlAgilityPack;
    using Models;

    public class OrgMemberHtmlSerializer {

        public IEnumerable<OrgMember> DeserializeList(string orgs) {
            _doc.LoadHtml(orgs);
            return _doc.DocumentNode.ChildNodes
                .Where(n => n.Name == "li")
                .Select(Deserialize)
                .Where(m => !String.IsNullOrWhiteSpace(m.Handle));
        }

        public OrgMember Deserialize(string org) {
            _doc.LoadHtml(org);
            return Deserialize(_doc.DocumentNode);
        }

        private OrgMember Deserialize(HtmlNode orgMemberNode) {
            try {
                var imgNode = orgMemberNode.SelectSingleNode("./a//span[contains(concat(' ', normalize-space(@class), ' '), ' thumb ')]/img");
                return new OrgMember
                {
                    Name = HtmlEntity.DeEntitize(orgMemberNode.SelectSingleNode("./a//span[contains(concat(' ', normalize-space(@class), ' '), ' name ')]").InnerText).Trim(),
                    Handle = HtmlEntity.DeEntitize(orgMemberNode.SelectSingleNode("./a//span[contains(concat(' ', normalize-space(@class), ' '), ' nick ')]").InnerText).Trim(),
                    AvatarUrl = imgNode != null ? imgNode.GetAttributeValue("src", string.Empty) : null
                };
            }
            catch (Exception e) {
                throw new Exception("Unable to parse org member. Probably the HTML has changed.", e);
            }
        }

        private readonly HtmlDocument _doc = new HtmlDocument();
    }
}