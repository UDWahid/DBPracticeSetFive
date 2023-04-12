using DOOR.EF.Data;
using Microsoft.AspNetCore.Mvc;
using DOOR.Shared.Utils;

namespace DOOR.Server.Controllers.Common
{
    public class BaseController : Controller
    {

        protected DOOROracleContext _context;
        protected readonly OraTransMsgs _OraTranslateMsgs;

        public BaseController(DOOROracleContext DBcontext,
    OraTransMsgs _OraTransMsgs)
        {
            _context = DBcontext;
            _OraTranslateMsgs = _OraTransMsgs;
        }
    }
}
