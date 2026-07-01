using System;

namespace FinanceManager.Models
{
    public class RecurringTransaction  // Повторяющаяся транзакция (подписка)
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public DateTime NextDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; } // Активна ли
        public RecurringPeriod RecurringPeriod { get; set; }
        public DateTime GetNextOccurrence()
        {
            if (IsActive == false)
            {
                return NextDate;
            }
            if (EndDate.HasValue && EndDate.Value <= DateTime.UtcNow)
            {
                IsActive = false;
                return NextDate;
            }
            if (NextDate == default || NextDate.Kind != DateTimeKind.Utc)
            {
                NextDate = DateTime.UtcNow;
            }
            if (NextDate == default)
            {
                NextDate = DateTime.UtcNow;
            }
            if (RecurringPeriod == RecurringPeriod.Daily) // Ежедневная
            {
                NextDate = NextDate.AddDays(1);
            }
            if (RecurringPeriod == RecurringPeriod.Monthly) // Ежемесячная
            {
                NextDate = NextDate.AddMonths(1);
            }
            if (RecurringPeriod == RecurringPeriod.Yearly) // Ежегодная
            {
                NextDate = NextDate.AddYears(1);
            }
            if (RecurringPeriod == RecurringPeriod.Weekly) // Еженедельная
            {
                NextDate = NextDate.AddDays(7);
            }
            if (NextDate.Kind != DateTimeKind.Utc)
            {
                NextDate = DateTime.SpecifyKind(NextDate, DateTimeKind.Utc);
            }
            if (EndDate.HasValue && NextDate > EndDate.Value)
            {
                IsActive = false;
                return NextDate;
            }
            return NextDate;
        }
    }
}