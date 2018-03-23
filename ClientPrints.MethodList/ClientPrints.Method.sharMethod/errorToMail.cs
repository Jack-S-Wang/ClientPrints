using ClientPrintsMethodList.ClientPrints.Method.sharMethod;
using System;
using System.Net.Mail;
using System.Text;

namespace ClientPrintsMethodList.ClientPrints.Method.sharMethod
{
    public class errorToMail
    {
        private MailMessage mailMessage = new MailMessage();
        private string toClient = "";
        private string user = "";
        private string password = "";
        private string title = "设备异常提示";
        public errorToMail(string message)
        {
            try
            {
                mailMessage.Body = message;
                mailMessage.From = new MailAddress(user);
                mailMessage.To.Add(new MailAddress(toClient));
                mailMessage.Subject = title;
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.Priority = MailPriority.Normal;
                setMail();
            }catch(Exception ex)
            {
                string str = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + string.Format("错误：{0}，追踪位置信息：{1}", ex, ex.StackTrace);
                SharMethod.writeErrorLog(str);
            }
        }

        private void completCall()
        {
            return;
        }

        private void setMail()
        {
            if (mailMessage != null)
            {
                var smtpClient = new SmtpClient();
                smtpClient.Credentials = new System.Net.NetworkCredential(mailMessage.From.Address, password);//设置发件人身份的票据  
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpClient.Host = "smtp." + mailMessage.From.Host;
                smtpClient.SendAsync(mailMessage, mailMessage.Body);
            }
        }
    
}
}
