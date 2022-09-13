using SuperSocket.Command;
using SuperSocket.ProtoBase;
using SuperSocket.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WSDemo.Models;

namespace WSDemo.Converters
{
    public class StringPackageConverter : IPackageMapper<WebSocketPackage, StringPackageInfo>
    {
        public StringPackageInfo Map(WebSocketPackage package)
        {
            var pack = new StringPackageInfo();

            CmdModel cmdInfo = new CmdModel();
            try
            {
                cmdInfo = JsonSerializer.Deserialize<CmdModel>(package.Message);
            }
            catch (Exception ex)
            {
                cmdInfo = new CmdModel
                {
                    DevTag = ""
                };
            }

            pack.Key = cmdInfo.DevTag;
            return pack;
        }
    }
}
