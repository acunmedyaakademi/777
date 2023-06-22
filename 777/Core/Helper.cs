using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using RestSharp;
using System.Globalization;
using System.Text.RegularExpressions;

namespace _777.Core;

public static class Helper
{
    private class ReCatpchaResponse
    {
        public bool success { get; set; }
        public double score { get; set; }
    }
    public static string DateToString(DateTime date)
    {
        int Month = date.Month;
        int Day = date.Day;
        string MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month);
        return $"{Day} {MonthName}";
    }

    public static int CountText(string text)
    {
        string[] splitedText = text.Split(' ');
        return splitedText.Length;
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

    public static double SentimentAnalysis (string Text)
    {
        string text = $@"{Text}";

        string editedText = Regex.Replace(text, @"[^\w\s]", " ");

        string[] splitedText = editedText.Split(' ');
        string[] positives = File.ReadAllLines("positive-words.txt");
        string[] negatives = File.ReadAllLines("negative-words.txt");
        double positive = 0;
        double negative = 0;

        for (int i = 0; i < splitedText.Length - 1; i++)
        {

            if (string.IsNullOrEmpty(splitedText[i]))
                i++;

            string s = splitedText[i];
            string? a = "";

             
            switch (i)
            {
                case 0:
                    break;
                default:
                    a = splitedText[i - 1];
                    break;
            }


            if ((s.EndsWith("mak")) || (s.EndsWith("mak")))
            {
                positive = positive + 1;
            }

            else if (positives.Any(e => e.Equals(s.ToLower())))
            {
                positive = positive + 1;
            }

            else if ((negatives.Any(e => e.Equals(s.ToLower())) && (a != "" ? a.EndsWith("an") : true || a != "" ? a.EndsWith("en") : true)))
            {
                negative = negative + 1;
            }

            else if (s.ToLower().Contains("ebil") || s.ToLower().Contains("abil") || s.ToLower().Contains("acağ") || s.ToLower().Contains("eceğ"))
            {
                if (s.ToLower().EndsWith("m"))
                {
                    positive = positive + 1;
                }
            }
            else if (negatives.Any(e => e.Equals(s.ToLower())))
            {
                negative = negative + 1;
            }
        }
        double score;
        if (!(positive + negative == 0))
            score = (positive - negative) / (positive + negative);
        else
            score = 0;
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
                smtp.Connect("******", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("***********", "****************");
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

