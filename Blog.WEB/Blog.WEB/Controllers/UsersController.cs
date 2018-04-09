using AutoMapper;
using Blog.BLL.DTO;
using Blog.BLL.Interfaces;
using Blog.BLL.Services;
using Blog.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Blog.WEB.Controllers
{
    public class UsersController : Controller
    {
        IServiceCreator service;
        IMapper mapperBusinessToView, mapperViewToBusiness;
        int pageNumber = 1;
        int pageSize = 3;
        public UsersController()
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

            //mapperBusinessToView = new MapperConfiguration(cfg => cfg.CreateMap<PostDTO, PostModel>()).CreateMapper();
            //mapperBlog = new MapperConfiguration(cfg => cfg.CreateMap<BlogDTO, BlogModel>()).CreateMapper();
            //mapperViewToBusiness = new MapperConfiguration(cfg => cfg.CreateMap<PostModel, PostDTO>()).CreateMapper();
        }
        // GET: Users
        public ActionResult Index()
        {
            var users = service.userService.GetAll();
            return View("ListUsers", mapperBusinessToView.Map<IEnumerable<UserModel>>(users).ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlockUnblockUser(string id)
        {
            service.userService.BlockUnblock(id);

            return RedirectToAction("Details", new { id });
        }
        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null || id == "")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            UserDTO userDTO = service.userService.GetById(id);
            if (userDTO == null)
                return HttpNotFound();
            UserModel user = mapperBusinessToView.Map<UserModel>(userDTO);
            return View(user);
        }

        // GET: Users/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Users/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null || id == "")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            UserDTO userDTO = service.userService.GetById(id);
            List<string> userRoles = service.roleService.GetRolesByUserId(id);
            var allRoles = service.roleService.GetAll();
            Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
            foreach (var role in allRoles)
                dictionary.Add(role, userRoles.Contains(role));

            //foreach (var item in dictionary.Keys)
            //{

            //}
            ViewBag.roles = dictionary;

            if (userDTO == null)
                return View("Error");
            UserModel user = mapperBusinessToView.Map<UserModel>(userDTO);
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(UserModel user, string[] selectedRoles, HttpPostedFileBase uploadImage)
        {
            //try
            //{
            if (user == null)
                return View("Error");
            if (selectedRoles == null)
                selectedRoles = new string[] { "user" };
            byte[] imageData = null;
            using (var binaryReader = new BinaryReader(uploadImage.InputStream))
            {
                imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
            }
            user.Photo = imageData;
            //service.userService.Modify(mapperViewToBusiness.Map<UserDTO>(user));
            service.roleService.UpdateListRoles(selectedRoles, user.Id);
            service.userService.Modify(mapperViewToBusiness.Map<UserDTO>(user));
            return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Users/Delete/5
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
