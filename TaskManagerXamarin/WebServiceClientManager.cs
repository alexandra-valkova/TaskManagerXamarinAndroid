using System;
using System.ServiceModel;
using TaskManagerApiWCF.Models;

namespace TaskManagerAndroid
{
    public class WebServiceClientManager
    {
        private static readonly EndpointAddress UserEndPoint = new EndpointAddress("http://100.98.79.97:58370/WcfServices/UserServices.svc");
        private static readonly EndpointAddress TaskEndPoint = new EndpointAddress("http://100.98.79.97:58370/WcfServices/TaskServices.svc");
        private static readonly EndpointAddress LogworkEndPoint = new EndpointAddress("http://100.98.79.97:58370/WcfServices/LogworkServices.svc");
        private static readonly EndpointAddress CommentEndPoint = new EndpointAddress("http://100.98.79.97:58370/WcfServices/CommentServices.svc");

        public static BaseServicesOf_UserModelClient UserClient { get; set; }
        public static BaseServicesOf_TaskModelClient TaskClient { get; set; }
        public static BaseServicesOf_LogworkModelClient LogworkClient { get; set; }
        public static BaseServicesOf_CommentModelClient CommentClient { get; set; }

        public static void InitializeWebServiceClient(Type type)
        {
            if (type == typeof(UserModel))
            {
                UserClient = new BaseServicesOf_UserModelClient(CreateBasicHttp(), UserEndPoint);
            }
            else if (type == typeof(TaskModel))
            {
                TaskClient = new BaseServicesOf_TaskModelClient(CreateBasicHttp(), TaskEndPoint);
            }
            else if (type == typeof(LogworkModel))
            {
                LogworkClient = new BaseServicesOf_LogworkModelClient(CreateBasicHttp(), LogworkEndPoint);
            }
            else if (type == typeof(CommentModel))
            {
                CommentClient = new BaseServicesOf_CommentModelClient(CreateBasicHttp(), CommentEndPoint);
            }
        }

        private static BasicHttpBinding CreateBasicHttp()
        {
            BasicHttpBinding binding = new BasicHttpBinding
            {
                Name = "basicHttpBinding",
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647
            };
            TimeSpan timeout = new TimeSpan(0, 0, 30);
            binding.SendTimeout = timeout;
            binding.OpenTimeout = timeout;
            binding.ReceiveTimeout = timeout;
            return binding;
        }
    }
}