using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlyEatsApp.Models
{
    public class OrderDetailSelectionRelation: BaseFilter
    {
        public const string ORDER_DETAILS_ID_COLUMN = "OrderDetailsId";
        public const string BUSINESS_ID_COLUMN = "BusinessId";
        public const string SELECTION_ID_COLUMN = "SelectionId";
        public const string CHOICES_ID_COLUMN = "ChoicesId";
        public const string CHOICE_NAME_COLUMN = "ChoiceName";
        public const string CHOICE_PRICE_COLUMN = "ChoicePrice";

        public int OrderDetailsId { get; set; }
        public int SelectionId { get; set; }
        public int ChoicesId { get; set; }
        public string ChoiceName { get; set; }
        public decimal ChoicePrice { get; set; }

        public static OrderDetailSelectionRelation ExtractObject(DataRow dataRow)
        {
            var newObject = new OrderDetailSelectionRelation();
            newObject.OrderDetailsId = Convert.ToInt32(dataRow[ORDER_DETAILS_ID_COLUMN]);
            newObject.BusinessId = Convert.ToInt32(dataRow[BUSINESS_ID_COLUMN]);
            newObject.SelectionId = Convert.ToInt32(dataRow[SELECTION_ID_COLUMN]);
            newObject.ChoicesId = Convert.ToInt32(dataRow[CHOICES_ID_COLUMN]);
            newObject.ChoiceName = dataRow[CHOICE_NAME_COLUMN].ToString();
            newObject.ChoicePrice = Convert.ToDecimal(dataRow[CHOICE_PRICE_COLUMN]);
            return newObject;
        }

    }
}
