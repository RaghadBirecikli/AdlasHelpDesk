using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using System.Numerics;
using System.Net.Mail;

namespace AdlasHelpDesk.Application.UserHelpers
{
    public static class Functions
    {
        public static string MD5(string text)
        {
            byte[] result = new byte[text.Length];
            MD5 md = new MD5CryptoServiceProvider();
            UTF8Encoding encode = new UTF8Encoding();
            result = md.ComputeHash(encode.GetBytes(text));
            return BitConverter.ToString(result).Replace("-", "");
        }
        //creating cookies

        public static ClaimsPrincipal CreateClaims(MemberDto signedMember)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("SignedMember", JsonConvert.SerializeObject(signedMember)));
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme));
            return claimsPrincipal;
        }
        public static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            MemberExpression memberExpression = propertyExpression.Body as MemberExpression;

            if (memberExpression == null)
            {
                throw new ArgumentException("Invalid property expression", nameof(propertyExpression));
            }

            PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;

            if (propertyInfo == null)
            {
                throw new ArgumentException("Invalid property expression", nameof(propertyExpression));
            }

            return propertyInfo.Name;
        }

        public static void DeleteImage<T>(string imagePath, T entity, IHostingEnvironment hostEnvironment)
        {
            Type tableClass = typeof(T);
            string tableName = tableClass.Name;
            string fullPath = Path.Combine(hostEnvironment.WebRootPath, imagePath);
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
        public static async Task<string> SaveImage<T>(IFormFile imageFile, T entity, IHostingEnvironment hostEnvironment)
        {
            Type tableClass = typeof(T);
            string tableName = tableClass.Name;

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);

            string relativePath = Path.Combine("Uploads", tableName, uniqueFileName); // Construct path directly

            string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath);
            string filePath = Path.Combine(uploadsFolder, relativePath);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return relativePath;
        }


        public static bool SendEmail(SmtpModel smtp, string body, string subject, string toAddress, Attachment attech)
        {
            bool sendEmail = false;
            string EmailBody = @$"<body>
<div style='width: 100%;height: 100%;background-color: #f2f2f2;padding-top: 10px;word-wrap:break-word;'> 
<div id='main' style='width:auto;  background-color: #f2f2f2;font-size: 14px;  margin: 0px auto;font-family: Helvetica;'>
<div style='float:right;display:inline-block;'><img src='https://sozdijital.com/images/Z%20Y%C3%B6netim%20Logo.png' height='60' width ='80'  style='padding-right:30px'></div>
</div> <hr /> 
<div id='body' style='text-align: justify; min-height: 200px;font-size: 16px;line-height:30px; padding-right: 15px;padding-left: 15px;'>
#CONTENT#  </div>
<div id='footer' style='padding-top: 20px;'>
<table style='width: 100%;background-color: #363334;padding-left: 5%;padding-right: 5%'>
<tr style='vertical-align: top'> <td style='width:50%'><div style='color:white;'> 
Söz Dijital 2016-{DateTime.Now.Year}</div></td> 
<td style='width:50%'> <div style='color:white;'> 
&copy; Tüm hakları saklıdır.</div>   </td> </tr></table> </div> </div> </div> </body>";
            EmailBody = EmailBody.Replace("#CONTENT#", body);

            try
            {
                SmtpClient mySmtpClient = new SmtpClient(smtp.Server, smtp.Port);
                mySmtpClient.UseDefaultCredentials = false;
                System.Net.NetworkCredential basicAuthenticationInfo = new
                   System.Net.NetworkCredential(smtp.User, smtp.Password);
                mySmtpClient.Credentials = basicAuthenticationInfo;
                mySmtpClient.EnableSsl = smtp.SSL;
                mySmtpClient.Timeout = smtp.Timeout;

                MailMessage myMail = new MailMessage();
                myMail.From = new MailAddress(smtp.User, smtp.UserAlias);
                foreach (string item in toAddress.Split(';'))
                {
                    if (!string.IsNullOrEmpty(item) && item.IndexOf('@') != -1)
                        myMail.To.Add(new MailAddress(item));
                }

                myMail.Subject = subject;
                myMail.SubjectEncoding = System.Text.Encoding.UTF8;
                myMail.Body = EmailBody;
                myMail.BodyEncoding = System.Text.Encoding.UTF8;
                myMail.IsBodyHtml = true;
                if (attech != null)
                    myMail.Attachments.Add(attech);

                mySmtpClient.Send(myMail);
                sendEmail=true;
            }
            catch (SmtpException ex)
            {
                sendEmail = false;
                throw new ApplicationException
                  ("SmtpException has occured: " + ex.Message);
            }
            catch (Exception ex)
            {
                sendEmail = false;
                throw ex;
            }
            return sendEmail;
        }
    }
}
