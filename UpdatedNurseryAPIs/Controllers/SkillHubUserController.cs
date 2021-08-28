using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UpdatedNurseryAPIs.Models;

namespace UpdatedNurseryAPIs.Controllers
{
    public class SkillHubUserController : ApiController
    {
        public IEnumerable<SkillHubUser> Get()
        {
            db_a78ede_nurseryEntities entities = new db_a78ede_nurseryEntities();

            return entities.SkillHubUsers.ToList();
        }

        public HttpResponseMessage Get(int id)
        {
            db_a78ede_nurseryEntities entities = new db_a78ede_nurseryEntities();

            var entity = entities.SkillHubUsers.FirstOrDefault(e => e.id == id);

            if (entity != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ID Not Found");
        }

        public HttpResponseMessage PostKid([FromBody] SkillHubUser user)
        {
            try
            {
                db_a78ede_nurseryEntities entities = new db_a78ede_nurseryEntities();

                user.creaeted_on = DateTime.Now;

                var username = entities.SkillHubUsers.Where(e => e.username == user.username).FirstOrDefault();

                if (username == null)
                {

                    entities.SkillHubUsers.Add(user);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, user);

                    message.Headers.Location = new Uri(Request.RequestUri + "/" + user.id.ToString());
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

        public HttpResponseMessage PutUser(int id, [FromBody] SkillHubUser user)
        {
            try
            {
                db_a78ede_nurseryEntities entities = new db_a78ede_nurseryEntities();

                var entity = entities.SkillHubUsers.FirstOrDefault(e => e.id == id);

                if (entity == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "ID Not Found");
                else
                {
                    entity.name = user.name;
                    entity.username = user.username;
                    entity.password = user.password;
                    entity.contact = user.contact;
                    entity.age = user.age;
                    entity.address = user.address;

                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage DeleteUser(int id)
        {
            try
            {
                db_a78ede_nurseryEntities entities = new db_a78ede_nurseryEntities();

                var entity = entities.SkillHubUsers.FirstOrDefault(e => e.id == id);

                if (entity == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User Not Found");
                }
                else
                {
                    entities.SkillHubUsers.Remove(entity);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "User Deleted");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage GetUserLogIn(string username, string password)
        {
            db_a78ede_nurseryEntities entities = new db_a78ede_nurseryEntities();

            var entityusername = entities.SkillHubUsers.Where(e => e.username.Equals(username) && e.password == password).FirstOrDefault();

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
