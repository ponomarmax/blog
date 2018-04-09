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
using Blog.BLL.Interfaces;
using Blog.BLL.Services;
using Blog.Models;

namespace Blog3AAA.Controllers
{
    public class PostController : Controller
    {
        IServiceCreator service;
        
        IMapper mapperBusinessToView, mapperViewToBusiness;
        //private UsersModel db = new UsersModel();
        public PostController()
        {
            service = new ServiceCreator("Blog");
            
            
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogDTO, BlogModel>();
                cfg.CreateMap<PostDTO, PostModel>();
                cfg.CreateMap<UserDTO, UserModel>();
                cfg.CreateMap<CommentDTO, CommentModel>();
                cfg.CreateMap<TagDTO, TagModel>();
            });
            config.AssertConfigurationIsValid();
            mapperBusinessToView = config.CreateMapper();

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogModel, BlogDTO>();
                cfg.CreateMap<PostModel, PostDTO>();
                cfg.CreateMap<UserModel, UserDTO>();
                cfg.CreateMap<CommentModel, CommentDTO>();
                cfg.CreateMap<TagModel, TagDTO>();
            });
            config.AssertConfigurationIsValid();
            mapperViewToBusiness = config.CreateMapper();
            ViewBag.PageSize = pageSize;

            
        }

        int pageSize = 2; 
        public ActionResult Index(int? blogId, int? tagId, int page = 1)
        {
            IEnumerable<PostModel> posts;
            ViewBag.Page = page;
            if (blogId != null)
            {
                var p = service.postService.GetByBlogId((int)blogId);
                posts = mapperBusinessToView.Map<IEnumerable<PostModel>>(p);
                return View(posts);
            }
            if (tagId != null)
            {
                ViewBag.tag = "tag";
                ViewBag.tagId = tagId;

                var p = service.postService.GetByTagId((int)tagId);
                posts = mapperBusinessToView.Map<IEnumerable<PostModel>>(p);
                return View(posts);
            }

            return View(new List<PostModel>());
        }

        // GET: Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostDTO postDTO = service.postService.Get(id);
            if (postDTO == null)
            {
                return HttpNotFound();
            }
            PostModel post = mapperBusinessToView.Map<PostModel>(postDTO);
            return View(post);
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            IEnumerable<BlogDTO> b = service.blogService.GetByName(User.Identity.Name);
            return View(mapperBusinessToView.Map<IEnumerable<BlogModel>>(b));
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,DateTime,Body,BlogID")] PostModel post, string tags)
        {
            if (ModelState.IsValid)
            {

                service.postService.Create(mapperViewToBusiness.Map<PostDTO>(post),tags);
                return RedirectToAction("Index", new { blogId = post.BlogID });
            }

            return View(post);
        }

        // GET: Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostDTO postDTO = service.postService.Get(id);
            if (postDTO == null)
            {
                return HttpNotFound();
            }
            PostModel post = mapperBusinessToView.Map<PostModel>(postDTO);
            return View(post);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,DateTime,Body,BlogID")] PostModel post)
        {
            if (ModelState.IsValid)
            {
                service.postService.Modify(mapperViewToBusiness.Map<PostDTO>(post));
                return RedirectToAction("Details", new { id = post.ID });
            }
            return View(post);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment([Bind(Include = "ID,DateTime,Body,UserID,PostID")] CommentModel comment)
        {
            if (ModelState.IsValid)
            {
                service.commentService.Create(mapperViewToBusiness.Map<CommentDTO>(comment));
                return RedirectToAction("Details", new { id = comment.PostID });
            }
            return View();
        }
        // GET: Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PostDTO postDTO = service.postService.Get(id);
            if (postDTO == null)
            {
                return HttpNotFound();
            }
            PostModel post = mapperBusinessToView.Map<PostModel>(postDTO);
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            service.postService.Delete(id);
            return RedirectToAction("Index", "Blogs");
        }
        [HttpPost, ActionName("DeleteComment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCommentConfirmed(int id, int postID)
        {
            service.commentService.Delete(id);
            return RedirectToAction("Index", "Blogs");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                service.postService.Dispose();
                service.blogService.Dispose();
                //postService.Dispose(disposing);

            }
            base.Dispose(disposing);
        }
    }
}
