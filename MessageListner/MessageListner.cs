using MessageListner;
using System;

namespace MSMQListner
{
    public class MessageListner
    {
        public static void Main()
        {
            var listener = new MSMQListener(@".\Private$\myqueue");
            listener.MessageReceived += new MessageReceivedEventHandler(listener_MessageReceived);
            listener.Start();
            listener.SendMail("name","email","data");
            Console.WriteLine("Read Message");
            Console.ReadLine();
            listener.Stop();
        }

        public static void listener_MessageReceived(object sender, MessageEventArgs args)
        {
            Console.WriteLine(args.MessageBody);
        }
    }
}