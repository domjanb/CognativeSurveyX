using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Plugin.Media;
using Xamarin.Forms.Xaml;
using Plugin.Media.Abstractions;
using CognativeSurveyX.Modell;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Diagnostics;
using System.IO;

namespace CognativeSurveyX.Fregments
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FPhoto : ContentPage
	{
		public FPhoto ()
		{
			InitializeComponent ();
		}

        private async void TakePicture_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            var cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera, Permission.Storage });
                cameraStatus = results[Permission.Camera];
                storageStatus = results[Permission.Storage];
            }
            if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", "No camera available", "OK");
                    return;
                }
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                //Location=Plugin.Media.Abstractions.Location( Constans.myZipPath),
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;
            Debug.WriteLine("iiiiiiiiiitttttttttteeeeeeeeeennnnnn");
            Debug.WriteLine(Constans.myZipPath);
            string pt1= Path.Combine(Constans.myZipPath, "photo");
            if (!Directory.Exists(pt1))
            {
                Directory.CreateDirectory(pt1);
            }
            string fileNeve;
            string fileNeve1 = "photo_" + Constans.kerdivUser + "_" + Constans.kerdivId + "_" + Constans.kerdivAlid + "_" + Constans.kerdivVer + Constans.aktQuestion.kerdeskod + "_";
            //String fileNeve1 = vUser + "_" + vKerdivid + "_" + vKerdivalid + "_" + vKerdivtip + "_" + vKerdivver + "_" + q_data.getKerdeskod() + "_";
            var fotoIndex = 0;
            Boolean oki = false;

            while (!oki)
            {
                fotoIndex = fotoIndex + 1;
                fileNeve = pt1 + "/foto_" + fileNeve1 + fotoIndex + ".jpg";
                //Log.e("fileok2_keresnev",fileNeve);
                if (!File.Exists(fileNeve))
                {
                    File.Copy(file.Path, fileNeve);
                    Constans.valaszok = Constans.valaszok + Constans.aktQuestion.kerdeskod + "_" + Convert.ToString(fotoIndex) + "=" + fileNeve + ";" ;
                    break;
                }
                
            }

            //File.Copy(file.Path,)

            //await DisplayAlert("File Location", file.Path, "OK");
            
            image1.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                
                file.Dispose();
                return stream;
            });
        }
        private void _Continue_Clicked(object sender, EventArgs e)
        {

            Constans.nextPage();
            Navigation.PushModalAsync(new FPage());
        }
    }
}