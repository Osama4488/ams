using AMS;
using AMS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AMS.Controllers
{
    public class HomeController : Controller
    {
        attendanceEntities e = new attendanceEntities();
       




        public ActionResult adminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult adminLogin(Admin a)
        {
            List<Admin> list = e.Admins.ToList();
            Admin r = list.Find(p => p.id == a.id);
            Admin x = e.Admins.Find(r.id);
            if (x.pass == a.pass)
            {
                return RedirectToAction("adminPanel", "Home");
            }
            else
            {
                ViewBag.msg = "Wrong password";
            }
            return View();
        }



        public ActionResult adminPanel()
        {


            return View();
        }




        public ActionResult addEmployee()
        {

            return View();
        }

        [HttpPost]
        public ActionResult addEmployee(User u)
        {

            User t = new User();

            t.Name = u.Name;
            t.Address = u.Address;
            t.Salary = u.Salary;
            t.Email = u.Email;
            t.CNIC = u.CNIC;
            t.Password = u.Password;
            t.Contact = u.Contact;
            t.Active = u.Active;
            e.Users.Add(t);
            e.SaveChanges();
            ViewBag.msg = "Users Added";
            return RedirectToAction("adminPanel", "Home");
        }



        public ActionResult showEmployees()
        {

            return View(e.Users.ToList());
        }


        //=========================================================
        public ActionResult employeeLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult employeeLogin(User u)
        {

            set.breaktimestart = "0";
            //SqlCommand sql = new SqlCommand("INSERTTIME", connection.getcon());
            //sql.CommandType = System.Data.CommandType.StoredProcedure;
            //sql.Parameters.AddWithValue("@fk_id", u.Id);
            //sql.Parameters.AddWithValue("@status", "Logged_IN");
            //sql.Parameters.AddWithValue("@startTime", d.start_time );
            //sql.Parameters.AddWithValue("@day",o );
            //sql.ExecuteNonQuery();

            List<User> list = e.Users.ToList();
            User r = list.Find(p => p.Id == u.Id);




            User x = e.Users.Find(r.Id);
            
            //DateTime start = new DateTime(2018, 12, 9, 10, 0, 0); //10 o'clock
            //DateTime end = new DateTime(2009, 12, 10, 12, 0, 0); //12 o'clock
            //DateTime now = DateTime.Now;
           
            List<Daily_times> list1 = e.Daily_times.ToList();
            Daily_times l = list1.Find(m => m.Id == u.Id);
            if (l == null)
            {
                l = new Daily_times();
                l.Day = DateTime.Now.Day.ToString();

            }
            //Daily_time q = e.Daily_times.Find(l.Id);
            string o = System.DateTime.Now.DayOfWeek.ToString();
            if (l.Day.Trim() == o)
            {
                e.Entry(l).State = System.Data.Entity.EntityState.Modified;
                l.start_time = DateTime.Now;

                //e.Daily_times.
                //e.Daily_times.Add(l);
                e.SaveChanges();
            }


            if (x.Password == u.Password)
            {
                set.id = u.Id;
                //Session["id"] = x.Id;
                if (l.Status == "Logged-IN")
                {

                    ViewBag.msg = "User is Already Logged in";
                    // Session["id"] = x.Id;
                    x.check = x.Id;
                    set.id = x.Id;
                    set.pass = x.Password;
                    Session["LoginAttempt"] = null;


                    RedirectToAction("employeeLogin", "Home");
                }


                //if (z == "Logged-IN")
                //{
                //    ViewBag.msg = "User is Already Logged in";
                //    RedirectToAction("employeeLogin", "Home");
                //}
                else
                {
                    if (l.User == null)
                    {

                        Session["id"] = x.Id;
                        set.id = x.Id;
                        set.pass = x.Password;
                        Daily_times d = new Daily_times();
                        string a = System.DateTime.Now.DayOfWeek.ToString();
                        DateTime myDate = DateTime.Parse(DateTime.Now.TimeOfDay.ToString("hh\\:mm"));
                        d.start_time = DateTime.Parse(myDate.TimeOfDay.ToString());
                        d.Month = DateTime.Now.ToString("MMM");
                        //d.end_time = DateTime.Now;
                        d.Day = a;
                        d.Id = u.Id;

                        d.Status = "Logged-IN";
                        Session["LoginAttempt"] = null;
                        Break_Times b = new Break_Times();
                        b.breakcheck = 0;
                        e.Daily_times.Add(d);
                        e.SaveChanges();
                    }
                }
                return RedirectToAction("details", "Home",new { x.Id });


            }
            else
            {
                ViewBag.msg = "Wrong password";
            }

            return View();


        }


        public ActionResult employeeLogout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult employeeLogout(User u)
        {
            if (set.id == u.Id && set.pass == u.Password)
            {
                List<Details> data = new List<Details>();
                //  Session["id"] = u.Id;
                Daily_times d = new Daily_times();
                List<Daily_times> dd = e.Daily_times.ToList();
                for (int i = dd.Count - 1; i >= 0; i--)
                {
                    if (dd[i].Id == u.Id)
                    {
                        d = dd[i];
                        break;
                    }
                }
                set.check = true;
                string o = System.DateTime.Now.DayOfWeek.ToString();
                //DateTime myDate = DateTime.Parse(DateTime.Now.TimeOfDay.ToString("hh\\:mm"));
                //d.start_time = DateTime.Parse(myDate.TimeOfDay.ToString());
                d.end_time = DateTime.Now;
                d.Id = u.Id;
                //d.Day = o;
                //Session["LoginAttempt"] = 00;
                set.breakchecker = 1;
                d.Status = "Logged-OUT";
                Session["LoginAttempt"] = null;
                d.Day = o;

                SqlCommand sql = new SqlCommand("LOGOUTENDTIME",   connection.getcon());
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@id", d.DailyTime_id);
                sql.Parameters.AddWithValue("@endtime", d.end_time);
                sql.Parameters.AddWithValue("@status", d.Status);
                sql.ExecuteNonQuery();
                set.id = 0;
                set.pass = null;
                return RedirectToAction("details", "Home", new { u.Id });
            }
            else
            {
                ViewBag.msg = "its not your id or password";
                return View();
            }

            

        }

        public ActionResult reports()
        {


            return View();

        }
        List<string> s = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        [HttpPost]
        public ActionResult reports(User u)
        {
            Weekly_Reports y = new Weekly_Reports();
            List<Weekly_Reports> q = e.Weekly_Reports.ToList();

            bool yes = false;
            foreach(var i in q)
            {
                if (i.Day_no == DateTime.Now.Day.ToString())
                {
                    yes = true;
                    break;
                }
            }

            if (yes)
            { }
            else
            {
                List<Daily_times> list = e.Daily_times.ToList();
                Daily_times l = list.Find(m => m.Id == u.Id);
                Weekly_Reports p = new Weekly_Reports();
                p.Day_no = (s.IndexOf(l.Day.Trim()) + 1).ToString();

                p.Start = DateTime.Parse(l.start_time.ToString());
                p.End = DateTime.Parse(l.end_time.ToString());

                DateTime date1 = Convert.ToDateTime(l.start_time);
                DateTime date2 = Convert.ToDateTime(l.end_time);
                string minutes = (date2.Subtract(date1).TotalMinutes).ToString();
                p.Total_Hours = minutes;
                e.Weekly_Reports.Add(p);
                e.SaveChanges();
            }
            return RedirectToAction("showReports", "Home");

        }


        public ActionResult showReports()
        {
            

            return View(e.Weekly_Reports.ToList());
        }


        public ActionResult breakStart(int? id)
        {
            ViewBag.id = 4;
            return View();
        }
        [HttpPost]
        public ActionResult breakStart(User u)
        {
            User t = e.Users.Find(u.Id);

            if (u.Id == t.Id && t.Password == u.Password)
            {
                Break_Times b = new Break_Times();
                b.User = t;
                string a = System.DateTime.Now.DayOfWeek.ToString();
                b.breakcheck = 1;
                b.startTime = DateTime.Now;
                b.Day = a;
                b.Id = u.Id;
                e.Break_Times.Add(b);
                e.SaveChanges();
                
               
                return RedirectToAction("details", "Home", new { u.Id });
            }
            else
            {
                ViewBag.msg = "wrong id or pass";
            }
            return View();
        }



        public ActionResult breakStop()
        {
            return View();
        }
        [HttpPost]
        public ActionResult breakStop(User u)
        {
            User t = e.Users.Find(u.Id);
            List<Daily_times> d = e.Daily_times.ToList();
            Daily_times dd = d.FindLast(x => x.User == t);
            e.Entry(dd).State = System.Data.Entity.EntityState.Modified;

            dd.check = 1;
            e.SaveChanges();
            if (u.Id == t.Id && t.Password == u.Password)
            {
                List<Break_Times> g = e.Break_Times.ToList();
                Break_Times b = new Break_Times();

                for (int i = g.Count - 1; i >= 0; i--)
                {
                    if (g[i].Id == u.Id)
                    {
                        b = g[i];
                        break;
                    }
                }
                // string a = System.DateTime.Now.DayOfWeek.ToString();

                b.endTime = DateTime.Now;
                //set.breaktimestart = "00";
                b.Id = u.Id;
                SqlCommand sql = new SqlCommand("BREAKSTOP", connection.getcon());
                sql.CommandType = System.Data.CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@id", b.BreakTimeID);
                sql.Parameters.AddWithValue("@endtime", b.endTime);
                sql.ExecuteNonQuery();
                b.breakcheck = 00;
                return RedirectToAction("details", "Home",new { u.Id});
            }
            else
            {
                ViewBag.msg = "wrong id or pass";
            }
            return View();
        }

        public ActionResult details(int id)
        {
            List<Details> data = new List<Details>();
            //List<User> u = e.Users.Join()
            //var details = (from a in e.Users
            //               join c in e.Daily_times on a.Id equals c.Id /*join n in e.Break_Times on a.Id equals n.Id*/
            //               select new
            //               {

            //                   ID = a.Id,
            //                   Name = a.Name,
            //                   Email = a.Email,
            //                   Active = a.Active,
            //                   StartTime = c.start_time,
            //                   Day = c.Day,
            //                   Contact = a.Contact,
            //                   //BreakStart = n.startTime,
            //                   // breakStop = n.endTime,
            //                   Status = c.Status,
            //                   end_time = c.end_time
            //               }
            //               ).ToList();

            var query = e.show_data();
            
            foreach (var i in query)
            {
                if (i.Day == DateTime.Now.DayOfWeek.ToString())
                {
                    Details d = new Details();
                    d.Id = i.Id;
                    d.Name = i.Name;
                    d.start_time = i.start_time;
                    d.check = (int)id;
                    d.end_time = i.end_time;
                    d.startTime = i.startTime;
                    d.endTime = i.endTime;
                    //d.Active = i.Active;
                    // d.Contact = i.Contact;
                    d.dailytimestatus = i.Status;
                    d.Month = i.Month;
                    d.Day = i.Day;
                    d.Status = i.Status;
                    
                    if (d.startTime == null)
                        d.breakcheck = 0;
                    else if (i.check == 1)
                    {
                        d.breakcheck = 2;
                    }
                    else
                        d.breakcheck = 1;
                    //d.startTime = i.BreakStart;
                    //d.endTime = i.breakStop;
                    data.Add(d);
                }
          }
            return View(data);

            //var details1 = (from t in e.Users
            //                join q in e.Break_Times on t.Id equals q.Id
            //                select new
            //                {
            //                    start = q.startTime,
            //                    end = q.endTime,
            //                    day = q.Day


            //                }

            //                ).ToList();


            //foreach (var i in details1)
            //{

            //}


        }




        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}