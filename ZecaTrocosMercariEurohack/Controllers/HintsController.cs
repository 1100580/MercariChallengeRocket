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
using RestSharp;
using System.Text;
using Newtonsoft.Json;

namespace ZecaTrocosMercariEurohack.Controllers
{
    public class HintsController : ApiController
    {
        private KnowledgeDbEntities db = new KnowledgeDbEntities();

        // GET: api/Hints
        [HttpGet]
        [Route("api/GetHintsByCategoryAndModifiers/{category}")]
        public List<HintDto> GetHintsByCategoryAndModifiers(string category)
        {
            List<Hint> dbHints = db.Hints.Where(ele => ele.CategoryIdentifier.Equals(category)).ToList();

            return Mapper.Map<List<HintDto>>(dbHints);
        }

        // GET: api/Hints
        [HttpPost]
        [Route("api/Categorize")]
        public string Categorize(BasicStringObj base64wrapper)
        {
            byte[] file = Convert.FromBase64String(base64wrapper.basicString.Split(',')[1]);

            var client = new RestClient("https://app.nanonets.com/api/v2/ImageCategorization/LabelFile/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("z0O7dG9rqKSkuRbOUV4Xd-A3Ih-NulaS:")));
            request.AddHeader("accept", "Multipart/form-data");
            request.AddParameter("modelId", "af0d9f9c-e513-41f8-92f3-d6d537547c7c");
            request.AddFileBytes("file", file, "filename.jpg");
            IRestResponse response = client.Execute(request);

            CategorizeResponse deserializedResponse = JsonConvert.DeserializeObject<CategorizeResponse>(response.Content);

            return deserializedResponse.result.First().prediction.First().label;
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