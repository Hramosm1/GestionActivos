using System.Text;

namespace itmanager.Class
{
    public class MessagesCollection
    {
        public List<Message> Collection = new List<Message>();

        public string toast() {

            StringBuilder s = new StringBuilder();
            TypeMessage msgtype = TypeMessage.information;

            string msg = "";
            string title = "";
            string type = "info";
            int timeout = 5000;

            foreach (var message in Collection) {
                s.Append(message.message);
                msgtype = message.messageType;
                timeout = message.timeout;
                title = message.title;
            }

            switch (msgtype) { 
            
                case TypeMessage.information:
                    type = "info";
                    break;
                case TypeMessage.success:
                    type = "success";
                    break;
                case TypeMessage.warning:
                    type = "warning";
                    break;
                case TypeMessage.error:
                    type = "error";
                    break;
            }

            if (title.Length <= 0) {
                title = type;
            }

            if (s.Length > 0) {
                msg = " const Toast = Swal.mixin({ toast: true, position: 'top-end', showConfirmButton: false, timer:" + timeout.ToString() + ", timerProgressBar: true, didOpen: (toast) => { toast.addEventListener('mouseenter', Swal.stopTimer);" + Environment.NewLine +
                      " toast.addEventListener('mouseleave', Swal.resumeTimer) } }); Toast.fire({ icon: '" + type +"', title:'"+ title +"', text:'"+s+"'})";
            }

            return msg;
        }
    }

    public partial class Message
    {
        public string message = "";
        public string title = "";
        public int timeout = 5000;
        public TypeMessage messageType = TypeMessage.information;
    }

    public enum TypeMessage { 
        information = 1,
        success = 2,
        warning = 3,
        error = 4
    }
}

