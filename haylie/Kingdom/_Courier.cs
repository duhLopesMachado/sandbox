using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace haylie.Kingdom
{
    public class _Courier
    {
        public static string Template(string p_template)
        {
            string result = "";
            switch (p_template)
            {
                case "bwatag":
                    result = @"<br/>
                            <a href='http://bosswebapps.net' style='margin-top: 63px; width: 100%; max-width: 207px; float: left;background: #efefef; height: 70px; padding: 3px;'>
                                <img src='http://haylie.bosswebapps.net/content/img/logo/bwashortcut.png' style='float: left; width: 70px; margin: 0;' />
                                    
                                <div style='float: right; width: 126px; height: 70px; display: inline-block'>
                                    <p style='margin: 0; width: 100%; float: right; text-align: right; font-family: monospace; color: #666; font-size: 9px;'>powered by</p>
                                    <h1 style='margin: 0; letter-spacing: 1px; font-family: verdana; font-weight: 100; float: right;color: #333;font-size: 25px; margin-top: 9px;'>BOSS</h1>
                                    <p style='margin: 0; width: 100%; float: right; text-align: right; font-family: verdana; color: #333; font-size: 9px;letter-spacing: 1px;'>build the future</p>
                                </div>
                            </a>";
                    break;
            }
            return result;
        }

        public static string Domain(string p_bot) {
            string result = "http://"; 
            switch(p_bot)
            {
                case "agata": result += "web.pedidofinal.com.br"; break; //duhflag> rootdomain instead of sub
                case "wanda": result += "octa.academianewfight.com.br"; break;
                case "swann": result += "neitiri"; break;

            }
            return result;
        }

        public static bool Send(string p_bot, string p_to, string p_sub, string p_body, string p_att = "")
        {
            
            string SMTP = "smtp.bosswebapps.net";
            string mailOwner = p_bot;
            string mailFrom = p_bot + "@bosswebapps.net";
            string key = "M@illocker000741!9"; 
            string mailTo = p_to;

            switch(p_bot)
            {
                case "agata": //pedidofinal
                    SMTP = "smtp.pedidofinal.com.br";
                    mailOwner = "Pedido Final";
                    mailFrom = "app@pedidofinal.com.br";
                    key = "M@illocker963!";
                    break;

                case "wanda": //octa
                    SMTP = "smtp.bosswebapps.net";
                    mailOwner = "New Fight | Overwatch";
                    mailFrom = "overwatch@bosswebapps.net";
                    key = "M@illocker000741!";
                    break;

                case "potts": //bwa
                    SMTP = "smtp.bosswebapps.net";
                    mailOwner = "BWA | Overwatch";
                    mailFrom = "overwatch@bosswebapps.net";
                    key = "M@illocker000741!";
                    break;

                case "ana": //ana
                    SMTP = "smtp.bosswebapps.net";
                    mailOwner = "BWA | ANA";
                    mailFrom = "ana@bosswebapps.net";
                    key = "M@illocker000741!";
                    break;


            }
            
            string subject = p_sub;
            string body = p_body;

            MailMessage objMail = new MailMessage();
            objMail.From = new System.Net.Mail.MailAddress(mailOwner + "<" + mailFrom + ">");
            //objMail.From = new System.Net.Mail.MailAddress(mailFrom);
            objMail.To.Add(mailTo);

            objMail.Priority = System.Net.Mail.MailPriority.Normal;
            objMail.IsBodyHtml = true;
            objMail.Subject = subject;
            objMail.Body = body;

            string att_file = "";
            if (!String.IsNullOrEmpty(p_att))
            {
                att_file = p_att;
                Attachment att = new Attachment(p_att, System.Net.Mime.MediaTypeNames.Application.Octet);
                objMail.Attachments.Add(att);
            }

            //objMail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            //objMail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

            // Caso queira enviar um arquivo anexo
            //Caminho do arquivo a ser enviado como anexo
            //string arquivo = Server.MapPath("arquivo.jpg");

            // Ou especifique o caminho manualmente
            //string arquivo = @"e:\home\LoginFTP\Web\arquivo.jpg";

            // Cria o anexo para o e-mail
            //Attachment anexo = new Attachment(arquivo, System.Net.Mime.MediaTypeNames.Application.Octet);

            // Anexa o arquivo a mensagem
            //objEmail.Attachments.Add(anexo);

            //Cria objeto com os dados do SMTP
            System.Net.Mail.SmtpClient objSmtp = new System.Net.Mail.SmtpClient();
            objSmtp.Credentials = new System.Net.NetworkCredential(mailFrom, key);
            objSmtp.Host = SMTP;
            objSmtp.Port = 587;
            //objSmtp.EnableSsl = true;

            try
            {
                objSmtp.Send(objMail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                objMail.Dispose();
                //anexo.Dispose();
            }

        }
    }
}