
namespace Imbick.StarCitizen.Api.Models
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class OrgMember
        : IEquatable<OrgMember>
    {
        public string Name { get; set; }
        public string Handle { get; set; }

        public string AvatarUrl { get; set; }

        public bool Equals(OrgMember other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && string.Equals(Handle, other.Handle) &&
                   string.Equals(AvatarUrl, other.AvatarUrl);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((OrgMember)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Handle != null ? Handle.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AvatarUrl != null ? AvatarUrl.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}