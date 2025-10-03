using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiTap_C
{
    public class QLHocPhan
    {
        public List<HocPhan> DSHP { get; private set; }
        public QLHocPhan()
        {
            DSHP = new List<HocPhan>();
        }
        public void ThemHP(HocPhan hp)
        {
            DSHP.Add(hp);
        }

        public void Them_CapNhat(HocPhan hp)
        {
            var existing = DSHP.FirstOrDefault(h => h.MaHP == hp.MaHP);
            if (existing != null)
            {
                existing.TenHP = hp.TenHP;
                existing.SoTinChi = hp.SoTinChi;
            }
            else
            {
                ThemHP(hp);
            }
        }

        public void XoaHP(string maHP)
        {
            var hp = DSHP.FirstOrDefault(h => h.MaHP == maHP);
            if (hp != null)
            {
                DSHP.Remove(hp);
            }
        }

        public void XoaNhieuHP(List<string> maHPs)
        {
            DSHP.RemoveAll(h => maHPs.Contains(h.MaHP));
        }

        public void DocFileTXT(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File không tồn tại", filePath);

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length != 3) continue;

                string maHP = parts[0].Trim();
                string tenHP = parts[1].Trim();
                if (!int.TryParse(parts[2].Trim(), out int soTinChi)) continue;

                if (string.IsNullOrWhiteSpace(maHP) || string.IsNullOrWhiteSpace(tenHP)) continue;

                var hp = new HocPhan
                {
                    MaHP = maHP,
                    TenHP = tenHP,
                    SoTinChi = soTinChi
                };

                Them_CapNhat(hp);
            }
        }

        public void SaveToFile(string filePath)
        {
            var lines = DSHP.Select(hp => $"{hp.MaHP},{hp.TenHP},{hp.SoTinChi}");
            File.WriteAllLines(filePath, lines);
        }

    }
}
