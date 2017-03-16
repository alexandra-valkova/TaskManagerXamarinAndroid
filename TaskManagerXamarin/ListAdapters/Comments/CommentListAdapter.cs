using Android.Views;
using Android.Widget;
using TaskManagerAndroid.Activities.Comments;
using TaskManagerApiWCF.Models;

namespace TaskManagerAndroid.ListAdapters.Comments
{
    internal class CommentListAdapter : BaseAdapter<CommentModel>
    {
        private CommentModel[] comments;
        private CommentsActivity commentsActivity;

        public CommentListAdapter(CommentsActivity commentsActivity, CommentModel[] comments)
        {
            this.commentsActivity = commentsActivity;
            this.comments = comments;
        }

        public override CommentModel this[int position]
        {
            get { return comments[position]; }
        }

        public override int Count
        {
            get { return comments.Length; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is avaible

            if (view == null)       // otherwise create a new one
            {
                view = commentsActivity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            }

            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = comments[position].Text;
            view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = comments[position].CreateDate.ToString();

            return view;
        }
    }
}