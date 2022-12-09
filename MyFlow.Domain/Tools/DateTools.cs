namespace MyFlow.Domain.Tools
{
    public static class DateUtils
    {
        /// <summary>
        ///  是否大於或等於 inputDate
        /// </summary>
        /// <param name="self"></param>
        /// <param name="inputDate"></param>
        /// <returns>bool(是/否)</returns>
        public static bool AfterOrEqual(this DateTime self, DateTime inputDate)
        {

            if (!(DateTime.Compare(self, inputDate) < 0))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///  是否大於 Date2
        /// </summary>
        /// <param name="inputDate1"></param>
        /// <param name="inputDate2"></param>
        /// <returns>bool(是/否)</returns>
        public static bool After(this DateTime self, DateTime inputDate)
        {

            if (DateTime.Compare(self, inputDate) > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///  是否小於或等於 inputDate
        /// </summary>
        /// <param name="self"></param>
        /// <param name="inputDate"></param>
        /// <returns>bool(是/否)</returns>
        public static bool BeforeOrEqual(this DateTime self, DateTime inputDate)
        {

            if (!(DateTime.Compare(self, inputDate) > 0))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///  是否小於 inputDate
        /// </summary>
        /// <param name="self"></param>
        /// <param name="inputDate"></param>
        /// <returns>bool(是/否)</returns>
        public static bool Before(this DateTime self, DateTime inputDate)
        {

            if (DateTime.Compare(self, inputDate) < 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判斷傳入Source是否介於傳入區間
        /// </summary>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool Between(this DateTime self, DateTime StartTime, DateTime EndTime)
        {
            bool result = false;

            if (StartTime <= self && EndTime >= self)
            {
                result = true;
            }
            return result;
        }
    }
}