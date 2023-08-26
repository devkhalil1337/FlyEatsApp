using System;
using System.Data;

namespace FlyEatsApp.Models
{
    public class Menus
    {
        public const string MENU_ID_COLUMN = "Id";
        public const string MENU_BUSINESS_ID_COLUMN = "BusinessId";
        public const string MENU_NAME_COLUMN = "MenuName";
        public const string MENU_URL_COLUMN = "MenuUrl";
        public const string MENU_ORDER_BY_COLUMN = "OrderBy";
        public const string MENU_ACTIVE_COLUMN = "isActive";

        public int Id { get; set; }
        public long BusinessId { get; set; }
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public int OrderBy { get; set; }
        public bool isActive { get; set; }

        public static Menus ExtractObject(DataRow dataRow)
        {
            var newObject = new Menus();
            newObject.Id = Convert.ToInt32(dataRow[MENU_ID_COLUMN]);
            newObject.BusinessId = Convert.ToInt64(dataRow[MENU_BUSINESS_ID_COLUMN]);
            newObject.MenuName = Convert.ToString(dataRow[MENU_NAME_COLUMN]);
            newObject.MenuUrl = Convert.ToString(dataRow[MENU_URL_COLUMN]);
            newObject.OrderBy = Convert.ToInt32(dataRow[MENU_ORDER_BY_COLUMN]);
            newObject.isActive = Convert.ToBoolean(dataRow[MENU_ACTIVE_COLUMN]);

            return newObject;
        }
    }
}
