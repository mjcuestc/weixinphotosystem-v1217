using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MMPSystemManager.DBContext;
using MMPSystemManager.Module;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;

namespace MMPSystemManager.Controllers
{
    [Produces("application/json")]
    [Route("api/SystemApi")]
    public class SystemApiController : Controller
    {
        private readonly MMPContext _context;

        public SystemApiController(MMPContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public bool Login()
        {
            IFormCollection req = Request.Form;
            StringValues id, passwd;
            System.Collections.Generic.ICollection<string> t3;

            t3 = req.Keys;
            string[] tt = new string[20];
            int i = 0;
            foreach (string x in t3)
            {
                tt[i++] = x;
            }
            if (i != 2)
                return false;

            req.TryGetValue(tt[0], out id);//ID
            req.TryGetValue(tt[1], out passwd);//password

            if ((string.Compare(id, null) == 0) || (string.Compare(passwd, null) == 0))
            {
                return false;
            }
            bool tableempty = true;
            foreach (var ty in _context.Admins)
            {
                tableempty = false;
                break;
            }
            if (tableempty)             //当管理员表为空时  登录账号 密码为SuperAdministrator SuperAdministrator
            {
                if ((string.Compare((string)id, "SuperAdministrator") == 0) && (string.Compare((string)passwd, "SuperAdministrator") == 0))
                    return true;
                else return false;
            }

            var db = _context.Admins.Find(id);

            if (db == null)
                return false;
            else
            {
                if (string.Compare(db.AdminPasswd, passwd) == 0)
                {
                    var linq = (from obj in _context.Admins
                                where obj.AdminNumber == id
                                select obj).SingleOrDefault();

                    linq.AdminOnline = true;
                    linq.AdminLoginTime = System.DateTime.Now;

                    int row = _context.SaveChanges();
                    if (row > 0)
                    {
                        return true;
                    }
                    else return false;

                }
                else return false;
            }
        }

        [HttpPost]
        [Route("Insert")]
        public bool Insert()
        {
            IFormCollection req = Request.Form;
            String[] val = new String[20];
            System.Collections.Generic.ICollection<string> t3;

            t3 = req.Keys;
            string[] nam = new string[20];
            int i = 0;
            StringValues te;
            foreach (string x in t3)
            {
                nam[i] = x;
                req.TryGetValue(x, out te);
                val[i] = te;
                i++;
            }

            if (i != 4)
                return false;
            var dest = new Userinfo
            {

                UserNumber = Guid.NewGuid(),
                UserName = "" + val[0] + "",
                UserWechatName = "" + null + "",
                UserId = "" + OpenIDPra.UserOpenID + "",
                UserContactPhone = "" + val[1] + "",
                UserContactEmail = "" + null + "",
                UserFacepict = "" + null + "",
                UserReservationPos = "" + val[2] + "",
                UserReservationTime = "" + val[3] + "",
                UserPicTime = System.DateTime.Now,
                Remark = "" + null + ""
            };
            
            _context.Userinfos.Add(dest);
            int row = _context.SaveChanges();
            if (row > 0)
            {
                return true;
            }
            else return false;
            
        } 

        [HttpPost]
        [Route("Research")]
        public JArray Research()
        {
            IFormCollection req = Request.Form;
            System.Collections.Generic.ICollection<string> t3;

            t3 = req.Keys;
            string[] tt = new string[20];
            int i = 0;
            foreach (string x in t3)
            {
                tt[i++] = x;
            }
            StringValues tel;
            req.TryGetValue(tt[0],out tel);
            JObject staff1 = new JObject();
            JArray staff = new JArray();
            if (i != 1)
            {
                staff1.Add(new JProperty("Requestformat", "false"));
                staff.Add(new JObject(staff1));
                return staff;
            }
            var query = from obj in _context.Userinfos
                        where obj.UserId == OpenIDPra.UserOpenID
                        select new Userinfo
                        {
                            UserNumber = obj.UserNumber,
                            UserName = obj.UserName,
                            UserWechatName = obj.UserWechatName,
                            UserId = obj.UserId,
                            UserContactPhone = obj.UserContactPhone,
                            UserContactEmail = obj.UserContactEmail,
                            UserFacepict = obj.UserFacepict,
                            UserReservationPos = obj.UserReservationPos,
                            UserReservationTime = obj.UserReservationTime,
                            UserPicTime = obj.UserPicTime,
                            Remark = obj.Remark
                        };
            var res = query.ToList();
            for (i = 0; i < res.Count; i++)
            {
                staff1.Add(new JProperty("UserNumber", "" + res[i].UserNumber + ""));
                staff1.Add(new JProperty("UserName", "" + res[i].UserName + ""));
                staff1.Add(new JProperty("UserWechatName", "" + res[i].UserWechatName + ""));
                staff1.Add(new JProperty("UserId", "" + res[i].UserId + ""));
                staff1.Add(new JProperty("UserContactPhone", "" + res[i].UserContactPhone + ""));
                staff1.Add(new JProperty("UserContactEmail", "" + res[i].UserContactEmail + ""));
                staff1.Add(new JProperty("UserFacepict", "" + res[i].UserFacepict + ""));
                staff1.Add(new JProperty("UserReservationPos", "" + res[i].UserReservationPos + ""));
                staff1.Add(new JProperty("UserReservationTime", "" + res[i].UserReservationTime + ""));
                staff1.Add(new JProperty("UserPicTime", "" + res[i].UserPicTime + ""));
                staff1.Add(new JProperty("Remark", "" + res[i].Remark + ""));

                staff.Add(new JObject(staff1));
                staff1.RemoveAll();
            }
            return staff;
        }

        [HttpPost]
        [Route("Delete")]
        public bool Delete()
        {
            IFormCollection req = Request.Form;
            StringValues id;
            System.Collections.Generic.ICollection<string> t3;

            t3 = req.Keys;
            string[] tt = new string[20];
            int i = 0;
            foreach (string x in t3)
            {
                tt[i++] = x;
            }
            req.TryGetValue(tt[0], out id);//ID
            
            var ty = _context.Userinfos.Find(id);
            if (object.Equals(ty, null))
            {
                return false;
            }
            else
            {
                _context.Userinfos.Remove(ty);
                int row = _context.SaveChanges();
                if (row > 0)
                {
                    return true;
                }
                else return false;
            }
        }
    }
}