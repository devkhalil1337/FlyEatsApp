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
        private int? Id { get; set; }


        public ResponseModel()
        {

        }


        public void setId(int Id)
        {
            this.Id = Id;
        }
        public int GetId()
        {
            return (int)this.Id;
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
