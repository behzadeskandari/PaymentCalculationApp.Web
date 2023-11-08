namespace OvertimePolicies
{
    public class OvertimePolicies
    {
        //محاسبه اضافه کار یا تعداد ساعات و معیار زمانی مشخص
        public double CalculateOvertimeHour(int totalHoursWorked, int standardHours)
        {
            if (totalHoursWorked <= standardHours)
            {
                return 0;//اضافه کار نداریم
            }
            else
            {
                return totalHoursWorked - standardHours;
            }
        }
        // محاسبه اضافه کار با تعداد ساعات کار و نرخ اضافه کار مشخص
        public double CalculateOvertimePay(int totalHoursWorked, double overtimeRate)
        {
            return CalculateOvertimeHour(totalHoursWorked, 40) * overtimeRate;
        }

        public double CalculateOvertimePay(int RightToAttract, int BaseSalary)
        {
            return RightToAttract + BaseSalary;
        }

        /// <summary>
        ///  محاصبه اضافه کار با تعداد ساعات و معیار زمانی و نرخ اضافه کار مشخص
        /// </summary>
        /// <param name="totalHoursWorked"></param>
        /// <param name="standardHours"></param>
        /// <param name="overtimeRate"></param>
        /// <returns></returns>
        public double CalculateOvertimePay(int totalHoursWorked, int standardHours, double overtimeRate)
        {
            return CalculateOvertimeHour(totalHoursWorked, standardHours) * overtimeRate;
        }

    }

}