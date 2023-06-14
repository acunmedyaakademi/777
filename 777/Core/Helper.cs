using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using RestSharp;

namespace _777.Core;

public static class Helper
{
    private class ReCatpchaResponse
    {
        public bool success { get; set; }
        public double score { get; set; }
    }

    public static bool ValidateRecaptcha(string token)
    {
        var client = new RestClient(new RestClientOptions("https://www.google.com/recaptcha/"));
        var request = new RestRequest("api/siteverify");

        request.AddParameter("secret", "SECRET_KEYS_COMES_HERE");
        request.AddParameter("response", token);

        try
        {
            var grResponse = client.Post<ReCatpchaResponse>(request);
            double minScore = 0.6;

            if (!grResponse.success || grResponse.score < minScore)
            {
                return false;
            }
        }
        catch
        {
            return false;
        }

        return true;
    }

    public static double SentimentAnalysis (string a)
    {
        string text = $@"{a}";

        string[] positives = File.ReadAllLines("positive-words.txt");
        string[] negatives = File.ReadAllLines("negative-words.txt");

        double positive = 0;
        double negative = 0;

        foreach (string s in text.Split(' '))
        {
            if (positives.Any(e => e.Equals(s.ToLower())))
            {
                positive = positive + 1;
            }

            else if (negatives.Any(e => e.Equals(s.ToLower())))
            {
                negative = negative + 1;
            }
        }

        double score = (positive - negative) / (positive + negative);

        return score;
    }

    public static bool SendMail(string mailTo, string code)
    {
        try
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("777", "onay@777.com"));
            emailMessage.To.Add(new MailboxAddress("Kullanici", mailTo));
            emailMessage.Subject = "Confirm Mail";
            var builder = new BodyBuilder();
            builder.HtmlBody = code;
            emailMessage.Body = builder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.eu.mailgun.org", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("postmaster@mailer.akademiprojeler.live", "668fb195685bcac61ad8e6c56ed6fdce-102c75d8-e7f16f5e");
                smtp.Send(emailMessage);
                smtp.Disconnect(true);
            }
            return true;

        }
        catch (Exception)
        {
            return false;

        }
    }
}

