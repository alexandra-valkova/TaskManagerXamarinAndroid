using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.ComponentModel;
using System.Linq;
using TaskManagerApiWCF.Models;

namespace TaskManagerAndroid.Activities.Users
{
    [Activity(Label = "Edit User", Icon = "@drawable/user")]
    public class UserEditActivity : Activity
    {
        private BaseServicesOf_UserModelClient client;

        int userID;
        public UserModel userModel = null;
        public UserModel userCheck = new UserModel();

        EditText username;
        EditText password;
        EditText firstName;
        EditText lastName;
        CheckBox admin;
        TextView errors;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EditUser);

            userID = Intent.GetIntExtra("userID", 0);

            WebServiceClientManager.InitializeWebServiceClient(typeof(UserModel));
            client = WebServiceClientManager.UserClient;

            if (userID > 0)
            {
                client.GetByIDAsync(userID);
                client.GetUserByIDCompleted += GetUserByIDCompleted;
            }

            username = FindViewById<EditText>(Resource.Id.usernameEdit);
            password = FindViewById<EditText>(Resource.Id.passwordEdit);
            firstName = FindViewById<EditText>(Resource.Id.firstNameEdit);
            lastName = FindViewById<EditText>(Resource.Id.lastNameEdit);
            admin = FindViewById<CheckBox>(Resource.Id.adminEdit);

            Button save = FindViewById<Button>(Resource.Id.saveUser);
            Button delete = FindViewById<Button>(Resource.Id.deleteUser);
            Button cancel = FindViewById<Button>(Resource.Id.cancelUser);
            errors = FindViewById<TextView>(Resource.Id.errorsEditUser);

            save.Click += SaveButtonOnClick;

            delete.Click += DeleteButtonOnClick;

            cancel.Click += delegate
            {
                Finish();
            };
        }

        // Get By ID
        private void GetUserByIDCompleted(object sender, GetUserByIDCompletedEventArgs getUserByIDCompletedEventArgs)
        {
            userModel = getUserByIDCompletedEventArgs.Result;

            RunOnUiThread(() =>
            {
                username.Text = userModel.Username;
                password.Text = userModel.Password;
                firstName.Text = userModel.FirstName;
                lastName.Text = userModel.LastName;
                admin.Checked = userModel.IsAdmin;
            });
        }

        // SAVE
        private void SaveButtonOnClick(object sender, EventArgs e)
        {
            if (userModel == null)
            {
                userModel = new UserModel();
            }

            userModel.Username = username.Text;
            userModel.Password = password.Text;
            userModel.FirstName = firstName.Text;
            userModel.LastName = lastName.Text;
            userModel.IsAdmin = admin.Checked;

            client.GetAllAsync();
            client.GetAllUsersCompleted += CheckUsernameCompleted;

            if (userCheck == null)
            {
                client.SaveAsync(userModel);
                client.SaveCompleted += SaveUserCompleted;
            }

            else
            {
                RunOnUiThread(() => errors.Text = "Username already exists!");
            }
        }

        private void CheckUsernameCompleted(object sender, GetAllUsersCompletedEventArgs getAllUsersCompletedEventArgs)
        {
            userCheck = getAllUsersCompletedEventArgs.Result.Where(u => u.Username == userModel.Username && u.ID != userModel.ID).FirstOrDefault();
        }

        private void SaveUserCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Finish();
        }

        // DELETE
        private void DeleteButtonOnClick(object sender, EventArgs e)
        {
            client.DeleteAsync(userModel);
            client.DeleteCompleted += DeleteUserCompleted;
        }

        private void DeleteUserCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Finish();
        }
    }
}