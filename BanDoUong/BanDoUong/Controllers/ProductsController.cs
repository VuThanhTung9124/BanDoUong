using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BanDoUong.Models;
using System.Collections; 
using System.Collections.Generic;
using System.Security.Cryptography; 

namespace BanDoUong.Controllers
{
    public class ProductsController : Controller
    {
        private DoUongDb db = new DoUongDb();









        // GET: Products
        public ActionResult Index()
        {
            List<Product> listSP = new List<Product>();
            foreach (var p in db.Products.ToList())
            {
                listSP.Add(p);
            }

            Session["GioHang"] = listSP;
            return View(listSP);

            ViewBag.ThongBaoEmail = "Cảm ơn bạn đã gửi email cho chúng tôi!!!";
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }




       
        public ActionResult GioHang(int? id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("DangNhap", "Accounts");
            }
            else
            {

            if (id == null)
            {
                return RedirectToAction("Index");
            }


            Product product = db.Products.Find(id);


            if (product == null)
            {
                return HttpNotFound();
            }


            List<CartItem> cart = Session["Cart"] as List<CartItem>;


            if (cart == null)
            {
                cart = new List<CartItem>();
            }



            CartItem existing = cart.FirstOrDefault(p => p.ProductId == product.product_id);



            if (existing != null)
            {
                existing.Quantity++;
            }

            else
            {
                CartItem item = new CartItem
                {
                    ProductId = product.product_id,  
                    Name = product.name,              
                    Thumbnail = product.thumbnail,   
                    Price = product.price,          
                    Quantity = 1                      
                };

                cart.Add(item);
            }

            Session["Cart"] = cart;

        
            return RedirectToAction("XemGioHang");
            }

            
        }



        public ActionResult XemGioHang()
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
            }
            ViewBag.TongTien = Session["tong_tien"];
            return View(cart);
        }



        [HttpPost]
        public ActionResult XoaGioHang(int productId)
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            if (cart != null)
            {
                cart.RemoveAll(a => a.ProductId == productId);
                Session["Cart"] = cart;
            }
            return RedirectToAction("XemGioHang");
        }

  

        [HttpGet]
        public ActionResult ThanhToan(int[] SelectedIds)
        {
            List<CartItem> cartItems = Session["Cart"] as List<CartItem>;
            string thongtin = "";

            var selectedItems = (from a in cartItems
                                 where SelectedIds.Contains(a.ProductId)
                                 select a).ToList();



            foreach (var data in selectedItems)
            {
                thongtin += $"Tên sản phẩm: " + data.Name + "<br>" +"Số lượng: " +data.Quantity + "<br>" +"Tổng tiền: "+ ((data.Price * data.Quantity).ToString("N0")) + " <br> " + "--------------------<br>";
            }

            
            decimal tongTien = selectedItems.Sum(x => x.Price * x.Quantity);
            Session["tong_tien"] = tongTien.ToString("N0");
            Session["thong_tin"] = thongtin;
            Session["dat_hang"] = cartItems;

          
            return View(selectedItems);
        }


        public ActionResult MuaNgay(int id)
        {

            if (Session["User"] == null)
            {
                return RedirectToAction("DangNhap", "Accounts");
            }

            else
            {
  var query = (from a in db.Products
                        where a.product_id == id
                        select a).FirstOrDefault();
                Session["MuaMotSanPham"] = query.product_id;
                string thongtin = "";
                thongtin += $"Tên sản phẩm: " + query.name + "<br>" + "Số lượng: " + 1 + "<br>" + "Tổng tiền: " + query.price.ToString("N0") + " <br> " + "--------------------<br>";
                
                Session["tong_tien"] = query.price.ToString("N0");
                Session["thong_tin"] = thongtin;
                Session["dat_hang"] = query;

                return View(query);
            }

           
          

        }

        public ActionResult MuaNgayMotSanPham()
        {
            return View();
        }





        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
