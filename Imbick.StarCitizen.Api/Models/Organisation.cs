
namespace Imbick.StarCitizen.Api.Models {
    using System;
    using System.Diagnostics;

    public enum Size {
        Small,
        Medium,
        Large
    }

    public enum Commitment {
        Casual,
        Regular,
        Hardcore
    }

    public enum Archetype {
        Organization,
        Corporation,
        Pmc,
        Faith,
        Syndicate
    }

    public enum Activity {
        BountyHunting = 9,
        Engineering = 7,
        Exploration = 12,
        Freelancing = 6,
        Infiltration = 5,
        Piracy = 10,
        Resources = 4,
        Scouting = 3,
        Security = 8,
        Smuggling = 2,
        Social = 13,
        Trading = 11,
        Transport = 1
    }

    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class Organisation
        : IEquatable<Organisation> {
        public string Name { get; set; }
        public string Symbol { get; set; }

        public string ThumbnailUrl { get; set; }

        public Archetype Archetype { get; set; }

        public Commitment Commitment { get; set; }

        public string Language { get; set; }

        public bool IsRecruiting { get; set; }

        public bool IsRolePlay { get; set; }

        public long MemberCount { get; set; }

        public bool Equals(Organisation other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && string.Equals(Symbol, other.Symbol) &&
                   string.Equals(ThumbnailUrl, other.ThumbnailUrl) && Archetype == other.Archetype &&
                   Commitment == other.Commitment && string.Equals(Language, other.Language) &&
                   IsRecruiting == other.IsRecruiting && IsRolePlay == other.IsRolePlay &&
                   MemberCount == other.MemberCount;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Organisation) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Symbol != null ? Symbol.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ThumbnailUrl != null ? ThumbnailUrl.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) Archetype;
                hashCode = (hashCode * 397) ^ (int) Commitment;
                hashCode = (hashCode * 397) ^ (Language != null ? Language.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsRecruiting.GetHashCode();
                hashCode = (hashCode * 397) ^ IsRolePlay.GetHashCode();
                hashCode = (hashCode * 397) ^ MemberCount.GetHashCode();
                return hashCode;
            }
        }
    }
}