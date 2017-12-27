using MMPSystemManager.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MMPSystemManager.Module
{
    public class Admin
    {
        [Key]
        public string AdminNumber { get; set; }    //编号
        public string AdminId { get; set; }         //身份证号
        public string AdminGrade { get; set; }      //权限等级
        public string AdminName { get; set; }       //姓名
        public string AdminPasswd { get; set; }       //密码
        public string AdminContactPhone { get; set; }        //联系电话
        public string AdminContactEmail { get; set; }        //联系邮箱
        public bool AdminOnline { get; set; }         //是否在线
        public string AdminPicture { get; set; }            //人脸图片
        public DateTime AdminPicTime { get; set; }         //人脸图片采集时间
        public DateTime AdminPicUpdateTime { get; set; }       //最新更新图片时间
        public DateTime AdminLoginTime { get; set; }       //最近登录时间
        public string AdminIdPict { get; set; }             //身份证图片
        public string Remark { get; set; }              //备注
    }
}
