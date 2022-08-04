using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ResponseModel
    {
        public string message { get; set; }
        public bool success { get; set; }


        public ResponseModel()
        {

        }

        public object onSuccess()
        {
            var result = new ResponseModel();
            result.success = true;
            result.message = "";
            return result;
        }

        public object onError(String error)
        {
            var result = new ResponseModel();
            result.success = true;
            result.message = error;
            return result;
        }

    }
}
