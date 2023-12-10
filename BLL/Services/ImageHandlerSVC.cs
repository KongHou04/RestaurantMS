using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace BLL.Services
{
    public class ImageHandlerSVC
    {
        private readonly string _root1; /*employee*/
        private readonly string _root2; /*product*/
        public ImageHandlerSVC(string root1, string root2)
        {
            _root1 = root1;
            _root2 = root2;
        }

        public string GetImageDirecory(string? imageName, bool isUseRoot1 = false)
        {
            string _root = (isUseRoot1)? _root1 : _root2; 
            if (imageName == null)
                imageName = "defaultUser.png";
            return Path.Combine(_root, imageName);
        }

        public BitmapImage? GetBitMap(string? imageName, bool isUseRoot1 = false/*, bool isUseCatcheOptionOnload = false*/)
        {
            string _root = (isUseRoot1) ? _root1 : _root2;
            try
            {
                if (imageName == null)
                    imageName = "defaultUser.png";

                string targetFilePath = Path.Combine(_root, imageName);
                using (var stream = new FileStream(targetFilePath, FileMode.Open, FileAccess.Read))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    return bitmap;
                }
            }
            catch
            {
                return null;
            }
        }
        public void CopyImage(string sourceImagePath, string targetFileName, bool isUseRoot1 = false)
        {
            string _root = (isUseRoot1)? _root1 : _root2;
            try
            {
                // Kiểm tra xem đường dẫn nguồn có tồn tại không
                if (!File.Exists(sourceImagePath))
                {
                    Console.WriteLine($"File not found: {sourceImagePath}");
                    return;
                }

                // Kiểm tra xem thư mục đích có tồn tại không, nếu không, tạo mới
                if (!Directory.Exists(_root))
                {
                    Directory.CreateDirectory(_root);
                }

                // Tạo đường dẫn đích với tên tệp được truyền vào
                string targetFilePath = Path.Combine(_root, targetFileName);
                //string copyFilePath = Path.Combine(_root + "\\Temp", targetFileName);

                // Copy ảnh từ nguồn đến đích
                File.Copy(sourceImagePath, targetFilePath, true);
                //File.Copy(sourceImagePath, copyFilePath, true);

                Console.WriteLine($"Image copied successfully to: {targetFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error copying image: {ex.Message}");
            }
        }
    }
}
