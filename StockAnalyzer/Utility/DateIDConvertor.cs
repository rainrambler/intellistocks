using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Utility
{
    class DateIDConvertor
    {
        public int MinDateId
        {
            get;
            set;
        }

        public int MaxDateId
        {
            get;
            set;
        }

        public void Init(ICollection<DateTime> allDates)
        {
            idToDate_.Clear();
            dateToId_.Clear();

            List<DateTime> dates = new List<DateTime>();
            dates.AddRange(allDates);
            dates.Sort();

            int currentId = 0;

            foreach (DateTime dt in dates)
            {
                idToDate_.Add(currentId, dt);
                dateToId_.Add(dt, currentId);

                currentId++;
            }

            List<int> ids = new List<int>(idToDate_.Keys);
            MinDateId = ids.Min();
            MaxDateId = ids.Max();
        }

        public void InitByDelta(ICollection<DateTime> allDates)
        {
            idToDate_.Clear();
            dateToId_.Clear();

            DateTime minDate = allDates.Min();

            foreach (DateTime dt in allDates)
            {
                int delta = (dt - minDate).Days;
                idToDate_.Add(delta, dt);
                dateToId_.Add(dt, delta);
            }
        }

        public int GetDateID(DateTime dt)
        {
            if (!dateToId_.ContainsKey(dt))
            {
                return INVALID_DATE_ID;
            }

            return dateToId_[dt];
        }

        public DateTime GetIDDate(int val)
        {
            if (!idToDate_.ContainsKey(val))
            {
                return DateTime.MinValue;
            }

            return idToDate_[val];
        }

        public const int INVALID_DATE_ID = -1;

        Dictionary<int, DateTime> idToDate_ = new Dictionary<int, DateTime>();
        Dictionary<DateTime, int> dateToId_ = new Dictionary<DateTime, int>();
    }
}
