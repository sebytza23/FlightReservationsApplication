namespace FlightReservationsApplication.Utils
{
    public class PaginationHelper
    {
        public static List<int> GetPageNumbers(int currentPage, int totalPages)
        {
            List<int> pageNumbers = new List<int>();
            int delta = 2;
            int left = currentPage - delta;
            int right = currentPage + delta + 1;
            int last = totalPages;

            for (int i = 1; i <= last; i++)
            {
                if (i == 1 || i == last || (i >= left && i < right))
                {
                    pageNumbers.Add(i);
                }
            }

            int? l = null;
            List<int> pageNumbersWithDots = new List<int>();
            foreach (var i in pageNumbers)
            {
                if (l != null)
                {
                    if (Convert.ToInt32(i) - l == 2)
                    {
                        pageNumbersWithDots.Add((int)l + 1);
                    }
                    else if (Convert.ToInt32(i) - l != 1)
                    {
                        pageNumbersWithDots.Add(-1);
                    }
                }
                pageNumbersWithDots.Add(i);
                l = Convert.ToInt32(i);
            }

            return pageNumbersWithDots;
        }
    }
}
