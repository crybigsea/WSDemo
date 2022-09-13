using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperSocket;
using SuperSocket.Command;
using SuperSocket.ProtoBase;
using SuperSocket.WebSocket.Server;
using WSDemo.AppSessions;
using WSDemo.Commands;
using WSDemo.Converters;

namespace WSDemo;

public partial class MainPage : ContentPage
{
    public static Action<string> MessageHandler = null;
    public static Action<int> ClientCountHandler = null;
    public static event Action<string> SendToClientEvent = null;

    private IHost host = null;

    public MainPage()
    {
        InitializeComponent();
        CloseBtn.IsEnabled = false;
        txtMessage.Text = @"{""DevTag"": ""HKVLPR"",""status"": 1,""MsgType"": 0,""cardNo"": ""津C35502"",""Date"": ""2022-08-05 10:03:02"",""IP"": ""192.168.1.44"",""Image"": """"}";

        host = WebSocketHostBuilder.Create()
                .UseCommand<StringPackageInfo, StringPackageConverter>(commandOptions =>
                {
                    commandOptions.AddCommand<Loadometer>();
                    commandOptions.AddCommand<PlateNumber>();
                    commandOptions.AddCommand<Infrared>();
                })
                .UseSession<MyAppSession>()
                .ConfigureAppConfiguration((hostCtx, configApp) =>
                {
                    configApp.AddInMemoryCollection(new Dictionary<string, string>
                    {
                    { "serverOptions:name", "TestServer" },
                    { "serverOptions:listeners:0:ip", "Any" },
                    { "serverOptions:listeners:0:port", txtPort.Text }
                    });
                })
                .ConfigureLogging((hostCtx, loggingBuilder) =>
                {
                    loggingBuilder.AddConsole();
                })
                .Build();

        MessageHandler = message =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (txtContent.Text != null && txtContent.Text.Length > 2000)
                {
                    txtContent.Text = txtContent.Text.Substring(1000);
                }
                else
                {
                    txtContent.Text += "\r\n" + message;
                }
            });
        };
        ClientCountHandler = count =>
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (CounterBtn.Text.IndexOf('(') >= 0)
                {
                    CounterBtn.Text = CounterBtn.Text.Substring(0, CounterBtn.Text.IndexOf('(')) + $"({count})";
                }
                else
                {
                    CounterBtn.Text = CounterBtn.Text + $"({count})";
                }
            });
        };
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        try
        {
            host.RunAsync();

            CounterBtn.IsEnabled = false;
            CounterBtn.Text = "已启动";
            //CloseBtn.IsEnabled = true;
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"服务启动失败\r\n{ex.Message}", "确定");
        }
    }

    private void but_Send_Clicked(object sender, EventArgs e)
    {
        SendToClientEvent?.Invoke(txtMessage.Text);
    }

    private void CloseBtn_Clicked(object sender, EventArgs e)
    {
        try
        {
            host.StopAsync();

            CounterBtn.IsEnabled = true;
            CounterBtn.Text = "启动";
            CloseBtn.IsEnabled = false;
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"服务关闭失败\r\n{ex.Message}", "确定");
        }
    }
}

