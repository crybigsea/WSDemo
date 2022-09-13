using SuperSocket.Command;
using SuperSocket.ProtoBase;
using SuperSocket.WebSocket.Server;
using System.Text.Json;
using System.Text.Json.Serialization;
using WSDemo.Models;

namespace WSDemo.Commands
{
    public class PlateNumber : IAsyncCommand<WebSocketSession, StringPackageInfo>
    {
        public async ValueTask ExecuteAsync(WebSocketSession session, StringPackageInfo package)
        {
            HKVLPRModel plateNumber = new HKVLPRModel()
            {
                DevTag = "HKVLPR",
                status = 1,
                MsgType = 0,
                cardNo = "津C35502",
                Date = "2022-08-05 10:03:02",
                IP = "192.168.1.44",
                Image = ""
            };
            string strPlateNumber = JsonSerializer.Serialize(plateNumber);
            if (MainPage.MessageHandler != null)
            {
                MainPage.MessageHandler.Invoke($"收到消息，来自{session.SessionID}：");
                MainPage.MessageHandler.Invoke($">>>>命令：PlateNumber");
                //MainPage.MessageHandler.Invoke($">>>>消息内容：{string.Join(",", package.Parameters)}");
                //MainPage.MessageHandler.Invoke($">>>>答复：{strPlateNumber}");
                MainPage.MessageHandler.Invoke($"--------------------------------------------------------------------------");
            }
            await session.SendAsync(strPlateNumber);
        }
    }
}
