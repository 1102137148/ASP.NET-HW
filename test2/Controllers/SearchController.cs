using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace test2.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        Models.CodeService codeService = new Models.CodeService();
        Models.OrderService orderService = new Models.OrderService();
        Models.OrderDetailService orderDetailService = new Models.OrderDetailService();

        /// <summary>
        /// 訂單查詢系統
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchOrder()
        {
            ViewBag.EmpCodeData = this.codeService.GetEmp();
            ViewBag.ShipCodeData = this.codeService.GetShipper();
            return View();
        }


        /// <summary>
        /// 訂單查詢結果
        /// </summary>
        /// <param name="sorder"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult SearchOrder(Models.SearchOrderArg sorder)
        {
            ViewBag.EmpCodeData = this.codeService.GetEmp();
            ViewBag.ShipCodeData = this.codeService.GetShipper();
            ViewBag.SearchResult = orderService.GetOrderByCondtioin(sorder);
            return View("SearchOrder");
        }
        /// <summary>
        /// 新增訂單畫面
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult InsertOrder()
        {
            ViewBag.CustCodeData = this.codeService.GetCustomer();
            ViewBag.EmpCodeData = this.codeService.GetEmp();
            ViewBag.ShipCodeData = this.codeService.GetShipper();
            ViewBag.ProductCodeData = this.codeService.GetProduct();
            return View(new Models.Order());
        }

        /// <summary>
        /// 新增訂單Action
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult InsertOrder(Models.Order order)
        {
            if (ModelState.IsValid)
            {
                int a = orderService.InsertOrder(order);
                orderDetailService.InsertOrderDetail(order.OrderDetails, a);
                return RedirectToAction("SearchOrder");

            }
            return View(order);
            //return View();
        }

        /// <summary>
        /// 更新訂單畫面
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public ActionResult Update(string id)
        {
            Models.Order order = this.orderService.GetOrderById(id);
            order.OrderDetails = this.orderDetailService.GetOrderByOrderId(id);
            ViewBag.EmpCodeData = this.codeService.GetEmp();
            ViewBag.ShipCodeData = this.codeService.GetShipper();
            ViewBag.CustCodeData = this.codeService.GetCustomer();
            ViewBag.OrderDate = string.Format("{0:yyyy-MM-dd}", order.OrderDate);
            ViewBag.RequireDdate = string.Format("{0:yyyy-MM-dd}", order.RequiredDate);
            ViewBag.ShippedDate = string.Format("{0:yyyy-MM-dd}", order.ShippedDate);
            ViewBag.ProductCodeData = this.codeService.GetProduct();
            ViewBag.OrderData = order;
            return View(order);
        }

        /// <summary>
        /// 更新訂單
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(Models.Order order)
        {
            orderService.UpdateOrder(order);
            orderDetailService.UpdateOrderDeail(order.OrderDetails, order.OrderId);
            ViewBag.EmpCodeData = this.codeService.GetEmp();
            ViewBag.ShipCodeData = this.codeService.GetShipper();
            return RedirectToAction("SearchOrder"); ;
        }

        /// <summary>
        /// 刪除訂單
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult DeleteOrder(string orderId)
        {
            try
            {
                orderDetailService.DeleteOrderDetail(orderId);
                orderService.DeleteOrderById(orderId);                
                return this.Json(true);
            }
            catch (Exception)
            {
                return this.Json(false);
            }
        }

        /// <summary>
        /// 依訂單ID跟產品ID刪除訂單明細
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPost()]
        public JsonResult DeleteOrderDetail(string orderId, string productId)
        {
            try
            {
                orderDetailService.DeleteOrderDetailByProductId(orderId, productId);
                return this.Json(true);
            }
            catch (Exception)
            {
                return this.Json(false);
            }
        }

        /// <summary>
        /// 取得系統時間
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSysDate()
        {
            return PartialView("_SysDatePartial");
        }


    }
}