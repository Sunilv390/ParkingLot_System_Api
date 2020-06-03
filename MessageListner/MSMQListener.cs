using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Messaging;

namespace MessageListner
{
    public delegate void MessageReceivedEventHandler(object sender, MessageEventArgs args);
    public class MSMQListener
    {

        private bool _listen;
        //    BinaryMessageFormatter _types;
        MessageQueue _queue;

        public event MessageReceivedEventHandler MessageReceived;

        public MSMQListener(string queuePath)
        {
            _queue = new MessageQueue(queuePath);
        }

        public void Start()
        {
            _listen = true;
            // Using only the XmlMessageFormatter. You can use other formatters as well

            _queue.Formatter = new BinaryMessageFormatter();
            _queue.PeekCompleted += new PeekCompletedEventHandler(OnPeekCompleted);
            _queue.ReceiveCompleted += new ReceiveCompletedEventHandler(OnReceiveCompleted);

            StartListening();
        }

        public void Stop()
        {
            _listen = false;
            _queue.PeekCompleted -= new PeekCompletedEventHandler(OnPeekCompleted);
            _queue.ReceiveCompleted -= new ReceiveCompletedEventHandler(OnReceiveCompleted);

        }

        private void StartListening()
        {
            if (!_listen)
            {
                return;
            }

            // The MSMQ class does not have a BeginRecieve method that can take in a 
            // MSMQ transaction object. This is a workaround – we do a BeginPeek and then 
            // recieve the message synchronously in a transaction.
            // Check documentation for more details
            if (_queue.Transactional)
            {
                _queue.BeginPeek();
            }
            else
            {
                _queue.BeginReceive();

            }

        }

        private void OnPeekCompleted(object sender, PeekCompletedEventArgs e)
        {
            _queue.EndPeek(e.AsyncResult);
            MessageQueueTransaction trans = new MessageQueueTransaction();
            Message msg = null;
            try
            {
                trans.Begin();
                msg = _queue.Receive(trans);
                trans.Commit();

                StartListening();

                FireRecieveEvent(msg.Body);
            }
            catch
            {
                trans.Abort();
            }
        }

        private void FireRecieveEvent(object body)
        {
            if (MessageReceived != null)
            {
                MessageReceived(this, new MessageEventArgs(body));
            }
        }

        private void OnReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            Message msg = _queue.EndReceive(e.AsyncResult);

            StartListening();

            FireRecieveEvent(msg.Body);
        }

        //SMTP method to send mail
        //public void SendMail(string name, string mail, string data)
        //{
        //    var message = new MimeMessage();

        //    message.From.Add(new MailboxAddress(name, mail));

        //    message.To.Add(new MailboxAddress("Parking Lot", "sunilv390@gmail.com"));

        //    message.Subject = "Registration";

        //    message.Body = new TextPart("plain")
        //    {
        //        Text = data
        //    };

        //    using (var client = new SmtpClient())
        //    {
        //        client.Connect("smtp.gmail.com", 587, false);
        //        client.Authenticate("sunilv390@gmail.com", "Sunilverma@390");
        //        client.Send(message);
        //        client.Disconnect(true);
        //    }
        //}
    }

    public class MessageEventArgs : EventArgs
    {
        private object _messageBody;

        private string name;
        private string mail;
        public object MessageBody
        {
            get { return _messageBody; }
        }

        public string Email
        {
            get { return name; }
        }

        public string UserName
        {
            get { return name; }
        }


        public MessageEventArgs(object body)
        {
            _messageBody = body;
        }
    }
}