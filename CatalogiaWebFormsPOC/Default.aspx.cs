using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatalogiaWebForms
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void Page_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            // Handle specific exception.
            if (exc is HttpUnhandledException)
            {
                Server.Transfer("ErrorPage.aspx?handler=Page_Error%20-%20Default.aspx",
                    true);

            }
            // Clear the error from the server.
            // Server.ClearError();
        }
    }
}