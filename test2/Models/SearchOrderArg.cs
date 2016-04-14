using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test2.Models
{
    public class SearchOrderArg
    {
        /// <summary>
        /// 訂單編號
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 客戶名稱
        /// </summary>
        public string CustName { get; set; }

        /// <summary>
        /// 業務(員工)代號
        /// </summary>
        public int EmpId { get; set; }

        /// <summary>
        /// 業務(員工姓名)
        /// </summary>
        public string EmpName { get; set; }

        /// <summary>
        /// 出貨公司名稱
        /// </summary>
        public string ShipperName { get; set; }

        /// <summary>
        /// 出貨公司代號
        /// </summary>
        public String ShipperId { get; set; }

        /// <summary>
        /// 訂單日期
        /// </summary>
        public String OrderDate { get; set; }

        /// <summary>
        /// 需要日期
        /// </summary>
        public String RequireDdate { get; set; }

        /// <summary>
        /// 出貨日期
        /// </summary>
        public String ShippedDate { get; set; }
    }
}