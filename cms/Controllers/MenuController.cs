using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cms.Models;
using System.Data;
using Newtonsoft.Json;
namespace cms.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Menu/
        CMSEntities db = new CMSEntities();
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Add(int? parent_id)
        {
            ViewBag.parent_id = parent_id;
            //ViewBag.depth = depth;            
            return View();
        }
        public ActionResult Edit(int id)
        {
            menu mn = db.menus.Find(id);
            if (mn != null)
            {
                ViewBag.id = id;
                ViewBag.name = mn.name;
                ViewBag.show_on_menu = mn.show_on_menu;
                ViewBag.parent_id= mn.parent_id;
                ViewBag.type = mn.type;
                ViewBag.order_no = mn.order_no;
            }
            return View();
        }
        public string Edit_Update(int id, string name,int type, byte show_on_menu, int? parent_id, int order_no)
        {
            try
            {
                //string query="update category set name="
                menu mn = db.menus.Find(id);
                mn.name = name;
                mn.type = type;
                mn.show_on_menu = show_on_menu;
                mn.parent_id= parent_id;                
                mn.order_no = order_no;
                db.Entry(mn).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "1";
        }
        public string getAllNodeMenu(int idselected, int parent_id)
        {
            string select = "<select id=parent_id name=parent_id><option value=-1 depth=0>" + Lang.menu_root_name + "</option>";
            var p = (from q in db.menus where q.parent_id == -1 select new { id = q.id, name = q.name,parent_id=q.parent_id, show_on_menu = q.show_on_menu, order_no = q.order_no }).OrderBy(o => o.order_no).ToList();
            for (int i = 0; i < p.Count; i++)
            {
                if (p[i].id != parent_id)
                {
                    select += "<option value=\"" + p[i].id + "\" >&nbsp;&nbsp;" + p[i].name + "</option>";
                }
                else
                {
                    select += "<option value=\"" + p[i].id + "\" selected>&nbsp;&nbsp;" + p[i].name + "</option>";
                }
                select += getAllChild(p[i].id, idselected, parent_id, 1);
            }
            select += "</select>";
            return select;
        }
        public string getAllChild(int id, int idselected, int parent_id, int DEPTH)
        {
            string option = "";
            var p1 = (from q in db.menus where q.parent_id == id select new { id = q.id, name = q.name, parent_id = q.parent_id, show_on_menu=q.show_on_menu, order_no = q.order_no }).OrderBy(o => o.order_no).ToList();
            for (int i = 0; i < p1.Count; i++)
            {
                if (p1[i].id != idselected)
                {
                    string spacer = "";
                    for (int j = 0; j < DEPTH; j++)
                    {
                        spacer += "&nbsp;&nbsp;&nbsp;&nbsp;";
                    }
                    if (p1[i].id != parent_id)
                    {
                        option += "<option value=\"" + p1[i].id + "\" >" + spacer + "-" + p1[i].name + "</option>";
                    }
                    else
                    {
                        option += "<option value=\"" + p1[i].id + "\" selected>" + spacer + "-" + p1[i].name + "</option>";
                    }
                    option += getAllChild(p1[i].id, idselected, parent_id, DEPTH + 1);
                }
                //else {
                //    option += "<option value=\"" + p1[i].id + "\" depth=\"" + p1[i].depth + "\" selected>-" + p1[i].name + "</option>";
                //}
            }
            return option;
        }
        public string Add_New(string name,int type, byte show_on_menu, int? parent_id)
        {
            try
            {
                if (parent_id == null) parent_id = -1;
                //if (depth == null) depth = -1;
                //Get max order no
                int? max_order_no = db.menus.Where(o => o.parent_id == parent_id).Max(o => o.order_no);
                if (max_order_no == 0 || max_order_no == null)
                    max_order_no = 1;
                else
                    max_order_no = max_order_no + 1;

                menu mn = new menu();
                mn.name = name;
                mn.type = type;
                mn.show_on_menu = show_on_menu;
                mn.parent_id = parent_id;
                //cate.depth = depth + 1;
                mn.order_no = max_order_no;
                db.menus.Add(mn);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "1";
        }
        public string Delete(int id)
        {
            try
            {
                var nim = db.news_item_menu.Where(o=>o.menu_id==id).FirstOrDefault();
                if (nim == null)
                {
                    string query = "delete from menu where id=" + id;
                    db.Database.ExecuteSqlCommand(query);
                }
                else {
                    return "-1";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "1";
        }
        public string getRootMenuList()
        {
            var p = (from q in db.menus where q.parent_id == -1 select new { id = q.id, name = q.name, parent_id = q.parent_id, show_on_menu=q.show_on_menu, order_no = q.order_no }).OrderBy(o => o.order_no).ToList();
            return JsonConvert.SerializeObject(p);
        }
        //Lấy ra các node mà có parent=id
        public string getAllChildOfNode(int id)
        {
            var p = (from q in db.menus where q.parent_id == id select new { id = q.id, name = q.name, parent_id = q.parent_id, show_on_menu=q.show_on_menu, order_no = q.order_no }).OrderBy(o => o.order_no).ToList();
            return JsonConvert.SerializeObject(p);
        }
        public string moveMenuAndSort(int fromid, int toid)
        {
            try
            {
                var fromidparent = db.menus.Where(o => o.id == fromid).FirstOrDefault().parent_id;
                var toidparent = db.menus.Where(o => o.id == toid).FirstOrDefault().parent_id;
                if (fromidparent == toidparent)
                {
                    var order_from = db.menus.Where(o => o.id == fromid).FirstOrDefault().order_no;
                    var order_to = db.menus.Where(o => o.id == toid).FirstOrDefault().order_no;
                    //Tăng các order no của các category sau category được chèn lên 1
                    string query = "update menu set order_no=order_no+1 where parent_id=" + fromidparent + " and order_no<=" + order_from + "  and order_no>=" + order_to;
                    db.Database.ExecuteSqlCommand(query);
                    //set order_no cho cate này
                    query = "update menu set order_no=" + order_to + " where id=" + fromid;
                    db.Database.ExecuteSqlCommand(query);
                }
                else
                {
                    //Đẩy thứ tự xếp hạng của các category cùng nhánh lên 1
                    string query = "update menu set order_no=order_no+1 where parent_id=" + toid + " and order_no>=1";
                    db.Database.ExecuteSqlCommand(query);
                    query = "update menu set parent_id=" + toid + ",order_no=1 where id=" + fromid;
                    db.Database.ExecuteSqlCommand(query);
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "1";
        }
    }
}
