using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using TaskManagerApiWCF.Models;

namespace TaskManagerAndroid.Activities.Tasks
{
    [Activity(Label = "Task Details", Icon = "@drawable/taskDetails")]
    public class TaskDetailsActivity : Activity
    {
        private BaseServicesOf_TaskModelClient client;
        private BaseServicesOf_UserModelClient clientUser;

        int taskID;
        int creatorID;
        int responsibleID;
        TaskModel task = new TaskModel();
        UserModel userCreator = new UserModel();
        UserModel userResponsible = new UserModel();

        TextView title;
        TextView description;
        TextView workingHours;
        TextView status;
        TextView createdDate;
        TextView lastEditDate;
        TextView creator;
        TextView responsible;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.TaskDetails);

            title = FindViewById<TextView>(Resource.Id.titleDetails);
            description = FindViewById<TextView>(Resource.Id.descriptionDetails);
            workingHours = FindViewById<TextView>(Resource.Id.workingHoursDetails);
            status = FindViewById<TextView>(Resource.Id.statusDetails);
            createdDate = FindViewById<TextView>(Resource.Id.createdDateDetails);
            lastEditDate = FindViewById<TextView>(Resource.Id.lastEditDateDetails);
            creator = FindViewById<TextView>(Resource.Id.creatorDetails);
            responsible = FindViewById<TextView>(Resource.Id.responsibleDetails);

            taskID = Intent.GetIntExtra("taskID", 0);

            WebServiceClientManager.InitializeWebServiceClient(typeof(TaskModel));
            client = WebServiceClientManager.TaskClient;

            client.GetByIDAsync(taskID);
            client.GetByIDCompleted += GetByIDTaskCompleted;

            WebServiceClientManager.InitializeWebServiceClient(typeof(UserModel));
            clientUser = WebServiceClientManager.UserClient;

            clientUser.GetByIDAsync(creatorID);
            clientUser.GetUserByIDCompleted += GetCreatorCompleted;

            //clientUser.GetByIDAsync(responsibleID);
            //clientUser.GetUserByIDCompleted += GetResponsibleCompleted;
        }

        // Get By ID
        private void GetByIDTaskCompleted(object sender, GetTaskByIDCompletedEventArgs getByIDTaskCompletedEventArgs)
        {
            task = getByIDTaskCompletedEventArgs.Result;
            creatorID = task.CreatorID;
            responsibleID = task.ResponsibleID;

            RunOnUiThread(() =>
            {
                title.Text = task.Title;
                description.Text = task.Description;
                workingHours.Text = task.WorkingHours.ToString();
                status.Text = task.Status.ToString();
                createdDate.Text = task.CreateDate.ToString();
                lastEditDate.Text = task.LastEditDate.ToString();
                //creator.Text = task.CreatorID.ToString();
                //responsible.Text = task.ResponsibleID.ToString();
            });
        }

        // Creator
        private void GetCreatorCompleted(object sender, GetUserByIDCompletedEventArgs getUserByIDCompletedEventArgs)
        {
            userCreator = getUserByIDCompletedEventArgs.Result;
            RunOnUiThread(() => creator.Text = userCreator.Username);
        }

        // Responsible
        //private void GetResponsibleCompleted(object sender, GetUserByIDCompletedEventArgs getUserByIDCompletedEventArgs)
        //{
        //    userResponsible = getUserByIDCompletedEventArgs.Result;
        //    RunOnUiThread(() => responsible.Text = userResponsible.Username);
        //}
    }
}