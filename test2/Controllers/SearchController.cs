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
        /// <summary>
        /// 訂單查詢系統
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchOrder()
        {
            ViewBag.EmpCodeData = this.codeService.GetEmp(-1);
            ViewBag.ShipCodeData = this.codeService.GetShipper(-1);
            return View();
        }



        [HttpPost()]
        public ActionResult SearchOrder(Models.SearchOrderArg sorder)
        {
            //Models.OrderService orderService = new Models.OrderService();
            //ViewBag.EmpCodeData = this.codeService.GetEmp(-1);
            //ViewBag.EmpResult = orderService.GetOrderByCondtioin(sorder);

            //ViewBag.ShipCodeData = this.codeService.GetShipper(-1);
            //ViewBag.ShipperResult = orderService.GetOrderByCondtioin(sorder);

            //Models.OrderService orderservice = new Models.OrderService();
            //List<Models.Order> result = orderservice.GetOrderByCondtioin(sorder);
            //ViewBag.searchresult = result;
            ViewBag.EmpCodeData = this.codeService.GetEmp(-1);
            ViewBag.ShipCodeData = this.codeService.GetShipper(-1);
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
            ViewBag.CustCodeData = this.codeService.GetCustomer(-1);
            ViewBag.EmpCodeData = this.codeService.GetEmp(-1);
            ViewBag.ProductCodeData = this.codeService.GetProduct();
            ViewBag.ShipCodeData = this.codeService.GetShipper(-1);
            return View(new Models.Order());
        }

        /// <summary>
        /// 新增訂單
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult InsertOrder(Models.Order order)
        {
            if (ModelState.IsValid)
            {
                int a = orderService.InsertOrder(order);

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
            ViewBag.EmpCodeData = this.codeService.GetEmp(Convert.ToInt32(order.EmpId));
            ViewBag.ShipCodeData = this.codeService.GetShipper(Convert.ToInt32(order.ShipperId));
            ViewBag.CustCodeData = this.codeService.GetCustomer(Convert.ToInt32(order.CustId));
            ViewBag.OrderDate = string.Format("{0:yyyy-MM-dd}", order.OrderDate);
            ViewBag.RequireDdate = string.Format("{0:yyyy-MM-dd}", order.RequiredDate);
            ViewBag.ShippedDate = string.Format("{0:yyyy-MM-dd}", order.ShippedDate);
            ViewBag.OrderData = order;
            return View(new Models.Order());
        }

        /// <summary>
        /// 更新訂單
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(Models.Order order)
        {
            orderService.UpdateOrder(order);
            ViewBag.EmpCodeData = this.codeService.GetEmp(-1);
            ViewBag.ShipCodeData = this.codeService.GetShipper(-1);
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
                Models.OrderService orderService = new Models.OrderService();
                orderService.DeleteOrderById(orderId);
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