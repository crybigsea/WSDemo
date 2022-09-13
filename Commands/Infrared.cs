using SuperSocket.Command;
using SuperSocket.ProtoBase;
using SuperSocket.WebSocket.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WSDemo.Models;

namespace WSDemo.Commands
{
    public class Infrared : IAsyncCommand<WebSocketSession, StringPackageInfo>
    {
        public async ValueTask ExecuteAsync(WebSocketSession session, StringPackageInfo package)
        {
            InfraredModel infrared = new InfraredModel()
            {
                DevTag = "Infrared",
                status = 1,
                infraredState = 1
            };
            string strInfrared = JsonSerializer.Serialize(infrared);
            if (MainPage.MessageHandler != null)
            {
                MainPage.MessageHandler.Invoke($"收到消息，来自{session.SessionID}：");
                MainPage.MessageHandler.Invoke($">>>>命令：Infrared");
                //MainPage.MessageHandler.Invoke($">>>>消息内容：{string.Join(",", package.Parameters)}");
                //MainPage.MessageHandler.Invoke($">>>>答复：{strInfrared}");
                MainPage.MessageHandler.Invoke($"--------------------------------------------------------------------------");
            }
            await session.SendAsync(strInfrared);
        }
    }
}
