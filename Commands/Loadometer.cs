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
    public class Loadometer : IAsyncCommand<WebSocketSession, StringPackageInfo>
    {
        private readonly Random random = new Random();

        public async ValueTask ExecuteAsync(WebSocketSession session, StringPackageInfo package)
        {
            LoadometerModel loadometer = new LoadometerModel()
            {
                DevTag = "Loadometer",
                status = 1,
                weight = Math.Round(random.NextDouble() * 100, 2)
            };
            string strLoadometer = JsonSerializer.Serialize(loadometer);
            if (MainPage.MessageHandler != null)
            {
                MainPage.MessageHandler.Invoke($"收到消息，来自{session.SessionID}：");
                MainPage.MessageHandler.Invoke($">>>>命令：Loadometer");
                //MainPage.MessageHandler.Invoke($">>>>消息内容：{string.Join(",", package.Parameters)}");
                //MainPage.MessageHandler.Invoke($">>>>答复：{strLoadometer}");
                MainPage.MessageHandler.Invoke($"--------------------------------------------------------------------------");
            }
            await session.SendAsync(strLoadometer);
        }
    }
}
