
using Newtonsoft.Json;
using NexaDataAccess;
using Sitecore.Data.Items;
using Sitecore.Feature.NexaWeb.Models;
using Sitecore.Foundation.API.Controllers;
using Sitecore.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Configuration;

namespace Sitecore.Feature.NexaWeb.Controllers
{
    public class EBookingController : Controller
    {
        const string ApiPath = "https://api.uat.marutifinancemarketplace.com";
        //string api = System.Configuration.ConfigurationManager.AppSettings["MsilAPIPath"];

        //Load All Car Models with Image
        public ActionResult CarModels()
        {
            const string carModelUrl = "/maruti/master/v1/car-model/all";
            const string allStateUrl = "/maruti/master/v1/state/all";
            string allCarModels = string.Empty;
            string allStates = string.Empty;
            try
            {
                allCarModels = WebHttpApi.ApiCall(ApiPath, carModelUrl);
            }
            catch (Exception ex)
            {
                allCarModels = "{\"Message:" + ex.Message + "\"}";
            }
            //Fetch data from Api call
            CarModelList carModelList = JsonConvert.DeserializeObject<CarModelList>(allCarModels);
            var data = carModelList.car_model_list.AsEnumerable();

            //Fetch Data From sitecore
            Sitecore.Data.Items.Item[] CarModelFolder = Sitecore.Context.Database.SelectItems("fast:/sitecore/content/AllSites/NexaWeb/Data/NexaWebCarModel/*");
            IEnumerable<Item> CarModelsDetails = CarModelFolder.ToList();

            var AllModelDetails = data.Join(CarModelsDetails,
                                  cm1 => cm1.msil_code,
                                  cm2 => cm2.Fields["CarModelCode"].Value,
                                  (cm1, cm2) => new AllModelDetailsModel
                                  {
                                      id = cm1.id,
                                      car_model = cm1.car_model,
                                      msil_code = cm1.msil_code,
                                      car_model_image = Sitecore.Resources.Media.MediaManager.GetMediaUrl(((Sitecore.Data.Fields.ImageField)cm2.Fields["CarImage"]).MediaItem)
                                  }).AsEnumerable();
            try
            {
                allStates = WebHttpApi.ApiCall(ApiPath, allStateUrl);
            }
            catch (Exception ex)
            {
                allStates = "{\"Message:" + ex.Message + "\"}";
            }
            AllStates allStateList = JsonConvert.DeserializeObject<AllStates>(allStates);

            ViewBag.AllStates = allStateList.state_list.AsEnumerable();
            ViewBag.AllModelDetails = AllModelDetails;
            return View("~/Views/EBooking/Index.cshtml");
        }

        public JsonResult CarModelVariant(string car_model_id)
        {
            string carVariantApi = "/maruti/master/v1/car-variant/" + car_model_id;
            string carModelVariant = string.Empty;
            try
            {
                carModelVariant = WebHttpApi.ApiCall(ApiPath, carVariantApi);
            }
            catch(Exception ex)
            {
                carModelVariant = "{\"Message:" + ex.Message + "\"}";
            }
            //var data = JsonConvert.SerializeObject(carModelVariant);
            return Json(carModelVariant, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCitiesByStateCode(string state_code)
        {
            string citiesApi = "/maruti/master/v1/city/"+state_code;
            string cities = string.Empty;
            try
            {
                cities = WebHttpApi.ApiCall(ApiPath, citiesApi);
            }catch(Exception ex)
            {
                cities = "{\"Message:" + ex.Message + "\"}";
            }
            //var data = JsonConvert.SerializeObject(cities);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        // GET: EBooking
        public ActionResult Index1()
        {
            Sitecore.Data.Database master = Sitecore.Configuration.Factory.GetDatabase("master");
            Sitecore.Data.Items.Item home = master.GetItem("/sitecore/content/AllSites/NexaWeb/Data/NexaWebCarModel");
            //Sitecore.Context.Database
            var model = new NexaCarModel();
            List<CarDetails> carDetails = new List<CarDetails>();
            List<Item> childItem = new List<Item>();
            //IMvcContext mvcContext = new MvcContext();
            //var model1 = mvcContext.GetContextItem<NexaCarModel>();
            foreach (Item item in home.Children) { childItem.Add(item); }
            foreach (var item in childItem)
            {
                //if (item.Fields["CarModelCode"].Value == "B")
                //{
                //Sitecore.Data.Fields.ImageField imgField = ((Sitecore.Data.Fields.ImageField)item.Fields["CarImage"]);
                //var CarImageUrl = Sitecore.Resources.Media.MediaManager.GetMediaUrl(imgField.MediaItem);
                //Sitecore.Data.Fields.ImageField LogoImgField = ((Sitecore.Data.Fields.ImageField)item.Fields["CarLogoImage"]);
                //var CarLogoUrl = Sitecore.Resources.Media.MediaManager.GetMediaUrl(LogoImgField.MediaItem);
                var CarImage = new MvcHtmlString(FieldRenderer.Render(item, "CarImage"));
                var CarLogoImage = new MvcHtmlString(FieldRenderer.Render(item, "CarLogoImage"));
                var CarModelName = item.Fields["CarModelName"].Value;
                var CarModelCode = item.Fields["CarModelCode"].Value;
                carDetails.Add(new CarDetails
                {
                    CarImage = CarImage,
                    CarLogoImage = CarLogoImage,
                    CarModelCode = CarModelCode,
                    CarModelName = CarModelName
                });
                model.NexaCars = carDetails;
                //model.CarImage = CarImageUrl;
                //model.CarModelCode = item.Fields["CarModelCode"].Value;
                //model.CarLogoImage = CarLogoUrl;
                //model.CarModelName = item.Fields["CarModelName"].Value;
                //}
            }
            return View(model);
        }
        public ActionResult Index()
        {
            //Return all car model
            //Return car model image
            //Return all states
            IEnumerable<nexaCarModel> carModels;
            IEnumerable<allState> allStates;
            IEnumerable<Item> CarModelsDetails;
            using (NexaDataDBEntities nexaDataDBEntities = new NexaDataDBEntities())
            {
                carModels = nexaDataDBEntities.nexaCarModels.ToList();
                allStates = nexaDataDBEntities.allStates.ToList();
                ViewBag.CarModels = carModels;
                ViewBag.AllStates = allStates;
            }
            //Sitecore.Data.Database current = Sitecore.Context.Database; // return Web database
            Sitecore.Data.Items.Item CarModelFolder = Sitecore.Context.Database.Items["/sitecore/content/AllSites/NexaWeb/Data/NexaWebCarModel"];
            CarModelsDetails = CarModelFolder.Children.ToList();


            var AllModelDetails = carModels.Join(CarModelsDetails,
                                  cm1 => cm1.ModelCode,
                                  cm2 => cm2.Fields["CarModelCode"].Value,
                                  (cm1, cm2) => new AllModelDetailsModel
                                  {
                                      car_model = cm1.ModelName,
                                      msil_code = cm1.ModelCode,
                                      car_model_image = Sitecore.Resources.Media.MediaManager.GetMediaUrl(((Sitecore.Data.Fields.ImageField)cm2.Fields["CarImage"]).MediaItem)
                                  }).AsEnumerable();
            ViewBag.AllModelDetails = AllModelDetails;

            //var Serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //string allModelDetails = Serializer.Serialize(AllModelDetails);

            //return Json(allModelDetails, JsonRequestBehavior.AllowGet);
            //JsonResult allModelDetailsJson = new JsonResult();
            //allModelDetailsJson.Data = AllModelDetails;
            return View(AllModelDetails);
        }
        public JsonResult GetCarModelWithImage()
        {
            IEnumerable<nexaCarModel> carModels;
            IEnumerable<allState> allStates;
            IEnumerable<Item> CarModelsDetails;
            using (NexaDataDBEntities nexaDataDBEntities = new NexaDataDBEntities())
            {
                carModels = nexaDataDBEntities.nexaCarModels.ToList();
                allStates = nexaDataDBEntities.allStates.ToList();
                ViewBag.CarModels = carModels;
                ViewBag.AllStates = allStates;
            }
            //Sitecore.Data.Database current = Sitecore.Context.Database; // return Web database
            //Sitecore.Data.Items.Item CarModelFolder = Sitecore.Context.Database.Items["/sitecore/content/AllSites/NexaWeb/Data/NexaWebCarModel"];
            Sitecore.Data.Items.Item[] CarModelFolder = Sitecore.Context.Database.SelectItems("fast:/sitecore/content/AllSites/NexaWeb/Data/NexaWebCarModel/*");
            //CarModelsDetails = CarModelFolder.Children.ToList();
            CarModelsDetails = CarModelFolder.ToList();

            var AllModelDetails = carModels.Join(CarModelsDetails,
                                  cm1 => cm1.ModelCode,
                                  cm2 => cm2.Fields["CarModelCode"].Value,
                                  (cm1, cm2) => new AllModelDetailsModel
                                  {
                                      car_model = cm1.ModelName,
                                      msil_code = cm1.ModelCode,
                                      car_model_image = Sitecore.Resources.Media.MediaManager.GetMediaUrl(((Sitecore.Data.Fields.ImageField)cm2.Fields["CarImage"]).MediaItem)
                                  }).AsEnumerable();
            //ViewBag.AllModelDetails = AllModelDetails;

            var Serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var allModelDetails = Serializer.Serialize(AllModelDetails);

            return Json(allModelDetails, JsonRequestBehavior.AllowGet);
        }
        // GET: NexaApi
        //public JsonResult GetAllCarModel()
        //{
        //    using (NexaDataDBEntities nexaDBEntities = new NexaDataDBEntities())
        //    {
        //        IEnumerable<nexaCarModel> data;
        //        //data = Newtonsoft.Json.JsonConvert.SerializeObject(nexaDBEntities.nexaCarModels.ToList(), Newtonsoft.Json.Formatting.None);
        //        data = nexaDBEntities.nexaCarModels.ToList();

        //        return Json(data, JsonRequestBehavior.AllowGet);
        //    }
        //}
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
                return ex;
                //Sitecore.Diagnostics.Log.Error("Error in Method GetCarVariant of Controller API", ex, this);
                //throw new HttpResponseException(System.Net.HttpStatusCode.ServiceUnavailable);
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
                //Sitecore.Diagnostics.Log.Error("Error in Method GetCarVariant of Controller API", ex, this);
                //throw new HttpResponseException(System.Net.HttpStatusCode.ServiceUnavailable);
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return ex;
            }
        }
        //Get State method
        //public Object GetAllStates()
        //{

        //    List<allState> allStates = new List<allState>();
        //    try
        //    {
        //        using (NexaDataDBEntities nexaDBEntities = new NexaDataDBEntities())
        //        {
        //            allStates = nexaDBEntities.allStates.ToList();
        //            return Json(allStates, JsonRequestBehavior.AllowGet);
        //            //return Request.CreateResponse(HttpStatusCode.OK, allStates);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Sitecore.Diagnostics.Log.Error("Error in Method GetCarVariant of Controller API", ex, this);
        //        //throw new HttpResponseException(System.Net.HttpStatusCode.ServiceUnavailable);
        //        //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //        return ex;
        //    }
        //}
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
                //Sitecore.Diagnostics.Log.Error("Error in Method GetCarVariant of Controller API", ex, this);
                //throw new HttpResponseException(System.Net.HttpStatusCode.ServiceUnavailable);
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return ex;
            }
        }
        public JsonResult GetModelImageByCode(string ModelCode)
        {
            Sitecore.Data.Database master = Sitecore.Configuration.Factory.GetDatabase("master");
            Sitecore.Data.Items.Item CarModelFolder = master.GetItem("/sitecore/content/AllSites/NexaWeb/Data/NexaWebCarModel");
            //Sitecore.Context.Database
            var model = new NexaCarModel();
            List<CarDetails> carDetails = new List<CarDetails>();
            //List<Item> childItem = new List<Item>();
            //foreach (Item item in NexaWebCarModelFolder.Children)
            //{
            //    childItem.Add(item);
            //}
            foreach (Item item in CarModelFolder.Children)
            {
                if (item.Fields["CarModelCode"].Value == ModelCode)
                {
                    Sitecore.Data.Fields.ImageField imgField = ((Sitecore.Data.Fields.ImageField)item.Fields["CarImage"]);
                    string url = Sitecore.Resources.Media.MediaManager.GetMediaUrl(imgField.MediaItem);
                    string CarImage = url;
                    //var CarImage = new MvcHtmlString(FieldRenderer.Render(item, "CarImage"));
                    var CarLogoImage = new MvcHtmlString(FieldRenderer.Render(item, "CarLogoImage"));
                    var CarModelName = item.Fields["CarModelName"].Value;
                    var CarModelCode = item.Fields["CarModelCode"].Value;
                    //carDetails.Add(new CarDetails
                    //{
                    //    CarImage = CarImage,
                    //    CarLogoImage = CarLogoImage,
                    //    CarModelCode = CarModelCode,
                    //    CarModelName = CarModelName
                    //});
                    //ViewBag.NexaCars = carDetails;
                    //ViewBag.CarImage = CarImage;
                    //model.NexaCars = carDetails;
                    return Json(CarImage);
                }
            }

            return null;
        }

    }
}


