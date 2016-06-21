using Health.Model.ViewModels;
using Health.Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using Microsoft.Owin.Security;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security.Cookies;
using Health.Model.DBContext;
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Health.Controllers
{
    [System.Web.Http.RoutePrefix("api/Account")]

    public class AccountController : ApiController
    {
        Hospital hospitalOperations = new Hospital();
        User userOperations = new User();

       

        [Route("HospitalRegister")]
        [HttpPost]
        public HttpResponseMessage HospitalRegister(HospitalRegister hospitalData)
        {
            // GetData g = new GetData();

            int returnaVal = hospitalOperations.RegisterHospital(hospitalData);


            if (returnaVal == 1)
                return Request.CreateResponse(HttpStatusCode.OK, "Submited successfully");

            else
                return Request.CreateResponse(HttpStatusCode.OK, "Unsuccessfully");
        }

        [Route("UserRegister")]
        [HttpPost]
        public HttpResponseMessage UserRegister(UserRegistration UserData)
        {
            // GetData g = new GetData();


            int returnaVal = userOperations.RegisterUser(UserData);

            if (returnaVal == 1)
                return Request.CreateResponse(HttpStatusCode.OK, "Submited successfully");
            else
                return Request.CreateResponse(HttpStatusCode.OK, "Unsuccessfully");
        }

        [AllowAnonymous]
        [Route("UserLogin")]
        [HttpPost]
        public HttpResponseMessage UserLogin(UserRegistration UserData)
        {

            //HealthCareEntities healthCareEntities = new HealthCareEntities();
            OAuthGrantResourceOwnerCredentialsContext context=null;
            

            int returnaVal = userOperations.LoginUser(UserData);



            if (returnaVal == 1)
            {
                IdentityUser user = new IdentityUser()
                {
                    UserName = UserData.Email
                };
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", UserData.Email));
                identity.AddClaim(new Claim("role", "user"));

                context.Validated(identity);
                return Request.CreateResponse(HttpStatusCode.OK, "Login successfully");
                    
            }
            else
                return Request.CreateResponse(HttpStatusCode.OK, "Login Unsuccessfully");
        }


        [Route("HospitalLogin")]
        [HttpPost]
        public HttpResponseMessage HospitalLogin(HospitalRegister hospitalData)
        {


            int returnaVal = hospitalOperations.LoginHospital(hospitalData);

            if (returnaVal == 1)
                return Request.CreateResponse(HttpStatusCode.OK, "Login successfully");
            else
                return Request.CreateResponse(HttpStatusCode.OK, "Login Unsuccessfully");
        }


        [System.Web.Http.Route("GetHospitalListData")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetHospitalListData(HospitalRegister hospitalData)
        {
            ViewHospitalModel viewHospitalModel = hospitalOperations.GetHospitalData();
            ////foreach (var items in viewHospitalModel.hospitalList )
            ////{
            ////    items.HospitalID;

            ////}
            ////return viewHospitalModel;
            return Request.CreateResponse(HttpStatusCode.OK, viewHospitalModel);

        }


        [System.Web.Http.Route("SendEmail")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage SendEmail()
        {

            //System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext=null;

            //   var principle = actionExecutedContext.Request.GetRequestContext().Principal;
            //string user_Name = ((ClaimsIdentity)principle.Identity).Claims.FirstOrDefault(x => x.Type == "UserName").Value;

            //string exeFile = (new System.Uri(Assembly.GetEntryAssembly().CodeBase)).AbsolutePath;
            //string exeDir = Path.GetDirectoryName(exeFile);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("asagaonkarneha@gmail.com", "Neha");
            mailMessage.To.Add("priyanka.lohar@jadeglobal.com");

            //foreach (var item in email)
            //{
            //    mailMessage.To.Add(item);
            //}

            mailMessage.Subject = "Emergency " + System.DateTime.Now.ToString();


            StreamReader streamReader = new StreamReader("D:\\Health\\Health\\EmailTemplates\\MailReport.html");

            string readFile = streamReader.ReadToEnd();
            string myString = "";
            myString = readFile;
            myString = myString.Replace("$$User$$", "Admin");
            myString = myString.Replace("$$Content$$", "Emergency service");

            mailMessage.Body = myString.ToString();
            mailMessage.IsBodyHtml = true;

            // SmtpClient smtp = new SmtpClient();

            SmtpClient smtp = new SmtpClient()
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("asagaonkarneha@gmail.com", "Neha1234"),
                EnableSsl = true
            };

            try
            {
                smtp.Send(mailMessage);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, "mail sent");
        }

        //[HttpGet]
        //public HttpResponseMessage test()
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, "Testing");
            
        //}
    }
}