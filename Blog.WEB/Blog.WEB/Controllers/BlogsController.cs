using Blog.BLL.Services;
using Blog.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Blog.BLL.DTO;
using Blog.Models;

namespace Blog.WEB.Controllers
{
    public class BlogsController : BaseController
    {
        IServiceCreator service;
        public BlogsController()
        {
            service = new ServiceCreator("Blog");
        }

        public ActionResult Index()
        {
            IEnumerable<BlogModel> blogs = mapperBusinessToView.Map<IEnumerable<BlogModel>>(service.blogService.GetAll());
            return View(blogs.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogDTO blogDTO = service.blogService.Get(id);
            if (blogDTO == null)
            {
                return HttpNotFound();
            }
            BlogModel blog = mapperBusinessToView.Map<BlogModel>(blogDTO);
            return View(blog);
        }

        // GET: Blogs/Create
        [Authorize]
        public ActionResult Create(string id)
        {
            if(id==null||id=="")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            UserDTO userDTO = service.userService.GetById(id);
            UserModel user = mapperBusinessToView.Map<UserModel>(userDTO);
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,BlogerEmail")] BlogModel blog)
        {
            if (ModelState.IsValid)
            {
                service.blogService.Create(mapperViewToBusiness.Map<BlogDTO>(blog));
                return RedirectToAction("Index");
            }

            return View("Error");
        }

        // GET: Blogs/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BlogDTO blogDTO = service.blogService.Get(id);
            if (blogDTO == null)
            {
                return HttpNotFound();
            }
            BlogModel blog = mapperBusinessToView.Map<BlogModel>(blogDTO);

            return View(blog);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,BlogerID")] BlogModel blog)
        {
            if (ModelState.IsValid)
            {
                service.blogService.Modify(mapperViewToBusiness.Map<BlogDTO>(blog));
                return RedirectToAction("Index");
            }
            //ViewBag.BlogerID = new SelectList(db.AspNetUsers, "Id", "Email", blog.BlogerID);
            return View(blog);
        }

        //// GET: Blogs/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Blog blog = blogService.Find(id);
        //    if (blog == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(blog);
        //}

        //// POST: Blogs/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    blogService.Remove(id);
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            //blogService.Dispose(disposing);
            service.blogService.Dispose();
            base.Dispose(disposing);
        }
    }
}
