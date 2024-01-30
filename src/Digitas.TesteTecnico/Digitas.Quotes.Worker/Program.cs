using Digitas.Quotes.Worker;
using Digitas.Quotes.Worker.Application.Common.Extensions.DependencyInjection;
using Digitas.Quotes.Worker.Infra.Extension.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = new HostBuilder()
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config
            .AddJsonFile($"appsettings.json", false, true)
            .AddEnvironmentVariables();
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
        logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
    })
    .ConfigureServices((hostContext, services) =>
    {
        var config = hostContext.Configuration;

        services.AddApplicationServices();
        services.AddInfrastructureServices(config);
    });

var host = builder.Build();

var workerService = host.Services.GetRequiredService<WorkerInitializerJob>();
await workerService.ExecuteAsync();


//using Digitas.Quotes.Worker.Infra.BitStamp.Requests;
//using Digitas.Quotes.Worker.Infra.BitStamp.Responses;
//using System.Net.WebSockets;
//using System.Text;
//using System.Text.Json;

//var ws = new ClientWebSocket();
//await ws.ConnectAsync(new Uri("wss://ws.bitstamp.net"), CancellationToken.None);

//Console.WriteLine($"Connection State: {ws.State}\n");

//var data = new SubscriptionData("order_book_btcusd");

//var subscription = new SubscriptionRequest("bts:subscribe", data);
//var subscribeMsg = JsonSerializer.Serialize(subscription);
//var bufferSubscricao = Encoding.UTF8.GetBytes(subscribeMsg);

//Console.WriteLine($"1: Subscribing Channel Live order book | Event {subscription.Event} | Channel {subscription.Data.Channel} ");

//Console.WriteLine($"2: Connection Request: {subscribeMsg}");

//try
//{
//    await ws.SendAsync(buffer: bufferSubscricao,
//                   messageType: WebSocketMessageType.Binary,
//                   endOfMessage: true,
//                   CancellationToken.None);

//    await ConfirmConnection(ws);

//    await ReceiveData(ws);
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.ToString());
//}
//finally
//{
//    if (ws.State == WebSocketState.Open)
//        await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Fechamento normal", CancellationToken.None);

//    ws.Dispose();
//}

//static async Task ConfirmConnection(ClientWebSocket webSocket)
//{
//    if (webSocket.State == WebSocketState.Open)
//    {
//        byte[] buffer = new byte[100];

//        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

//        if (result.MessageType == WebSocketMessageType.Text)
//        {
//            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);

//            Console.WriteLine($"3: Conection Response: {receivedMessage}");

//            var confirmConnectionObject = JsonSerializer.Deserialize<BookOrderResponse>(receivedMessage);
//            Console.WriteLine($"Evento recebido: {confirmConnectionObject!.Event}");
//            Console.WriteLine($"Channel subscrito: {confirmConnectionObject!.Channel}");
//        }
//    }
//}

//static async Task ReceiveData(ClientWebSocket webSocket)
//{
//    byte[] buffer = new byte[4739];

//    while (webSocket.State == WebSocketState.Open)
//    {
//        WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

//        //Console.WriteLine($"mensagem completa: {result.EndOfMessage}");

//        if (result.MessageType == WebSocketMessageType.Text && result.Count != 0)
//        {
//            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);

//            var newUpdatesData = JsonSerializer.Deserialize<BookOrderResponse>(receivedMessage);

//            Console.WriteLine($"timestamp: {newUpdatesData!.Data.Timestamp} && {newUpdatesData!.Data.Microtimestamp}");

//            //Console.WriteLine($"Mensagem recebida: {receivedMessage}");
//        }
//        else if (result.MessageType == WebSocketMessageType.Close)
//        {
//            Console.WriteLine("Recebido comando de fechamento do servidor WebSocket.");
//            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Fechamento normal", CancellationToken.None);
//        }
//    }
//}



//try
//{
//    var receivedTask = Task.Run(async () =>
//    {
//        var buffer = new byte[1024];
//        var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

//        var msg = Encoding.UTF8.GetString(buffer, 0, result.Count);

//        Console.WriteLine(msg);

//    });

//    await receivedTask;
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.ToString());
//}


