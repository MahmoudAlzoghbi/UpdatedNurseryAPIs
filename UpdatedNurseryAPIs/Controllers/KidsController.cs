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
using UpdatedNurseryAPIs.Models;

namespace UpdatedNurseryAPIs.Controllers
{
    public class KidsController : ApiController
    {

        public IEnumerable<Kid> Get()
        {
            db_a78ede_nurseryEntities entity = new db_a78ede_nurseryEntities();

            //entity.Kids.ToList();
            return entity.Kids.ToList();
        }

        public HttpResponseMessage Get(int id)
        {
            db_a78ede_nurseryEntities entities = new db_a78ede_nurseryEntities();

            var entity = entities.Kids.Where(e => e.id == id).FirstOrDefault();

            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ID Not Found");
            }
                
        }

        public HttpResponseMessage PostKid([FromBody] Kid kid)
        {
            try
            {
                db_a78ede_nurseryEntities entities = new db_a78ede_nurseryEntities();

                kid.created_on = DateTime.Now;

                var username = entities.Kids.Where(e => e.email == kid.email).FirstOrDefault();

                if (username == null)
                {

                    entities.Kids.Add(kid);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, kid);

                    message.Headers.Location = new Uri(Request.RequestUri + "/" + kid.id.ToString());
                    return message;
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Username are Found try to Log in");
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage PutKid(int id, [FromBody] Kid kid)
        {
            try
            {
                db_a78ede_nurseryEntities entities = new db_a78ede_nurseryEntities();

                var entity = entities.Kids.FirstOrDefault(e => e.id == id);

                if (entity == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ID Not Found");
                else
                {
                    entity.name = kid.name;
                    entity.age = kid.age;
                    entity.phone1 = kid.phone1;
                    entity.phone2 = kid.phone2;
                    //entity.email = kid.email;
                    entity.password = kid.password;
                    entity.approve = kid.approve;
                    entity.collected = kid.collected;
                    entity.image = kid.image;
                    entity.date_of_birth = kid.date_of_birth;

                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage DeleteKid(int id)
        {
            try
            {
                db_a78ede_nurseryEntities entities = new db_a78ede_nurseryEntities();

                var entity = entities.Kids.FirstOrDefault(e => e.id == id);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kid Not Found");
                }
                else
                {
                    entities.Kids.Remove(entity);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage GetKidLogIn(string username, string password)
        {
            db_a78ede_nurseryEntities entities = new db_a78ede_nurseryEntities();

            var entityusername = entities.Kids.Where(e => e.email.Equals(username) && e.password == password).FirstOrDefault();

            if (entityusername != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entityusername);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "userName not found");
            }
        }
    }
}