using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumerableUniqueness
{
    public partial class CustomWidget : IEquatable<CustomWidget>
    {
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = WidgetType.GetHashCode();
                hashCode = (hashCode * 397) ^ Amount.GetHashCode();
                hashCode = (hashCode * 397) ^ Description.GetHashCode();
                return hashCode;
            }
        }

        // implement all the following methods to satisfy IEquatable and other
        // magic behavior expected by the framework
        public bool Equals(CustomWidget? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return WidgetType.Equals(other.WidgetType) && Amount == other.Amount && Description == other.Description;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CustomWidget)obj);
        }

        public static bool operator ==(CustomWidget? left, CustomWidget? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CustomWidget? left, CustomWidget? right)
        {
            return !Equals(left, right);
        }

        // ========================================================

        // implement this class to get a custom comparer, although it
        // doesn't have to be a nested class or a partial class, I
        // just prefer this for organization if I'm allowed to modify
        // the original source 

        public class CaseInsensitiveComparer : IEqualityComparer<CustomWidget>
        {
            // make people use the static instance for reasons
            private CaseInsensitiveComparer() { }

            public bool Equals(CustomWidget x, CustomWidget y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.WidgetType.Equals(y.WidgetType) && x.Amount == y.Amount && string.Equals(x.Description, y.Description, StringComparison.OrdinalIgnoreCase);
            }


            public static IEqualityComparer<CustomWidget> Instance { get; } = new CaseInsensitiveComparer();
        }

        public class DefaultComparer : IEqualityComparer<CustomWidget>
        {
            // make people use the static instance for reasons
            private DefaultComparer() { }

            public bool Equals(CustomWidget x, CustomWidget y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.WidgetType.Equals(y.WidgetType) && x.Amount == y.Amount && x.Description == y.Description;
            }


            public static IEqualityComparer<CustomWidget> Instance { get; } = new DefaultComparer();
        }
    }
}
