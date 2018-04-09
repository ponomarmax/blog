using AutoMapper;
using Blog.BLL.DTO;
using Blog.BLL.Interfaces;
using Blog.BLL.Services;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WEB.Controllers
{
    public class SearchController : Controller
    {
        IServiceCreator service;

        IMapper mapperBusinessToView, mapperViewToBusiness;
        //private UsersModel db = new UsersModel();
        public SearchController()
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
        }
        // GET: Search
        public ActionResult Index(string word,int page=1)
        {
            var blogDTO = service.blogService.GetAll();
            List<PostDTO> postsDTO = new List<PostDTO>();
            foreach (var b in blogDTO)
                foreach (var p in b.Posts)
                    if (p.Title.Contains(word) || p.Body.Contains(word))
                        postsDTO.Add(p);
                    else foreach (var tag in p.Tags)
                            if (tag.Tag == word)
                                postsDTO.Add(p);
            var posts = mapperBusinessToView.Map<List<PostModel>>(postsDTO);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View("Index",posts);
        }
        int pageSize = 3;
        // GET: Search/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Search/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Search/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Search/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
