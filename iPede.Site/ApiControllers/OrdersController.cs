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
using AutoMapper;
using iPede.Site.Models.DTOs;

namespace iPede.Site.ApiControllers
{
    public class OrdersController : ApiController
    {
        private iPedeContext db = new iPedeContext();
        private MapperConfiguration config;
        private IMapper mapper;

        public OrdersController()
        {
            config = AutoMapperConfig.GetMapperInstance();
            mapper = config.CreateMapper();
        }

        // GET: api/Orders
        public IEnumerable<OrderDTO> GetOrders()
        {
            return db.Orders.ToList().Select(o => mapper.Map<OrderDTO>(o));
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrder(int id, Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        [Route("api/Orders/{id}")]
        [HttpPost]
        [ResponseType(typeof(OrderDTO))]
        public IHttpActionResult PostOrder(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = db.Orders.Find(id);
            order.OrderStatusId = db.OrderStatuses.Single(os => os.Name == OrderStatus.Placed).Id;
            db.SaveChanges();

            return Ok(MapOrder(order));
        }

        // POST: api/Orders
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
        }

        
        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.Id == id) > 0;
        }

        private OrderDTO MapOrder(Order order)
        {
            var dto = mapper.Map<OrderDTO>(order);
            foreach (var item in dto.Items)
            {
                item.Product.MainImageThumbUrl = Url.Content(item.Product.MainImageThumbUrl);
                item.Product.MainImageUrl = Url.Content(item.Product.MainImageUrl);
            }
            return dto;
        }
    }
}