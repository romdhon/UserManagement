namespace UserManagement.ViewModel
{
    public class UserUpdateViewModel : UserCreateViewModel
    {
        public int ID { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}