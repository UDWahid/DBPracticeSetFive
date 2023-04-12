using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOOR.Shared.Utils
{
    public interface IOraTransMsgs
    {
        public void LoadMsgs();
        public string TranslateMsg(string strMessage);
    }
}
