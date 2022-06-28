namespace BlogMVC.Models
{
    public class Helper
    {
        public bool IsAdmin()
        {
            IHttpContextAccessor HttpContextAccessor = new HttpContextAccessor();
            var session = HttpContextAccessor.HttpContext.Session;
            if (session.GetInt32("_ROLE") != 0)
            {
                return false;
            }
            return true;
        }
    }
}
