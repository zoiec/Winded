using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string AsBase64(object photo)
    {
        string img = "data:image/jpg;base64,{0}";
        byte[] bytes = photo as byte[];
        if (bytes != null)
        {
            using (var memStream = new MemoryStream())
            {
                int offset = 78; // For Northwind images only - legacy of the OLE image format
                memStream.Write(bytes, offset, bytes.Length - offset);
                img = string.Format(img, Convert.ToBase64String(memStream.ToArray()));

            }
        }
        else
        {
            img = "";
        }
        return img;
    }
}