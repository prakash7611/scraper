using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace YelpScraper
    {
    class ClassWebScraper
        {
        public string getHtml(string Url, bool isLocalPath)
            {
            if (isLocalPath)
                {
                return File.ReadAllText(Url);
                }
            else
                {
                try
                    {
                    System.Net.HttpWebRequest hr = null;
                    System.Net.WebResponse response = null;
                    StreamReader streamReader = null;
                    hr = System.Net.HttpWebRequest.CreateHttp(Url);
                    response = hr.GetResponse();
                    streamReader = new StreamReader(response.GetResponseStream(), true);
                    string strHtml;
                    strHtml = streamReader.ReadToEnd();
                    streamReader.Close();
                    response.Close();
                    if (strHtml.Contains("Error Message"))
                        {
                        strHtml = "";
                        }
                    return strHtml;
                    }
                catch (Exception ex)
                    {
                    return "error:" + Url;
                    }
                }
            }

        public string getResponseUrl(string Url, bool isLocalPath)
        {
            if (isLocalPath)
            {
                return File.ReadAllText(Url);
            }
            else
            {
                try
                {
                    System.Net.HttpWebRequest hr = null;
                    System.Net.WebResponse response = null;
                    StreamReader streamReader = null;
                   hr = System.Net.HttpWebRequest.CreateHttp(Url);
                   hr.AllowAutoRedirect = false;
                   response = hr.GetResponse();
                   streamReader = new StreamReader(response.GetResponseStream(), true);
                   string strHtml;
                   strHtml = streamReader.ReadToEnd();
                   streamReader.Close();
                   response.Close();
                  //  return response.ResponseUri.AbsolutePath; 
                // WebHeaderCollection wc = hr.Headers;

                   return strHtml;
                 //   return "";
                }
                catch (Exception ex)
                {
                    return "error:" + Url;
                }
            }
        }

        public bool DownloadRemoteImageFile(string uri, string fileName)
            {
            if (File.Exists(fileName))
                {
                File.Delete(fileName);
                }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response;
            try
                {
                response = (HttpWebResponse)request.GetResponse();
                }
            catch (Exception)
                {
                return false;
                }

            
            if ((response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.Moved ||
                response.StatusCode == HttpStatusCode.Redirect) &&
                response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
                {

                // if the remote file was found, download it
                using (Stream inputStream = response.GetResponseStream())
                using (Stream outputStream = File.OpenWrite(fileName))
                    {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    do
                        {
                        bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bytesRead);
                        } while (bytesRead != 0);
                    }
                return true;
                }
            else
                return false;
            }

        public string getPath(string url)
            {
            url = url.Replace(":", "_");
            url = url.Replace("/", "_");
            url = url.Replace(".", "_");
            url = url.Replace("\\", "_");
            url = url.Replace("*", "_");
            url = url.Replace("?", "_");
            url = url.Replace("\"", "_");
            url = url.Replace("<", "_");
            url = url.Replace(">", "_");
            return url;
            //\ / : * ? " < > |
            }
        
        }
    }
