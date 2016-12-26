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
using Newtonsoft.Json.Linq;
using iPede.Site.Models;
using System.Threading.Tasks;

namespace iPede.Site.ApiControllers
{
    public class OrderItemsController : ApiController
    {
        private iPedeContext db = new iPedeContext();
        private MapperConfiguration config;
        private IMapper mapper;

        public OrderItemsController()
        {
            config = AutoMapperConfig.GetMapperInstance();
            mapper = config.CreateMapper();
        }

        // GET: api/OrderItems
        public IEnumerable<OrderItemDTO> GetOrderItems()
        {
            return db.OrderItems.ToList().Select(oi => mapper.Map<OrderItemDTO>(oi));
        }

        // GET: api/OrderItems/5
        [ResponseType(typeof(OrderItemDTO))]
        public IHttpActionResult GetOrderItem(int id)
        {
            OrderItem orderItem = db.OrderItems.Find(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<OrderItemDTO>(orderItem));
        }

        // PUT: api/OrderItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrderItem(int id, OrderItem orderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderItem.Id)
            {
                return BadRequest();
            }

            db.Entry(orderItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(id))
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

        // POST: api/OrderItems
        [ResponseType(typeof(OrderItem))]
        public async Task<IHttpActionResult> PostOrderItem(OrderItem orderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.OrderItems.Add(orderItem);
            db.SaveChanges();
            //db.Entry(orderItem).Reference(oi => oi.Product).Load();

            var item = mapper.Map<OrderItemDTO>(orderItem);
            await NotificationManager.Instance.SendNotificationAsync(NotificationEventNames.OrderItemCreated, item);

            return CreatedAtRoute("DefaultApi", new { id = item.Id }, item);
        }

        // DELETE: api/OrderItems/5
        [ResponseType(typeof(OrderItemDTO))]
        public IHttpActionResult DeleteOrderItem(int id)
        {
            OrderItem orderItem = db.OrderItems.Find(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            db.OrderItems.Remove(orderItem);
            db.SaveChanges();

            return Ok(mapper.Map<OrderItemDTO>(orderItem));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderItemExists(int id)
        {
            return db.OrderItems.Count(e => e.Id == id) > 0;
        }
    }
}