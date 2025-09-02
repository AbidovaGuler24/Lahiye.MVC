using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearning.Core.Helpers.Exictance
{
    public static class DateTimeExtensions
    {
        public static DateTime ToAzerbaijanTime(this DateTime utcDate)
        {
            return utcDate.Kind == DateTimeKind.Utc
                ? utcDate.AddHours(4)
                : DateTime.SpecifyKind(utcDate, DateTimeKind.Utc).AddHours(4);
        }
    }
}
