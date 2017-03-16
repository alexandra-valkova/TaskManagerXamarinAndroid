using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.ComponentModel;
using System.Linq;
using TaskManagerAndroid.Activities.Tasks;
using TaskManagerApiWCF.Models;

namespace TaskManagerAndroid.Activities
{
    [Activity(Label = "Task Manager", MainLauncher = true, Icon = "@drawable/task")]
    public class MainActivity : Activity
    {
        private BaseServicesOf_UserModelClient client;

        private EditText username;
        private EditText password;
        private Button login;
        private Button register;
        private TextView result;

        private UserModel userCheck = new UserModel();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            WebServiceClientManager.InitializeWebServiceClient(typeof(UserModel));
            client = WebServiceClientManager.UserClient;

            username = FindViewById<EditText>(Resource.Id.username);
            password = FindViewById<EditText>(Resource.Id.password);
            login = FindViewById<Button>(Resource.Id.login);
            register = FindViewById<Button>(Resource.Id.register);
            result = FindViewById<TextView>(Resource.Id.result);

            login.Click += LoginButtonOnClick;
            register.Click += RegisterButtonOnClick;
        }

        // LOGIN
        private void LoginButtonOnClick(object sender, EventArgs eventArgs)
        {
            client.GetAllAsync();
            client.GetAllUsersCompleted += LoginCompleted;
        }

        private void LoginCompleted(object sender, GetAllUsersCompletedEventArgs getAllUsersCompletedEventArgs)
        {
            string msg = null;

            if (getAllUsersCompletedEventArgs.Error != null)
            {
                msg += getAllUsersCompletedEventArgs.Error.Message;
                msg += getAllUsersCompletedEventArgs.Error.InnerException;
            }

            else if (getAllUsersCompletedEventArgs.Cancelled)
            {
                msg = "Request was cancelled.";
            }

            else
            {
                UserModel user = getAllUsersCompletedEventArgs.Result.Where(u => u.Username == username.Text && u.Password == password.Text).FirstOrDefault();

                if (user != null)
                {
                    AuthenticationService.LoggedUser = user;
                    AuthenticationService.IsLogged = true;

                    if (user.IsAdmin)
                    {
                        StartActivity(typeof(MenuActivity));
                    }

                    else
                    {
                        StartActivity(typeof(TasksActivity));
                    }
                }

                else
                {
                    msg = "Wrong username or password!";
                }
            }
            RunOnUiThread(() => result.Text = msg);
        }

        // REGISTER
        private void RegisterButtonOnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(username.Text) && string.IsNullOrEmpty(password.Text))
            {
                result.Text = "Username and password cannot be null!";
            }

            else if (string.IsNullOrEmpty(username.Text))
            {
                result.Text = "Username cannot be null!";
            }

            else if (string.IsNullOrEmpty(password.Text))
            {
                result.Text = "Password cannot be null!";
            }

            else
            {
                client.GetAllAsync();
                client.GetAllUsersCompleted += CheckUsername;

                if (userCheck == null)
                {
                    UserModel user = new UserModel
                    {
                        ID = 0,
                        Username = username.Text,
                        Password = password.Text
                    };
                    client.SaveAsync(user);
                    client.SaveCompleted += RegisterCompleted; 
                }

                else
                {
                    RunOnUiThread(() => result.Text = "Username already exists!");
                }
            }
        }

        private void CheckUsername(object sender, GetAllUsersCompletedEventArgs getAllUsersCompletedEventArgs)
        {
            userCheck = getAllUsersCompletedEventArgs.Result.Where(u => u.Username == username.Text).FirstOrDefault();
        }

        private void RegisterCompleted(object sender, AsyncCompletedEventArgs eventArgs)
        {
            StartActivity(typeof(MenuActivity));
        }
    }
}