using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using TrainSheet.Model.ServiceModel;
using static TrainSheet.Utilities.Utilities;

namespace TrainSheet.ViewModel
{
	public class ProfileVM : BindableObject
    {
        public ObservableCollection<BodyParts> bodyParts { get; set; } = new ObservableCollection<BodyParts>();
        public ObservableCollection<BodyParts> userInfos { get; set; } = new ObservableCollection<BodyParts>();
        public bool     isEditingMesurments { get; set; }
        public bool     isUpdatingPhoto { get; set; }
        public bool     isLoading { get; set; }
        public bool isEditingUser { get; set; }
        public string editUserIcon { get; set; }
        public string   editUserMesurments { get; set; }
        public ImageSource userPhoto { get; set; }
        public ICommand editUserMesurment { get; }
        public ICommand editUserInfo { get; }
        public ICommand editUserImage { get; }
        public ICommand cancelEditImage { get; }
        public ICommand saveUserImage { get; }
        private List<BodyParts> bodyPartsFromDB = new List<BodyParts>();
        private FileResult file;
        private const string SavedFileName = "selected_photo.jpg";
        private readonly string _savedPath = Path.Combine(FileSystem.AppDataDirectory, SavedFileName);

        public ProfileVM()
        {
            isEditingMesurments = false;
            editUserMesurments = "edit";
            isEditingUser = false;
            editUserIcon = "edit";
            editUserInfo        = new AsyncRelayCommand(EditUserInfo);
            editUserMesurment   = new AsyncRelayCommand(EditUserMesurment);
            editUserImage       = new AsyncRelayCommand(EditUserImage);
            cancelEditImage     = new Command(CancelEdit);
            saveUserImage       = new AsyncRelayCommand(SaveUserImage);
        }
        public async Task GetBodyParts()
        {
            try
            {
                bodyPartsFromDB = await bodyPartsDB.GetAllAsync();

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");

            }
        }
        public void SetLoading(bool loading)
        {
            isLoading = loading;
            OnPropertyChanged(nameof(isLoading));
        }
        public void SetUserPhoto()
        {
            if (File.Exists(_savedPath))
                userPhoto = ImageSource.FromFile(_savedPath);
            else
                userPhoto = "profilepic.png";
            OnPropertyChanged(nameof(userPhoto));
        }
        public void SetBodyParts()
        {
            if (bodyParts.Count == 0)
            {
                var bodyPartsMesurments = bodyPartsFromDB.ToList();
                if (bodyPartsMesurments.Any())
                {
                    bodyPartsMesurments.RemoveRange(0, 3);
                    foreach (var bodyPart in bodyPartsMesurments)
                    {
                        bodyParts.Add(bodyPart);
                    }
                    OnPropertyChanged(nameof(bodyParts));
                }
            }
        }
        public void SetUserInfos()
        {
            if (userInfos.Count == 0)
            {
                var bodyPartsMesurments = bodyPartsFromDB.ToList();
                if (bodyPartsMesurments.Any())
                {
                    bodyPartsMesurments.RemoveRange(3, bodyPartsMesurments.Count - 3);
                    try
                    {
                        foreach (var bodyPart in bodyPartsMesurments)
                        {
                            userInfos.Add(bodyPart);
                        }
                    }
                    catch (Exception ex)
                    {
                        Application.Current.MainPage.DisplayAlert("Error 2", ex.ToString(), "OK");
                    }

                    OnPropertyChanged(nameof(userInfos));
                }


            }
        }
        private async Task EditUserMesurment()
        {
            if (isEditingMesurments)
            {
                await SaveUserMesurments();
            }
            isEditingMesurments = !isEditingMesurments;
            OnPropertyChanged(nameof(isEditingMesurments));
            editUserMesurments = isEditingMesurments ? "check" : "edit";
            OnPropertyChanged(nameof(editUserMesurments));
        }
        
        private async Task EditUserInfo()
        {
            if (isEditingUser)
            {
                await SaveUserInfos();
            }
            isEditingUser = !isEditingUser;
            OnPropertyChanged(nameof(isEditingUser));
            editUserIcon = isEditingUser ? "check" : "edit";
            OnPropertyChanged(nameof(editUserIcon));
        }
        private async Task SaveUserMesurments()
        {
            foreach (var bodyPart in bodyParts)
            {
                await bodyPartsDB.SaveAsync(bodyPart);
            }
        }
        private async Task SaveUserInfos()
        {
            foreach (var userInfo in userInfos)
            {
                await bodyPartsDB.SaveAsync(userInfo);
            }
        }
        private async Task EditUserImage()
        {
            isUpdatingPhoto = true;
            OnPropertyChanged(nameof(isUpdatingPhoto));
            try
            {
                file = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Select a photo"
                });

                if (file == null)
                    return; // user cancelled

                userPhoto = ImageSource.FromFile(file.FullPath);
                OnPropertyChanged(nameof(userPhoto));

                
            }
            catch (FeatureNotSupportedException)
            {
                await Application.Current.MainPage.DisplayAlert("Not supported", "Photo picking is not supported on this device.", "OK");
            }
            catch (PermissionException)
            {
                await Application.Current.MainPage.DisplayAlert("Permission", "We don't have permission to access photos.", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        private void CancelEdit()
        {
            isUpdatingPhoto = false;
            OnPropertyChanged(nameof(isUpdatingPhoto));
            SetUserPhoto();
        }
        private async Task SaveUserImage()
        {
            try
            {
                // Delete previously saved copy (if any) so we only keep one
                if (!string.IsNullOrEmpty(_savedPath) && File.Exists(_savedPath))
                {
                    File.Delete(_savedPath);
                }

                // Copy picked photo into the saved path
                using (var sourceStream = await file.OpenReadAsync())
                using (var destinationStream = File.OpenWrite(_savedPath))
                {
                    await sourceStream.CopyToAsync(destinationStream);
                }

                isUpdatingPhoto = false;
                OnPropertyChanged(nameof(isUpdatingPhoto));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }
}

