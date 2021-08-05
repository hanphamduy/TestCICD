using Emr.Pluggable.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converter;
namespace Emr.Plugins.Tranform.Test.LayThongTinChuyenVien
{
    public class Tranform : ITransformPlugin
    {
        public string Name
        {
            get { return "LayThongTinChuyenVien"; }
        }

        public string Version
        {
            get { return "1.0.0.0"; }
        }

        JToken ITransformPlugin.Tranform(JToken data, Dictionary<string, object> requestParams)
        {
            try
            {
                var convert = new EmrConverter();
                var resultData = convert.ConvertHisObjectToEmrObject(data);

                return resultData;

            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xãy ra, vui lòng liên hệ Quản trị viên!.", ex);
            }
        }
    }
}
