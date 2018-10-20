using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ZecaTrocosMercariEurohack.Models;
using LINQPad;
using AutoMapper;

namespace ZecaTrocosMercariEurohack.Controllers
{
    public class HintsController : ApiController
    {
        private KnowledgeDbEntities db = new KnowledgeDbEntities();

        // GET: api/Hints
        [HttpGet]
        [Route("/api/GetHintsByCategoryAndModifiers/{category}/{csmodifiers}")]
        public List<HintDto> GetHintsByCategoryAndModifiers(string category, string csmodifiers)
        {
            List<Hint> dbHints;

            if (string.IsNullOrWhiteSpace(csmodifiers))
            {
                dbHints = db.Hints.Where(ele => ele.CategoryIdentifier.Equals(category)).ToList();
            }
            else
            {
                List<string> mods = csmodifiers.Split(',').ToList();

                dbHints = db.Hints.Where(ele => ele.CategoryIdentifier.Equals(category) && 
                ele.HintModifiers.Any(som => mods.Contains(som.Modifier.Name))).ToList();
            }

            return Mapper.Map<List<HintDto>>(dbHints);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HintExists(int id)
        {
            return db.Hints.Count(e => e.Id == id) > 0;
        }
    }
}