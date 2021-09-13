using FileUpload.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileUpload.Controllers
{
    public class HomeController : Controller
    {
        EmployeeEnterpriseEntities databaseobject = new EmployeeEnterpriseEntities();
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
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(tbl_FileUpload tbl_file, HttpPostedFileBase imgfile)  // 'imgfile' should be same as UI name
        {
            tbl_FileUpload tblfileupload = new tbl_FileUpload();

            string path = Uploadimage(imgfile);

            if(path.Equals("-1"))
            {
                ViewBag.message = "Please select file to upload...";
            }
            else
            {
                tblfileupload.FullName = tbl_file.FullName;
                tblfileupload.Email = tbl_file.Email;
                tblfileupload.Address = tbl_file.Address;
                tblfileupload.FilePath = path;

                databaseobject.tbl_FileUpload.Add(tblfileupload);
                databaseobject.SaveChanges();
                ViewBag.message = "File Uploaded Successfully...";

                ModelState.Clear();
            }

            return View();
        }

        public string Uploadimage(HttpPostedFileBase file)
        {
            Random r = new Random();

            string path = "-1";

            int random = r.Next();

            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Content/Upload"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Content/Upload/" + random + Path.GetFileName(file.FileName);
                        //    ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg ,jpeg or png formats are acceptable....'); </script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please select a file'); </script>");

                path = "-1";
            }
            return path;

        }

        //public ActionResult UploadFile(HttpPostedFileBase file)
        //{
        //    try
        //    {
        //        if(file.ContentLength > 0)
        //        {
        //            string _FileName = Path.GetFileName(file.FileName);
        //            string _path = Path.Combine(Server.MapPath("~/Content/Upload"), _FileName);
        //            file.SaveAs(_path);
        //        }
        //        ViewBag.Message = "File Uploaded Successfully!!";

        //        return View();
        //    }

        //    catch
        //    {
        //        ViewBag.Message = "File upload failed!!";
        //        return View();
        //    }
        //}
    }
}