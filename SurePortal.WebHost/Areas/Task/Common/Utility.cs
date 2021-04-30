using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurePortal.Module.Task.Web
{
    public class Utility
    {
        public static byte[] ReadAllBytes(HttpPostedFileBase instream)
        {

            byte[] fileBytes = new byte[instream.ContentLength];
            System.IO.Stream myStream = instream.InputStream;
            myStream.Read(fileBytes, 0, instream.ContentLength);
            return fileBytes;

        }
    }
}