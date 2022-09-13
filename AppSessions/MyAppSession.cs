using SuperSocket;
using SuperSocket.Channel;
using SuperSocket.ProtoBase;
using SuperSocket.WebSocket.Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WSDemo.AppSessions
{
    public class MyAppSession : WebSocketSession
    {
        private static ConcurrentDictionary<string, string> sessions = new ConcurrentDictionary<string, string>();

        private int _messageSent;

        public int MessageSent
        {
            get { return _messageSent; }
        }

        private int _messageClientReceived;

        public int MessageClientReceived
        {
            get { return _messageClientReceived; }
        }

        protected override async ValueTask OnSessionConnectedAsync()
        {
            if (!sessions.ContainsKey(this.SessionID))
            {
                if (sessions.TryAdd(this.SessionID, this.SessionID))
                {
                    MainPage.SendToClientEvent += MainPage_SendToClientEvent;
                    MainPage.ClientCountHandler?.Invoke(sessions.Count);
                }

                await this.SendAsync($"客户端已连接：{this.SessionID}");
            }
        }

        private void MainPage_SendToClientEvent(string message)
        {
            this.SendAsync(message).GetAwaiter();
        }

        protected override async ValueTask OnSessionClosedAsync(CloseEventArgs e)
        {
            if (sessions.ContainsKey(this.SessionID))
            {
                if (sessions.TryRemove(this.SessionID, out string result))
                {
                    MainPage.SendToClientEvent -= MainPage_SendToClientEvent;
                    MainPage.ClientCountHandler?.Invoke(sessions.Count);
                }
                await this.SendAsync($"客户端已断开连接：{this.SessionID}");
            }
        }

        public void Ack()
        {
            Interlocked.Increment(ref _messageClientReceived);
        }

        public override async ValueTask SendAsync(string message)
        {
            if (MainPage.MessageHandler != null)
            {
                MainPage.MessageHandler.Invoke($">>>>>>：{message}");
                MainPage.MessageHandler.Invoke($"--------------------------------------------------------------------------");
            }
            await base.SendAsync(message);
            Interlocked.Increment(ref _messageSent);
        }

        public void PrintStats()
        {
            var speed = (double)_messageSent / (double)this.LastActiveTime.Subtract(this.StartTime).TotalSeconds;
            Console.WriteLine($"Sent {_messageSent} messages, received {_messageClientReceived} messages, {speed:F1}.");
        }
    }
}
