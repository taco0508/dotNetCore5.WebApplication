namespace dotNetCore5.WebApi.ViewModels
{
    /// <summary>
    /// 客戶 ViewModel
    /// </summary>
    public class CustomersViewModel
    {
        /// <summary>
        /// 客戶編號
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// 公司名稱
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 聯絡人
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// 聯絡人職稱
        /// </summary>
        public string ContactTitle { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 地區
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// 郵遞區號
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// 國別
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// NUM_3
        /// </summary>
        public string NUM_3 { get; set; }

        /// <summary>
        /// ALPHA_2
        /// </summary>
        public string ALPHA_2 { get; set; }

        /// <summary>
        /// ALPHA_3
        /// </summary>
        public string ALPHA_3 { get; set; }

        /// <summary>
        /// 手機號碼
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 傳真號碼
        /// </summary>
        public string Fax { get; set; }
    }
}
