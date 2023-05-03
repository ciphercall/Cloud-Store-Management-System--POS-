using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AS_Store_GL.Models;
using AS_Store_GL.Models.DTO;

namespace AS_Store_GL.Controllers.Api
{
    public class ApiChequePaymentController : ApiController
    {
        Store_GL_DbContext db = new Store_GL_DbContext();

        //Datetime formet
        IFormatProvider dateformat = new System.Globalization.CultureInfo("fr-FR", true);
        TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time");
        public DateTime td;

        //Get Ip ADDRESS,Time & user PC Name
        public string strHostName;
        public IPHostEntry ipHostInfo;
        public IPAddress ipAddress;

        public ApiChequePaymentController()
        {
            strHostName = System.Net.Dns.GetHostName();
            ipHostInfo = Dns.Resolve(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            td = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);
        }









        //[System.Web.Http.Route("api/ApiRoomNoController/GetMediCareData")]
        [System.Web.Http.HttpGet]
        public IEnumerable<Gl_StransDTO> GetPaymentData(string cid, string date, string type)
        {
            Int64 compid = Convert.ToInt64(cid);
            DateTime dateTime = Convert.ToDateTime(date);
            var find_GridData = (from m in db.GlStransDbSet
                                 where m.COMPID == compid && m.TRANSDT == dateTime && m.TRANSTP == type 
                                 && (m.TRANSMODE== "A/C PAYEE CHEQUE" || m.TRANSMODE == "CASH CHEQUE")
                                 select new
                                 {
                                     m.Gl_StransID,
                                     m.COMPID,
                                     m.TRANSTP,
                                     m.TRANSDT,
                                     m.CHEQUEDT,
                                     m.DEBITCD,
                                     m.AMOUNT,
                                     m.CHQPAYTO,

                                     m.INSIPNO,
                                     m.INSLTUDE,
                                     m.INSTIME,
                                     m.INSUSERID,
                                 }).OrderBy(e => e.CHEQUEDT).ToList();

            if (find_GridData.Count == 0)
            {
                yield return new Gl_StransDTO
                {
                    count = 1, //no data found
                };
            }
            else
            {
                foreach (var s in find_GridData)
                {
                    String chequeDate = "";
                    if (s.CHEQUEDT != null)
                    {
                        DateTime chequeDateTime = Convert.ToDateTime(s.CHEQUEDT);
                        chequeDate = chequeDateTime.ToString("dd-MMM-yyyy");
                    }
                   
                    

                    String paymentParticulars = "";
                    var findpaymentParticulars = (from child in db.GlAcchartDbSet
                       where child.COMPID == compid && child.ACCOUNTCD == s.DEBITCD 
                        select new { child.ACCOUNTNM}).ToList();
                    foreach (var x in findpaymentParticulars)
                    {
                        paymentParticulars = x.ACCOUNTNM;
                    }
                    


                    yield return new Gl_StransDTO
                    {
                        Id = s.Gl_StransID,
                        COMPID = Convert.ToInt64(s.COMPID),
                        TRANSTP = Convert.ToString(s.TRANSTP),
                        TRANSDT = Convert.ToDateTime(s.TRANSDT),
                        DEBITCD =s.DEBITCD,
                        PaymentParticulars = paymentParticulars,
                        CHEQUEDT = chequeDate,
                        AMOUNT = s.AMOUNT,
                        CHQPAYTO = s.CHQPAYTO,

                        INSUSERID = s.INSUSERID,
                        INSLTUDE = s.INSLTUDE,
                        INSTIME = s.INSTIME,
                        INSIPNO = s.INSIPNO,
                    };
                }
            }
        }


        
        //[Route("api/ApiRoomNoController/grid/UpdateData")]
        [HttpPost]
        public HttpResponseMessage UpdateData(Gl_StransDTO model)
        {
            var data_find = (from n in db.GlStransDbSet where n.Gl_StransID == model.Id && n.COMPID == model.COMPID && n.TRANSTP == model.TRANSTP && n.TRANSDT == model.TRANSDT && n.DEBITCD==model.DEBITCD select n).ToList();

            foreach (var item in data_find)
            {
                item.CHQPAYTO = model.CHQPAYTO;

                item.USERPC = strHostName;
                item.UPDIPNO = ipAddress.ToString();
                item.UPDLTUDE = Convert.ToString(model.INSLTUDE);
                item.UPDTIME = Convert.ToDateTime(td);
                item.UPDUSERID = Convert.ToInt16(model.INSUSERID);

            }
            db.SaveChanges();

            //Log data save from GL_STRANS Tabel
            //RoomController controller = new RoomController();
            //controller.update_HML_ROOM_LogData(model);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, model);
            return response;

        }
        
        
    }
}
