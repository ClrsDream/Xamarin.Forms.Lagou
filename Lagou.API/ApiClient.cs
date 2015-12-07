using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lagou.API {
    public class MessageArgs : EventArgs {
        public ErrorTypes ErrorType {
            get;
            set;
        }

        public string Message {
            get;
            set;
        }
    }

    public class ApiClient {

        private static ApiClient Instance = null;
        private static object LockObj = new Object();

        public static event EventHandler<MessageArgs> OnMessage;

        private static ApiClient GetInstance() {
            if (Instance == null) {
                lock (LockObj) {
                    Instance = new ApiClient();
                }
            }
            return Instance;
        }

        private ApiClient() {

        }

        public string GetUrl(MethodBase method) {
            return string.Format("http://www.lagou.com/{0}", method.Module);
        }

        public static async Task<TResult> Execute<TResult>(MethodBase<TResult> method) {
            return await ApiClient.GetInstance()
                .InnerExecute(method)
                .ContinueWith((t, m) => {
                    var mt = (MethodBase)m;
                    if (mt.HasError && OnMessage != null)
                        OnMessage.Invoke(null, new MessageArgs() {
                            ErrorType = mt.ErrorType ?? ErrorTypes.Unknow,
                            Message = mt.Message
                        });
                    return t.Result;
                }, method);
        }

        private async Task<TResult> InnerExecute<TResult>(MethodBase<TResult> method) {
            return await method.Execute(this);
        }
    }
}
