using BaiTapXoSoCacMien;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BaiTapSoXoCacMien
{
    public class RssService
    {
        private readonly HttpClient httpClient = new HttpClient();

        public async Task<List<KetQuaSoXo>> GetAllLotteryResultsAsync(string url)
        {
            var results = new List<KetQuaSoXo>();
            var xml = await httpClient.GetStringAsync(url);
            var doc = XDocument.Parse(xml);

            foreach (var item in doc.Descendants("item"))
            {
                string title = item.Element("title")?.Value ?? "";
                string pubDate = item.Element("pubDate")?.Value ?? "";
                string description = item.Element("description")?.Value ?? "";
                string link = item.Element("link")?.Value ?? "";

                DateTime d;
                if (!TryParseDateFromLink(link, out d) && !TryParseDateFromTitle(title, out d))
                {
                    DateTimeOffset dto;
                    d = DateTimeOffset.TryParse(pubDate, CultureInfo.InvariantCulture,
                                                DateTimeStyles.AllowWhiteSpaces, out dto)
                        ? dto.Date : DateTime.MinValue;
                }

                results.Add(new KetQuaSoXo
                {
                    TieuDe = title,
                    NgayCongBo = d == DateTime.MinValue ? pubDate : d.ToString("yyyy-MM-dd"),
                    NoiDung = description
                });
            }
            return results;
        }

        private static bool TryParseDateFromLink(string link, out DateTime date)
        {
            date = default(DateTime);
            if (string.IsNullOrWhiteSpace(link)) return false;

            string path;
            Uri uri;
            if (Uri.TryCreate(link, UriKind.Absolute, out uri))
                path = uri.AbsolutePath;
            else
                path = link;

            int idx = path.IndexOf("ngay-", StringComparison.OrdinalIgnoreCase);
            if (idx < 0) return false;

            int start = idx + 5;
            string after = path.Substring(start);
            string token = after.Split('/')[0]; // e.g. 12-9-2025

            string[] parts = token.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            int d, m, y;
            if (parts.Length != 3 ||
                !int.TryParse(parts[0], out d) ||
                !int.TryParse(parts[1], out m) ||
                !int.TryParse(parts[2], out y))
                return false;

            try
            {
                date = new DateTime(y, m, d);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool TryParseDateFromTitle(string title, out DateTime date)
        {
            date = default(DateTime);
            if (string.IsNullOrWhiteSpace(title)) return false;

            int start = title.IndexOfAny("0123456789".ToCharArray());
            if (start < 0) return false;

            int end = start;
            while (end < title.Length &&
                   (char.IsDigit(title[end]) || title[end] == '/' || title[end] == '-'))
            {
                end++;
            }

            if (end <= start) return false;

            string token = title.Substring(start, end - start); 
            string[] parts = token.Split(new[] { '/', '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2) return false;

            int d, m;
            if (!int.TryParse(parts[0], out d) || !int.TryParse(parts[1], out m)) return false;

            int y = DateTime.Now.Year;
            if (parts.Length >= 3)
            {
                int yp;
                if (!int.TryParse(parts[2], out yp)) return false;
                y = yp < 100 ? yp + 2000 : yp;
            }

            try
            {
                date = new DateTime(y, m, d);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
