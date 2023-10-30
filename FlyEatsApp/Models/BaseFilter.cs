using System.Globalization;

namespace FlyEatsApp.Models
{
    public class BaseFilter
    {
        public int? BusinessId { get; set; }


        public DateTime? CreateDate { get; set; }
        private string? CreationDate
        {
            get
            {
                return CreateDate.HasValue ? CreateDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : null;
            }

            set
            {
                if (value != null)
                {
                    CreateDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
        }

 
        public DateTime? UpdateDate { get; set; }
        public string? ModifyDate
        {
            get
            {
                return UpdateDate.HasValue ? UpdateDate.Value.ToString("yyyy-MM-ddTHH:mm:ss") : null;
            }

            set
            {
                if (!string.IsNullOrWhiteSpace(value) && DateTime.TryParseExact(value, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                {
                    UpdateDate = parsedDate;
                }
                else
                {
                    // Handle invalid date format or empty strings
                    UpdateDate = null;
                }
            }
        }

        public bool? IsDeleted { get; set; }
        public bool? Active { get; set; }

    }
}
