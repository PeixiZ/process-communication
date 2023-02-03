using System.Messaging;

public class Server
{
    public static void Main()
    {
        /// <summary>
        /// Private$表示在建立私有队列,使用csc生成时/r:System.Messaging.dll
        /// </summary>
        string path = """.\Private$\MY""";
        MessageQueue message = null;
        
        string info = "something to run";

        if (!MessageQueue.Exists(path))
            MessageQueue.Create(path);                
        
        message = new MessageQueue(path);
        message.Label = "test label";
        
        //使用默认格式发送时，队列中储存的是xml格式的消息
        message.Send(info);
        message.Dispose();
        //单独运行此程序后，会发现队列仍然存在，而进程早已退出，在系统重开机后队列也会消失
    }
}