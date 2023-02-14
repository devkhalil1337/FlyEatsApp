namespace FlyEatsApp.Models
{
    public class BaseFilter
    {
        public int? BusinessId { get; set; }


        public DateTime? CreateDate { get; set; }
        public string? CreationDate
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

        public DateTime? ModifyDate { get; set; }
        public string? UpdateDate
        {
            get { return ModifyDate.HasValue ? ModifyDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : null; }

            set
            {
                if (value != null)
                {
                    ModifyDate = DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
        }

        public bool? IsDeleted { get; set; }
        public bool? Active { get; set; }

    }
}
