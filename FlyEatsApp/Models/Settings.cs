using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlyEatsApp.Models
{
    public class Settings:BaseFilter
    {

        public const string SETTINGS_ID_COLUMN = "SettingsId";
        public const string SETTINGS_BUSINESS_ID_COLUMN = "BusinessId";
        public const string SETTINGS_CATEGORY_ID_COLUMN = "RegisterNumber";
        public const string CATEGORY_VAT_COLUMN = "Vat";
        public const string SETTINGS_VAT_TYPE_COLUMN = "VatType";
        public const string SETTINGS_SERVICE_CHARGES_COLUMN = "ServiceCharges";
        public const string SETTINGS_DELIVERY_CHARGES_COLUMN = "DeliveryCharges";
        public const string SETTINGS_MINIMUM_ORDER_COLUMN = "MinimumOrder";
        public const string SETTINGS_AVERAGE_PREPARE_TIME_COLUMN = "AveragePrepareTime";
        public const string SETTINGS_DELIVERY_TIME_COLUMN = "DeliveryTime";
        public const string SETTINGS_IS_GUEST_LOGIN_COLUMN = "IsGuestLoginActive";
        public const string SETTINGS_IS_DELIVERY_ORDER_ACTIVE_COLUMN = "IsDeliveryOrderActive";
        public const string SETTINGS_IS_COLLECTION_ORDER_ACTIVE_COLUMN = "IsCollectionOrderActive";
        public const string SETTINGS_IS_TABLE_ORDER_ACTIVE_COLUMN = "IsTableOrderActive";
        public const string SETTINGS_CREATE_DATE_COLUMN = "CreationDate";
        public const string SETTINGS_UPDATE_DATE_COLUMN = "UpdateDate";

        public int? SettingsId { get; set; }
        public string? RegisterNumber { get; set; }
        public decimal? Vat { get; set; }   
        public string? VatType { get; set; }    
        public decimal? ServiceCharges {get; set; }
        public decimal? DeliveryCharges { get; set; }
        public decimal? MinimumOrder { get; set; }
        public decimal? AveragePrepareTime { get; set; }
        public decimal? DeliveryTime { get; set; }
        public bool? IsGuestLoginActive { get; set; }
        public bool? IsDeliveryOrderActive { get; set; }
        public bool? IsCollectionOrderActive { get; set; }
        public bool? IsTableOrderActive { get; set; }


        public static Settings ExtractObject(DataRow dataRow)
        {
            var newObject = new Settings();
            newObject.SettingsId = Convert.ToInt32(dataRow[SETTINGS_ID_COLUMN]);
            newObject.BusinessId = Convert.ToInt32(dataRow[SETTINGS_BUSINESS_ID_COLUMN]);
            newObject.RegisterNumber = dataRow[SETTINGS_CATEGORY_ID_COLUMN] == DBNull.Value ? "" : Convert.ToString(dataRow[SETTINGS_CATEGORY_ID_COLUMN]);
            newObject.Vat = dataRow[CATEGORY_VAT_COLUMN] == DBNull.Value ? 0 : Convert.ToDecimal(dataRow[CATEGORY_VAT_COLUMN]);
            newObject.VatType = dataRow[SETTINGS_VAT_TYPE_COLUMN] == DBNull.Value ? "" : Convert.ToString(dataRow[SETTINGS_VAT_TYPE_COLUMN]).Replace(" ", "");
            newObject.ServiceCharges = dataRow[SETTINGS_SERVICE_CHARGES_COLUMN] == DBNull.Value ? 0 : Convert.ToDecimal(dataRow[SETTINGS_SERVICE_CHARGES_COLUMN]);
            newObject.DeliveryCharges = dataRow[SETTINGS_DELIVERY_CHARGES_COLUMN] == DBNull.Value ? 0 : Convert.ToDecimal(dataRow[SETTINGS_DELIVERY_CHARGES_COLUMN]);
            newObject.MinimumOrder = dataRow[SETTINGS_MINIMUM_ORDER_COLUMN] == DBNull.Value ? 0 : Convert.ToDecimal(dataRow[SETTINGS_MINIMUM_ORDER_COLUMN]);
            newObject.AveragePrepareTime = dataRow[SETTINGS_AVERAGE_PREPARE_TIME_COLUMN] == DBNull.Value ? 0 : Convert.ToDecimal(dataRow[SETTINGS_AVERAGE_PREPARE_TIME_COLUMN]);
            newObject.DeliveryTime = dataRow[SETTINGS_DELIVERY_TIME_COLUMN] == DBNull.Value ? 0 : Convert.ToDecimal(dataRow[SETTINGS_DELIVERY_TIME_COLUMN]);
            newObject.CreateDate = dataRow[SETTINGS_CREATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[SETTINGS_CREATE_DATE_COLUMN]);
            newObject.ModifyDate = dataRow[SETTINGS_UPDATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[SETTINGS_UPDATE_DATE_COLUMN]);
            newObject.IsGuestLoginActive = dataRow[SETTINGS_IS_GUEST_LOGIN_COLUMN] == DBNull.Value ? false : Convert.ToBoolean(dataRow[SETTINGS_IS_GUEST_LOGIN_COLUMN]);
            newObject.IsDeliveryOrderActive = dataRow[SETTINGS_IS_DELIVERY_ORDER_ACTIVE_COLUMN] == DBNull.Value ? false : Convert.ToBoolean(dataRow[SETTINGS_IS_DELIVERY_ORDER_ACTIVE_COLUMN]);
            newObject.IsCollectionOrderActive = dataRow[SETTINGS_IS_COLLECTION_ORDER_ACTIVE_COLUMN] == DBNull.Value ? false : Convert.ToBoolean(dataRow[SETTINGS_IS_COLLECTION_ORDER_ACTIVE_COLUMN]);
            newObject.IsTableOrderActive = dataRow[SETTINGS_IS_TABLE_ORDER_ACTIVE_COLUMN] == DBNull.Value ? false : Convert.ToBoolean(dataRow[SETTINGS_IS_TABLE_ORDER_ACTIVE_COLUMN]);
            return newObject;
        }


    }
}
