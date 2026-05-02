using System.Collections.Generic;
using System.Linq;

namespace ERPsystem.Domain.Common
{
    /// <summary>
    /// صنف أساسي يمثل كائنات القيمة (Value Objects) في معمارية تصميم النطاق (DDD).
    /// كائنات القيمة ليس لها هوية مستقلة (Id)، بل تُعرف بقيم خصائصها، وتستخدم لتمثيل المفاهيم مثل (المال، العنوان).
    /// </summary>
    public abstract class ValueObject
    {
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        /// <summary>
        /// يُرجع المكونات (الخصائص) الأساسية التي تحدد حالة هذا الكائن للمقارنة.
        /// </summary>
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }
            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}
