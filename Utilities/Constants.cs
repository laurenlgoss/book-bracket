using book_bracket.Models.Enums;

namespace book_bracket.Utilities
{
    public static class Constants
    {
        /// <summary>
        /// All months of the year in chronological order.
        /// </summary>
        public static List<Month> Months => 
            [
                Month.January,
                Month.February,
                Month.March,
                Month.April,
                Month.May,
                Month.June,
                Month.July,
                Month.August,
                Month.September,
                Month.October,
                Month.November,
                Month.December,
            ];
    }
}