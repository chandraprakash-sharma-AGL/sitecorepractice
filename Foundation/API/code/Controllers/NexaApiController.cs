using System.Collections.Generic;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using NexaDataAccess;
using Sitecore.Diagnostics;

namespace Sitecore.Foundation.API.Controllers
{
    public class NexaApiController : Controller
    {
        // GET: NexaApi

        public JsonResult GetAllCarModel()
        {
                using (NexaDataDBEntities nexaDBEntities = new NexaDataDBEntities())
                {
                    IEnumerable<nexaCarModel> data;
                //data = Newtonsoft.Json.JsonConvert.SerializeObject(nexaDBEntities.nexaCarModels.ToList(), Newtonsoft.Json.Formatting.None);
                data = nexaDBEntities.nexaCarModels.ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
                }   
        }
        public Object GetCarVariant(string ModelCode)
        {
            try
            {
                using (NexaDataDBEntities nexaDBEntities = new NexaDataDBEntities())
                {
                    var nexaCarVarients = nexaDBEntities.nexaCarVarients.Where(m => m.ModelCode.ToLower() == ModelCode.ToLower()).ToList();

                    //return Request.CreateResponse(HttpStatusCode.OK, nexaCarVarients);
                    return Json(nexaCarVarients, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Error in Method GetCarVariant of Controller API", ex, this);
                throw new HttpResponseException(System.Net.HttpStatusCode.ServiceUnavailable);
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Get Car Variant Color api method
        public Object GetCarVariantColor(string VariantCode)
        {
            try
            {
                using (NexaDataDBEntities nexaDBEntities = new NexaDataDBEntities())
                {

                    var color = from c in nexaDBEntities.nexaCarColors
                                join v in nexaDBEntities.nexaVarientColors
                                on c.colorCode equals v.ColorCode
                                where v.VarientCode.ToLower() == VariantCode.ToLower()
                                select new
                                {
                                    colorName = c.colorName,
                                    colorCode = c.colorCode
                                };

                    //Dictionary<string, string> res = new Dictionary<string, string>();
                    //foreach(var r in color)
                    //{
                    //    res.Add(r.colorCode, r.colorName);
                    //}
                    //return Request.CreateResponse(HttpStatusCode.OK, color.ToList());
                    return Json(color.ToList(), JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Error in Method GetCarVariant of Controller API", ex, this);
                throw new HttpResponseException(System.Net.HttpStatusCode.ServiceUnavailable);
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Get State method
        public Object GetAllStates()
        {

            List<allState> allStates = new List<allState>();
            try
            {
                using (NexaDataDBEntities nexaDBEntities = new NexaDataDBEntities())
                {
                    allStates = nexaDBEntities.allStates.ToList();
                    return Json(allStates, JsonRequestBehavior.AllowGet);
                    //return Request.CreateResponse(HttpStatusCode.OK, allStates);
                }
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Error in Method GetCarVariant of Controller API", ex, this);
                throw new HttpResponseException(System.Net.HttpStatusCode.ServiceUnavailable);
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Get All cities by state name
        public Object GetAllCitiesByStateName(string stateCode)
        {
            try
            {
                using (NexaDataDBEntities nexaDBEntities = new NexaDataDBEntities())
                {
                    var allCities = nexaDBEntities.AllCities.Where(c => c.stateCode.ToLower() == stateCode.ToLower()).ToList();
                    return Json(allCities, JsonRequestBehavior.AllowGet);
                    //return Request.CreateResponse(HttpStatusCode.OK, allCities);
                }
            }
            catch (Exception ex)
            {
                Sitecore.Diagnostics.Log.Error("Error in Method GetCarVariant of Controller API", ex, this);
                throw new HttpResponseException(System.Net.HttpStatusCode.ServiceUnavailable);
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public ActionResult GetNexaCarDetailsByModelCode(string ModelCode)
        {
            return null;
        }
    }
}