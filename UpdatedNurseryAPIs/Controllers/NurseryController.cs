using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UpdatedNurseryAPIs.Models;

namespace UpdatedNurseryAPIs.Controllers
{
    public class NurseryController : ApiController
    {
        public IEnumerable<Nursery> Get()
        {
            db_a78ede_nurseryEntities entities = new db_a78ede_nurseryEntities();

            return entities.Nurseries.ToList();
        }

        public HttpResponseMessage Get(int id)
        {
            db_a78ede_nurseryEntities entities = new db_a78ede_nurseryEntities();

            var entity = entities.Nurseries.FirstOrDefault(e => e.id == id);

            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ID Not Found");
        }

        public HttpResponseMessage PostNursery([FromBody] Nursery nursery)
        {
            try
            {
                db_a78ede_nurseryEntities entities = new db_a78ede_nurseryEntities();

                entities.Nurseries.Add(nursery);
                entities.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, nursery);

                message.Headers.Location = new Uri(Request.RequestUri + "/" + nursery.id.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage PutNursiry(int id, [FromBody] Nursery user)
        {
            try
            {
                db_a78ede_nurseryEntities entities = new db_a78ede_nurseryEntities();

                var entity = entities.Nurseries.FirstOrDefault(e => e.id == id);

                if (entity == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ID Not Found");
                else
                {
                    entity.name = user.name;
                    entity.phone = user.phone;
                    entity.address = user.address;
                    entity.website = user.website;
                    entity.facebook = user.facebook;
                    entity.logo = user.logo;
                    entity.email = user.email;

                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage DeleteNursery(int id)
        {
            try
            {
                db_a78ede_nurseryEntities entities = new db_a78ede_nurseryEntities();

                var entity = entities.Nurseries.FirstOrDefault(e => e.id == id);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Nursery Not Found");
                }
                else
                {
                    entities.Nurseries.Remove(entity);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
