using Microsoft.AspNetCore.Mvc;

namespace itmanager.Class
{
    public class SessionHandler
    {
        public SessionHandler(HttpContext context) {

            if (String.IsNullOrEmpty(context.Session.GetString("userid"))) {
                context.Session.Clear();
            }


            //@if(String.IsNullOrEmpty(HttpContext.Session.GetString("userid"))) {
            //    Context.Session.Clear();
            //    Context.Response.Redirect("Index");
            //}



        }


    }
}
