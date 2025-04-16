using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DAL.Entities.Identity;

namespace Demo.BLL.Common.Services.EmailSettings
{
    public interface IEmailSettings
    {
        public void SendEmail(Email email);
    }
}
