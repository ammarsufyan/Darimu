using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darimu.ClassFolder
{
    class ClassValidasi
    {
        public String cekAngka(String str)
        {
            String hasil = "false";

            char[] data = str.ToCharArray();
            for (int i = 0; i < data.Length; i++)
            {
                Boolean cek = Char.IsDigit(data[i]);
                if (cek == true)
                {
                    hasil = "true";
                }
            }
            return hasil;
        }

        public String cekHuruf(String str)
        {
            String hasil = "false";

            char[] data = str.ToCharArray();
            for (int i = 0; i < data.Length; i++)
            {
                Boolean cek = Char.IsLetter(data[i]);
                if (cek == true)
                {
                    hasil = "true";
                }
            }
            return hasil;
        }
    }
}
