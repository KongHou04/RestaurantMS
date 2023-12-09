using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BLL.Services
{
    public class BitMapConverter
    {
        private readonly string _root;
        public BitMapConverter(string root)
        {
            _root = root;
        }

        public BitmapImage? GetBitMap(string? imageName)
        {
            try
            {
                if (imageName == null)
                    imageName = "defaultUser.png";
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(_root + "/" + imageName, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();
                return bitmap;
            }
            catch
            {
                return null;
            }
        }
    }
}
