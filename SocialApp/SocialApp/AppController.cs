using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using AppCommonClasses.Models;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.Storage;
using SocialApp.Repository;
using SocialApp.Services;
using Microsoft.UI.Xaml.Media.Imaging;

namespace SocialApp
{
    public sealed class AppController
    {
        private static readonly Lazy<AppController> instance = new(() => new AppController());

        public static AppController Instance => instance.Value;

        public User CurrentUser { get; set; }

        public AppController() { }

        public bool EmailExists(string email)
        {
            UserRepository userRepository = new UserRepository();
            return userRepository.GetByEmail(email) != null;
        }

        public bool Login(string email, string password)
        {
            UserRepository userRepository = new UserRepository();
            User user = userRepository.GetByEmail(email);
            if (user != null && user.PasswordHash == password)
            {
                CurrentUser = user;
                return true;
            }
            return false;
        }

        public void Register(string username, string email, string password, string image)
        {
            UserRepository userRepository = new UserRepository();
            UserService userService = new UserService(userRepository);
            userService.AddUser(username, email, password, image);
            Login(email, password);
        }

        //trrbuie scoasa
        public void Logout()
        {
            CurrentUser = null;
        }


        public static async Task<string> EncodeImageToBase64Async(StorageFile imageFile)
        {
            try
            {
                // Read the entire file as a byte array
                using (IRandomAccessStream stream = await imageFile.OpenAsync(FileAccessMode.Read))
                {
                    byte[] bytes = new byte[stream.Size];
                    await stream.ReadAsync(bytes.AsBuffer(), (uint)stream.Size, InputStreamOptions.None);
                    return Convert.ToBase64String(bytes);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error encoding image to Base64: {ex.Message}");
                throw;
            }
        }

        public static async Task<BitmapImage> DecodeBase64ToImageAsync(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/User.png"));
            }

            try
            {
                // Decode Base64 to byte array
                byte[] bytes = Convert.FromBase64String(base64String);

                // Create BitmapImage from byte array
                var bitmapImage = new BitmapImage();
                using (var stream = new InMemoryRandomAccessStream())
                {
                    await stream.WriteAsync(bytes.AsBuffer());
                    stream.Seek(0);
                    await bitmapImage.SetSourceAsync(stream);
                }

                return bitmapImage;
            }
            catch (Exception ex)
            {
                // Fallback to default image on error
                System.Diagnostics.Debug.WriteLine($"Error decoding Base64: {ex.Message}");
                return new BitmapImage(new Uri("ms-appx:///Assets/User.png"));
            }
        }

    }
}
