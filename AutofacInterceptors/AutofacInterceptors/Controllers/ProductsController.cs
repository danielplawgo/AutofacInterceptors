using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutofacInterceptors.Logics;
using AutofacInterceptors.Models;
using AutofacInterceptors.ViewModels.Products;
using AutoMapper;

namespace AutofacInterceptors.Controllers
{
    public class ProductsController : Controller
    {
        private Lazy<IProductLogic> _logic;

        protected IProductLogic Logic
        {
            get { return _logic.Value; }
        }

        private Lazy<IMapper> _mapper;

        protected IMapper Mapper
        {
            get { return _mapper.Value; }
        }

        public ProductsController(Lazy<IProductLogic> logic,
            Lazy<IMapper> mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        // GET: /Products2/
        public ActionResult Index()
        {
            var items = this.Logic.GetAllActive();

            var viewModel = new IndexViewModel();

            viewModel.Items = this.Mapper.Map<List<IndexItemViewModel>>(items);

            return View(viewModel);
        }

        // GET: /Products2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = this.Logic.GetById(id.Value);
            if (model == null)
            {
                return HttpNotFound();
            }

            var viewModel = this.Mapper.Map<ProductViewModel>(model);

            return View(viewModel);
        }

        // GET: /Products2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Products2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = this.Mapper.Map<Product>(viewModel);

                this.Logic.Add(model);

                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        // GET: /Products2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = this.Logic.GetById(id.Value);

            if (model == null)
            {
                return HttpNotFound();
            }

            var viewModel = this.Mapper.Map<ProductViewModel>(model);

            return View(viewModel);
        }

        // POST: /Products2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var model = this.Logic.GetById(viewModel.Id);

                if (model == null)
                {
                    return HttpNotFound();
                }

                this.Mapper.Map(viewModel, model);

                this.Logic.Update(model);

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: /Products2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = this.Logic.GetById(id.Value);
            if (model == null)
            {
                return HttpNotFound();
            }

            var viewModel = this.Mapper.Map<ProductViewModel>(model);

            return View(viewModel);
        }

        // POST: /Products2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            this.Logic.Delete(id);

            return RedirectToAction("Index");
        }
    }
}