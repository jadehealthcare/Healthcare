using Health.Model.ViewModels;
using Health.Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace Health.Controllers
{
    [System.Web.Http.RoutePrefix("api/Account")]

    public class AccountController : ApiController
    {
        Hospital hospitalOperations = new Hospital();
        User userOperations = new User();

        [System.Web.Http.Route("HospitalRegister")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage HospitalRegister(HospitalRegister hospitalData)
        {
            // GetData g = new GetData();

            int returnaVal = hospitalOperations.RegisterHospital(hospitalData);


            if (returnaVal == 1)
                return Request.CreateResponse(HttpStatusCode.OK, "Submited successfully");

            else
                return Request.CreateResponse(HttpStatusCode.OK, "Unsuccessfully");
        }

        [System.Web.Http.Route("UserRegister")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage UserRegister(UserRegistration UserData)
        {
            // GetData g = new GetData();


            int returnaVal = userOperations.RegisterUser(UserData);

            if (returnaVal == 1)
                return Request.CreateResponse(HttpStatusCode.OK, "Submited successfully");
            else
                return Request.CreateResponse(HttpStatusCode.OK, "Unsuccessfully");
        }

        [System.Web.Http.Route("UserLogin")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage UserLogin(UserRegistration UserData)
        {
            //var authenticated = false;
            //if (authenticated || (UserData.Email == "a" && UserData.Password == "a"))
            //{

            //    var identity = new ClaimsIdentity(Startup.OAuthOptions.AuthenticationType);
            //    identity.AddClaim(new Claim(ClaimTypes.Name, UserData.Email));
            //    identity.AddClaim(new Claim("Password", UserData.Password));

            //    AuthenticationTicket ticket = new AuthenticationTicket(identity, new AuthenticationProperties());
            //    var currentUtc = new SystemClock().UtcNow;
            //    ticket.Properties.IssuedUtc = currentUtc;
            //    ticket.Properties.ExpiresUtc = currentUtc.Add(TimeSpan.FromMinutes(30));

            //    var token = Startup.OAuthOptions.AccessTokenFormat.Protect(ticket);
            //    var response = new HttpResponseMessage(HttpStatusCode.OK)
            //    {
            //        Content = new ObjectContent<object>(new
            //        {
            //            UserName = UserData.Email,
            //            AccessToken = token
            //        }, Configuration.Formatters.JsonFormatter)
            //    };

            //    FormsAuthentication.SetAuthCookie(UserData.Email, true);

            //    HttpContext.Current.Session["UserEmail"] = UserData.Email;

            //    return response;


            //    //return new HttpResponseMessage(HttpStatusCode.BadRequest);
            //}
            int returnaVal = userOperations.LoginUser(UserData);

            if (returnaVal == 1)
                return Request.CreateResponse(HttpStatusCode.OK, "Login successfully");
            else
                return Request.CreateResponse(HttpStatusCode.OK, "Login Unsuccessfully");
        }


        [System.Web.Http.Route("HospitalLogin")]
        [System.Web.Http.HttpPost]
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
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
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