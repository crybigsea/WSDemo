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
        private Queue<double> weights = new Queue<double>(new List<double>() { 0, 0, 0, 0, 0, 0, 0, 5.2, 6.5, 7.8, 15.3 });

        public Loadometer()
        {
            for (int i = 0; i < 100; i++)
            {
                weights.Enqueue(20.86);
            }
            weights.Enqueue(0);
            weights.Enqueue(0);
        }

        public async ValueTask ExecuteAsync(WebSocketSession session, StringPackageInfo package)
        {
            double weight = weights.Dequeue();
            weights.Enqueue(weight);
            LoadometerModel loadometer = new LoadometerModel()
            {
                DevTag = "Loadometer",
                status = 1,
                weight = weight
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
