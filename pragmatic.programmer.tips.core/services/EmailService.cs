using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using pragmatic.programmer.tips.core.models;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace pragmatic.programmer.tips.core.services
{
    /// <summary>
    /// service for sending emails
    /// </summary>
    public class EmailService
    {
        private readonly EmailServerConfiguration _config;

        /// <summary>
        /// constructor for the email service.
        /// </summary>
        public EmailService(EmailServerConfiguration emailServerConfiguration)
        {
            _config = emailServerConfiguration;
        }

        /// <summary>
        /// send the provided email message.
        /// </summary>
        /// <param name="message"></param>
        public async Task SendAsync(MimeMessage message)
        {
            using var client = _config.UseCustomCertificateInformationInCertificateValidationCallback
            ? new SmtpClient { ServerCertificateValidationCallback = CertificateValidationCallback }
            : new SmtpClient();

            await client.ConnectAsync(_config.Host, _config.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_config.Username, _config.PlaintextPassword);
            await client.SendAsync(message);

            // quit is to "disconnect cleanly"
            await client.DisconnectAsync(quit: true);
        }

        /// <summary>
        /// builds a MimeMessage from the provided parameters using a html body.
        /// </summary>
        /// <param name="subject">the subject of the email</param>
        /// <param name="bodyAsHtml">the body of the email in html</param>
        /// <param name="toEmails">who to send the email to</param>
        /// <param name="fromEmails">who the email is from</param>
        /// <returns>a build MimeMessage from the parameters</returns>
        public static MimeMessage BuildMimeMessageUsingHtml(
            string subject,
            string bodyAsHtml,
            IEnumerable<EmailAddress> toEmails,
            IEnumerable<EmailAddress> fromEmails)
            => BuildMimeMessage(subject, bodyAsHtml, toEmails, fromEmails, TextFormat.Html);

        /// <summary>
        /// builds a MimeMessage from the provided parameters using a plaintext body.
        /// </summary>
        /// <param name="subject">the subject of the email</param>
        /// <param name="bodyAsPlainText">the body of the email in plaintext</param>
        /// <param name="toEmails">who to send the email to</param>
        /// <param name="fromEmails">who the email is from</param>
        /// <returns>a build MimeMessage from the parameters</returns>
        public static MimeMessage BuildMimeMessageUsingPlainText(
            string subject,
            string bodyAsPlainText,
            IEnumerable<EmailAddress> toEmails,
            IEnumerable<EmailAddress> fromEmails)
            => BuildMimeMessage(subject, bodyAsPlainText, toEmails, fromEmails, TextFormat.Text);

        /// <summary>
        /// builds a MimeMessage from the provided parameters.
        /// </summary>
        /// <param name="subject">the subject of the email</param>
        /// <param name="body">the body of the email</param>
        /// <param name="toEmails">who to send the email to</param>
        /// <param name="fromEmails">who the email is from</param>
        /// <param name="bodyFormat">the format that the body text is in</param>
        /// <returns>a build MimeMessage from the parameters</returns>
        private static MimeMessage BuildMimeMessage(
            string subject,
            string body,
            IEnumerable<EmailAddress> toEmails,
            IEnumerable<EmailAddress> fromEmails,
            TextFormat bodyFormat)
        {
            // set up what emails to show in the TO field.
            var to = new InternetAddressList();
            foreach (var sender in toEmails)
                to.Add(new MailboxAddress(sender.Name, sender.Email));

            // set up what emails to show in the FROM field.
            var from = new InternetAddressList();
            foreach (var recipient in fromEmails)
                from.Add(new MailboxAddress(recipient.Name, recipient.Email));

            // make the body of the message and return it.
            var constructedBody = new TextPart(bodyFormat) { Text = body };
            return new MimeMessage(from, to, subject, constructedBody);
        }

        /// <summary>
        /// method for validating the certificates coming from the mail server. this method should only be used if a custom certificate needs to be validated.
        /// see http://www.mimekit.net/docs/html/Frequently-Asked-Questions.htm
        /// </summary>
        /// <param name="sender">the hostname, should come in as a string</param>
        /// <param name="certificate">the X509Certificate certificate to validate</param>
        /// <param name="chain">chain errors with the X509Certificate</param>
        /// <param name="errors">the types of errors</param>
        /// <returns></returns>
        /// <exception cref="Exception">something is wrong with the X509Certificate</exception>
        private bool CertificateValidationCallback(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors errors)
        {
            // no certificate, invalid.
            if (certificate == null)
                return false;

            // no errors, valid.
            if (errors == SslPolicyErrors.None)
                return true;

            // handle RemoteCertificateNotAvailable by throwing Exception.
            var host = (string)sender;
            if ((errors & SslPolicyErrors.RemoteCertificateNotAvailable) != 0)
                throw new Exception($"The certificate was not available for {host}.");

            // handle name mismatch by throwing Exception.
            if ((errors & SslPolicyErrors.RemoteCertificateNotAvailable) != 0)
            {
                var certificateName = certificate is X509Certificate2 asX509Certificate
                    ? asX509Certificate.GetNameInfo(X509NameType.SimpleName, false)
                    : certificate.Subject;

                throw new Exception($"The Common Name for the certificate did not match {host}. Instead, it was {certificateName}.");
            }

            // handle certificate could not be converted into a X509Certificate2
            if (certificate is not X509Certificate2 x509Certificate)
                throw new Exception("The certificate could not be converted into a X509Certificate2, could not continue");

            // handle custom certificate check
            if (IsValidCertificate(x509Certificate))
                return true;

            // if no chain errors to show, return invalid.
            if (chain == null)
                return false;

            // handle chain errors by throwing Exception.
            var error = "The certificate for the server could not be validated for the following reasons: ";
            foreach (var element in chain.ChainElements)
            {
                error += $"\n{element.Certificate.Subject}";
                foreach (var status in element.ChainElementStatus)
                    error += $"\n\t{status.StatusInformation}";
            }
            throw new Exception(error);
        }

        /// <summary>
        /// checks the provided certificate against the CustomCertificateInformation.
        /// </summary>
        /// <param name="certificate">the X509Certificate2 to check against the _config.CustomCertificateInformation</param>
        /// <returns></returns>
        private bool IsValidCertificate(X509Certificate2 certificate)
        {
            // <- breakpoint and copy-paste CertificateInformation.
            return _config.CustomCertificateInformation?.Name == certificate.GetNameInfo(X509NameType.SimpleName, false)
                   && _config.CustomCertificateInformation?.FingerPrint == certificate.Thumbprint
                   && _config.CustomCertificateInformation?.Serial == certificate.SerialNumber
                   && _config.CustomCertificateInformation?.Issuer == certificate.Issuer;
        }
    }
}