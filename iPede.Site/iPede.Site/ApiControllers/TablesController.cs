﻿using System;
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
    public class TablesController : ApiController
    {
        private iPedeContext db = new iPedeContext();
        private IMapper mapper;

        public TablesController()
        {
            var config = AutoMapperConfig.GetMapperInstance();
            mapper = config.CreateMapper();
        }
        // GET: api/Tables
        public IQueryable<Table> GetTables()
        {
            return db.Tables;
        }

        // GET: api/Tables/5
        [ResponseType(typeof(TableDTO))]
        public IHttpActionResult GetTable(int id)
        {
            Table table = db.Tables.Find(id);
            if (table == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<TableDTO>(table));
        }

        // PUT: api/Tables/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTable(int id, Table table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != table.Id)
            {
                return BadRequest();
            }

            db.Entry(table).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableExists(id))
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

        // POST: api/Tables
        [ResponseType(typeof(Table))]
        public IHttpActionResult PostTable(Table table)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tables.Add(table);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = table.Id }, table);
        }

        // DELETE: api/Tables/5
        [ResponseType(typeof(Table))]
        public IHttpActionResult DeleteTable(int id)
        {
            Table table = db.Tables.Find(id);
            if (table == null)
            {
                return NotFound();
            }

            db.Tables.Remove(table);
            db.SaveChanges();

            return Ok(table);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TableExists(int id)
        {
            return db.Tables.Count(e => e.Id == id) > 0;
        }
    }
}