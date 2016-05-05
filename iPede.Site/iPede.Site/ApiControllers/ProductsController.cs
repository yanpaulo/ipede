using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using iPede.Site.Models.Entities;
using iPede.Site.Models.DTOs;
using AutoMapper;

namespace iPede.Site.ApiControllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private iPedeContext db = new iPedeContext();
        private MapperConfiguration config;
        private IMapper mapper;

        public ProductsController()
        {
            config = AutoMapperConfig.GetMapperInstance();
            mapper = config.CreateMapper();
        }

        [Route("categorized")]
        public IEnumerable<CategoryDTO> GetCategorized()
        {

            return db.Categories.Where(c => c.ParentCategory == null)
                .ToList()
                .Select(c => MapCategory(c));
        }

        // GET: api/Products
        public IEnumerable<ProductDTO> GetProducts()
        {
            
            return db.Products
                .ToList()
                .Select(p => MapProduct(p));
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(MapProduct(product));
        }

        #region Unsupported Actions
        //At the moment, the following actions are (and maybe won't be) supported

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ProductId)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ProductId == id) > 0;
        }

        private CategoryDTO MapCategory(Category c)
        {
            CategoryDTO ret = mapper.Map<Category, CategoryDTO>(c);
            foreach (var sub in ret.SubCategories)
            {
                foreach (var p in sub.Products)
                {
                    SetupProductDTO(p);
                }
            }
            foreach (var p in ret.Products)
            {
                SetupProductDTO(p);
            }
            return ret;
        }

        private ProductDTO MapProduct(Product p)
        {
            ProductDTO ret = mapper.Map<Product, ProductDTO>(p);
            return SetupProductDTO(ret);
        }

        private ProductDTO SetupProductDTO(ProductDTO p)
        {
            p.MainImageUrl = Url.Content(p.MainImageUrl);
            p.MainImageThumbUrl = Url.Content(p.MainImageThumbUrl);
            p.IsSuggested = db.SuggestedProducts.ToList().Count(s => s.Product.ProductId == p.ProductId) > 0;

            return p;
        }
    }
}